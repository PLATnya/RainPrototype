
using Unity.Entities;

using Unity.Mathematics;
using Unity.Transforms;
using Rain.Components;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Rain.Systems
{
    public class SpotMovementSystem : ComponentSystem
    {
        private Random random;
        private int startCount = 20;
        private float timeDelay;
        private EntityManager entityManager;

        protected override void OnCreate()
        {
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

        protected override void OnUpdate()
        {
            timeDelay -= Time.DeltaTime;
            if (timeDelay <= 0.0f)
            {
                SpawnSpot(GameManager.Instance.spawnHeight);
                timeDelay = random.NextFloat(0, GameManager.Instance.spawnDelay * 2);
            }
           
            Entities.WithAll<SpotComponent>().ForEach(
                (Entity spot, ref Translation translation, ref SpotComponent spotComponent) =>
                {
                    translation.Value += spotComponent.Velocity;
                    if (EntityManager.HasComponent<PlayerComponent>(spot))
                    {
                        if (Input.GetKey(KeyCode.LeftArrow)) spotComponent.Velocity = new float3(math.clamp(spotComponent.Velocity.x-0.005f,-0.05f,0.05f), spotComponent.Velocity.y, 0);
                        if (Input.GetKey(KeyCode.RightArrow)) spotComponent.Velocity = new float3(math.clamp(spotComponent.Velocity.x+0.005f,-0.05f,0.05f), spotComponent.Velocity.y, 0);
                        GameManager.Instance.gameObject.transform.position = translation.Value + new float3(0, 0, -7);
                    }

                    if (translation.Value.y < GameManager.Instance.deadHeight)
                    {
                        entityManager.DestroyEntity(spot);
                    }
                });

        }

        void AddParent(Entity child, Entity parent)
        {
            entityManager.AddComponentData(child, new Parent {Value = parent});
            entityManager.AddComponentData(child, new LocalToParent());
        }

        void SpawnSpot(float y)
        {
            Entity spawnedSpot = entityManager.Instantiate(GameManager.Instance.spotEntity);
            entityManager.SetComponentData(spawnedSpot, new Translation
            {
                Value = new float3(random.NextFloat(-5, 5), y, 0)
            });
            entityManager.AddComponentData(spawnedSpot, new SpotComponent
            {
                Velocity = new float3(0, random.NextFloat(-0.1f, 0), 0)
            });
        }

    }
}