using Rain.Components;

using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateBefore(typeof(StepPhysicsWorld))]
public class CollisionSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem ecbSystem;
    StepPhysicsWorld m_StepPhysicsWorld;
    protected override void OnCreate()
    {
        m_StepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
        ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    struct CollisionJob:ICollisionEventsJob
    {
        public EntityCommandBuffer commandBuffer;
        public ComponentDataFromEntity<PlayerComponent> playerData;
        public void Execute(CollisionEvent collisionEvent)
        {
            
            bool IsAPlayer = playerData.HasComponent(collisionEvent.EntityA);
            bool IsBPlayer = playerData.HasComponent(collisionEvent.EntityB);
            Entity playerEntity = Entity.Null;
            if (IsAPlayer || IsBPlayer)
            {
                playerEntity = IsAPlayer ? collisionEvent.EntityA : collisionEvent.EntityB;
                commandBuffer.DestroyEntity(playerEntity);
            }
            
        }
    }
    protected override void OnUpdate()
    {
        SimulationCallbacks.Callback testCollisionEventsCallback =
            (ref ISimulation simulation, ref PhysicsWorld world, JobHandle inDeps) =>
            {
                return new CollisionJob
                {
                    commandBuffer = ecbSystem.CreateCommandBuffer(),
                    playerData = GetComponentDataFromEntity<PlayerComponent>()
                }.Schedule(simulation, ref world, inDeps);
            };
        m_StepPhysicsWorld.EnqueueCallback(SimulationCallbacks.Phase.PostSolveJacobians, testCollisionEventsCallback, Dependency);
    }
}
