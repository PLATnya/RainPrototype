
using Rain.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditor;
using UnityEditor.Build.Pipeline.Tasks;
using UnityEngine;


namespace Rain.Systems
{
    public class PlayerComponentSystem: SystemBase
    {
        private EntityQuery spotGroup;
        private bool teleport;
        protected override void OnCreate()
        {
            
            spotGroup = GetEntityQuery(new EntityQueryDesc
            {
                All = new ComponentType[]
                {
                    typeof(SpotComponent),
                    typeof(Translation)
                }
            });
        }

        [BurstCompile]
        struct TeleportPlayerJob:IJobChunk
        {
            [ReadOnly] public EntityTypeHandle EntityHandles;
            [ReadOnly] public ComponentDataFromEntity<PlayerComponent> PlayerData;
            public ComponentTypeHandle<SpotComponent> SpotHandles;
            public ComponentTypeHandle<Translation> TranslationHandles;
            
            public float halfDelta;
            private bool playerIschecked;
            public EntityCommandBuffer.ParallelWriter ConcurrentCommands;
            public NativeArray<bool> teleportArray;
            public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
            {
                NativeArray<Entity> chunkEntityData = chunk.GetNativeArray(EntityHandles);
                NativeArray<SpotComponent> chunkSpotData = chunk.GetNativeArray(SpotHandles);
                NativeArray<Translation> chunkTranslationData = chunk.GetNativeArray(TranslationHandles);
                for (int i = 0; i < chunk.Count; ++i)
                {
                    Entity entity = chunkEntityData[i];
                    SpotComponent spotComponent = chunkSpotData[i];
                    Translation translation = chunkTranslationData[i];
                    if (PlayerData.HasComponent(entity))
                    {
                        if (translation.Value.y <= halfDelta * (-2))
                        {
                            teleportArray[0] = true;
                        }
                    }
                }
                /*playerIschecked = false;
                teleport = false;
                for (int i = 0; i < chunk.Count; ++i)
                {
                    Debug.Log(i);
                    Entity entity = chunkEntityData[i];
                    SpotComponent spotComponent = chunkSpotData[i];
                    Translation translation = chunkTranslationData[i];
                    if (teleport)
                    {
                        if (translation.Value.y <= -halfDelta)
                        {
                            translation.Value.y += halfDelta * 4;
                            chunkTranslationData[i] = translation;
                        }
                        else if (translation.Value.y > halfDelta &&
                                 translation.Value.y < halfDelta*3)
                        {
                            ConcurrentCommands.DestroyEntity(chunkIndex,entity);
                            i--;
                        }
                    }
                    if (PlayerData.HasComponent(entity))
                    {
                        if (!playerIschecked)
                        {
                            if (translation.Value.y <= halfDelta * (-2))
                            {
                                teleport = true;
                                playerIschecked = true;
                                i = -1;
                                
                            }
                            else
                            {
                                teleport = false;
                            }
                        }
                    }
                    
                    
                }*/


            }

            
        }

        
        protected override void OnUpdate()
        {
            /*NativeArray<float> backPoints = new NativeArray<float>(3, Allocator.Persistent);
            var teleportJob =Entities.WithAll<SpotComponent>().WithReadOnly(backPoints).ForEach((Entity entity, ref Translation translation, ref SpotComponent spotComponent) =>
            {
                bool teleport = false;
                
                if (EntityManager.HasComponent<PlayerComponent>(entity))
                {
                    if (Input.GetKeyDown(KeyCode.LeftArrow)) spotComponent.Velocity += new float3(-0.1f, 0, 0);
                    if (Input.GetKeyDown(KeyCode.RightArrow)) spotComponent.Velocity += new float3(-0.1f, 0, 0);
                    GameManager.Instance.gameObject.transform.position = translation.Value + new float3(0, 0, -7);
                    if (translation.Value.y <= backPoints[2])
                    {
                        teleport = true;
                    }
                }
                if (teleport)
                {
                    float halfDelta = GameManager.Instance.halfDelta;
                    if (translation.Value.y <=
                        backPoints[1] - halfDelta)
                        translation.Value.y += GameManager.Instance.halfDelta * 4;
                    else if (translation.Value.y > backPoints[0] - halfDelta &&
                             translation.Value.y < backPoints[0] + halfDelta)
                    {
                        EntityManager.DestroyEntity(entity);
                    }
                }
            }).Schedule(inputDeps)*/;
            //NativeArray<float> back = GameManager.Instance.backPoints;
            
            
            
            
            
            
            
            
            
            
            /*NativeArray<bool> injectTeleportArray = new NativeArray<bool>(1,Allocator.TempJob);
            injectTeleportArray[0] = teleport;
            var ecbSystem =
                World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            var teleportJob = new TeleportPlayerJob()
            {
                teleportArray = injectTeleportArray,
                EntityHandles = GetEntityTypeHandle(),
                PlayerData = GetComponentDataFromEntity<PlayerComponent>(),
                TranslationHandles = GetComponentTypeHandle<Translation>(),
                SpotHandles = GetComponentTypeHandle<SpotComponent>(),
                ConcurrentCommands = ecbSystem.CreateCommandBuffer().AsParallelWriter(),
                halfDelta = GameManager.Instance.halfDelta
            };
            
            Dependency = teleportJob.Schedule(spotGroup, Dependency);
            Dependency.Complete();
            ecbSystem.AddJobHandleForProducer(Dependency);

            teleport = injectTeleportArray[0];
            injectTeleportArray.Dispose();*/
        }
    }
}