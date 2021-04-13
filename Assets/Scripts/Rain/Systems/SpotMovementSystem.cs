using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Rain.Components;
using Unity.Physics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Rain.Systems
{
    public class SpotMovementSystem : ComponentSystem
    {
        private Random _random;
        private EntityManager _entityManager;
        private PlayerControll _control;
        
        private float _timeDelay;
        
        private const int StartCount = 20;
        private const float MAXSpotSpeed = 0.1f;

        protected override void OnCreate()
        {
            _control = new PlayerControll();
            _control.Enable();
            _random = new Random((uint) GetHashCode());
            _entityManager = World.EntityManager;
        }

        protected override void OnStartRunning()
        {
            for (var i = 0; i < StartCount; i++)
            {
                
                SpawnSpot(GameManager.Instance.halfDelta,5,MAXSpotSpeed);
            }
            _timeDelay = GameManager.Instance.spawnDelay;
        }
        private float _angleX;

        private static void AngleClamp(ref float angle)
        {
            if (angle > 180) angle = -180;
            else if (angle < -180) angle = 180;
        }

        protected override void OnUpdate()
        {
            
            _timeDelay -= Time.DeltaTime;
            if (_timeDelay <= 0.0f)
            {
                SpawnSpot(GameManager.Instance.spawnHeight,5,MAXSpotSpeed);
                _timeDelay = _random.NextFloat(0, GameManager.Instance.spawnDelay * 2);
            }
            Entities.WithAll<SpotComponent>().ForEach(
                (Entity spot, ref Translation translation, ref SpotComponent spotComponent,
                    ref PhysicsVelocity velocity, ref LocalToWorld ltw) =>
                {
                    
                    Vector2 axis = _control.MovementAxis.Move.ReadValue<Vector2>();
                    float axisFactor = 0.05f;
                    float horizontalAxis = axis[0];
                    float verticalAxis = axis[1];
                    
                    GameObject camera = GameManager.Instance.gameObject;
                    velocity.Linear = spotComponent.Velocity/Time.DeltaTime;
                    if (EntityManager.HasComponent<PlayerComponent>(spot))
                    {
                        
                        spotComponent.Velocity =
                            new float3(0, spotComponent.Velocity.y, 0) +
                            (float3) camera.transform.forward * verticalAxis * axisFactor +
                            (float3)camera.transform.right * horizontalAxis * axisFactor;
                        
                        float3 forwardVector = ltw.Forward;
                        float3 rightVector = ltw.Right;
                        
                        AngleClamp(ref _angleX);
                        _angleX -= _control.MouseAxis.MouseDeltaX.ReadValue<float>();
                       
                        float aX = math.sin(_angleX) * GameManager.Instance.CameraDistance;
                        float bX = math.cos(_angleX) * GameManager.Instance.CameraDistance;
                        
                        
                        float3 verticalRotation = forwardVector * aX + rightVector * bX;
                        camera.transform.position = translation.Value + verticalRotation + new float3(0,GameManager.Instance.CameraOffset.y,0);
                        camera.transform.rotation = Quaternion.LookRotation((Vector3)translation.Value - camera.transform.position);
                        
                    }
                    if (translation.Value.y < GameManager.Instance.deadHeight)
                    {
                        _entityManager.DestroyEntity(spot);
                    }
                });
        }
        
        
        void SpawnSpot(float y,float radius,float maxSpeed)
        {
            Entity spawnedSpot = _entityManager.Instantiate(GameManager.Instance.spotEntity);
            _entityManager.SetComponentData(spawnedSpot, new Translation
            {
                Value = new float3(_random.NextFloat(-radius, radius), y, _random.NextFloat(-radius, radius))
            });
            
            _entityManager.AddComponentData(spawnedSpot, new SpotComponent
            {
                Velocity = new float3(0, _random.NextFloat(-maxSpeed, 0), 0)
            });
        }

    }
}