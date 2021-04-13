
using System;
using Unity.Entities;

using Unity.Mathematics;
using Unity.Transforms;
using Rain.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Physics;
using Unity.Physics.Authoring;
using Unity.Rendering;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ParticleSystemJobs;
using UnityEngine.PlayerLoop;
using Random = Unity.Mathematics.Random;

namespace Rain.Systems
{
    public class SpotMovementSystem : ComponentSystem
    {
        private Random random;
        private int startCount = 20;
        private float timeDelay;
        private EntityManager entityManager;

        PlayerControll controll;
        protected override void OnCreate()
        {
            controll = new PlayerControll();
            controll.Enable();
            random = new Random((uint) GetHashCode());
            entityManager = World.EntityManager;
        }

        protected override void OnStartRunning()
        {
            for (int i = 0; i < startCount; i++)
            {
                SpawnSpot(5.0f);
            }
            timeDelay = GameManager.Instance.spawnDelay;
        }
        private float angleX;
        private float angleY;

        void AngleClamp(ref float angle)
        {
            if (angle > 180) angle = -180;
            else if (angle < -180) angle = 180;
        }

        
        
        
        
        
        private float maxSpeed = 0.1f;
        protected override void OnUpdate()
        {
            
            timeDelay -= Time.DeltaTime;
            if (timeDelay <= 0.0f)
            {
                SpawnSpot(GameManager.Instance.spawnHeight);
                timeDelay = random.NextFloat(0, GameManager.Instance.spawnDelay * 2);
            }
            Entities.WithAll<SpotComponent>().ForEach(
                (Entity spot, ref Translation translation, ref SpotComponent spotComponent,
                    ref PhysicsVelocity velocity, ref LocalToWorld ltw) =>
                {
                    try
                    {
                        
                        Entity particleSystemEntity = entityManager.GetBuffer<Child>(spot)[0].Value;
                        ParticleSystem particleSystem =entityManager.GetComponentObject<ParticleSystem>(particleSystemEntity);
                        
                        float speed = -velocity.Linear.y;
                        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
                        
                        
                        float3 emitOffset = random.NextFloat3(new float3(-0.7f, -0.7f, -0.7f), new float3(0.7f, 0.7f, 0.7f));
                        emitParams.position = emitOffset; 
                        emitParams.velocity =Vector3.forward*( 0 + (speed - 0) * (5 - 0) / (maxSpeed - 0))/100;
                        particleSystem.Emit(emitParams,1);
                        


                    }
                    catch (Exception ex)
                    {
                        Debug.Log(ex.Message);
                    }


                    Vector2 axis = controll.MovementAxis.Move.ReadValue<Vector2>();
                    float HorizontalAxis = axis[0];
                    float VerticalAxis = axis[1];
                    
                    GameObject camera = GameManager.Instance.gameObject;
                    velocity.Linear = spotComponent.Velocity/Time.DeltaTime;
                    if (EntityManager.HasComponent<PlayerComponent>(spot))
                    {
                        spotComponent.Velocity =
                            new float3(0, spotComponent.Velocity.y, 0) +
                            (float3) camera.transform.forward * VerticalAxis * 0.05f +
                            (float3)camera.transform.right * HorizontalAxis * 0.05f;
                        
                        float3 forwardVector = ltw.Forward;
                        float3 rightVector = ltw.Right;
                        
                        AngleClamp(ref angleX);
                        angleX -= controll.MouseAxis.MouseDeltaX.ReadValue<float>();
                       
                        float a_X = math.sin(angleX) * GameManager.Instance.CameraDistance;
                        float b_X = math.cos(angleX) * GameManager.Instance.CameraDistance;
                        
                        
                        float3 VerticalRotation = forwardVector * a_X + rightVector * b_X;
                        camera.transform.position = translation.Value + VerticalRotation + new float3(0,GameManager.Instance.CameraOffset.y,0);
                        camera.transform.rotation = Quaternion.LookRotation((Vector3)translation.Value - camera.transform.position);
                        
                    }
                    if (translation.Value.y < GameManager.Instance.deadHeight)
                    {
                        entityManager.DestroyEntity(spot);
                    }
                });
        }
        
        
        void SpawnSpot(float y)
        {
            Entity spawnedSpot = entityManager.Instantiate(GameManager.Instance.spotEntity);
            entityManager.SetComponentData(spawnedSpot, new Translation
            {
                Value = new float3(random.NextFloat(-5, 5), y, random.NextFloat(-5, 5))
            });

            entityManager.AddComponentData(spawnedSpot, new SpotComponent
            {
                Velocity = new float3(0, random.NextFloat(-maxSpeed, 0), 0)
            });
        }

    }
}