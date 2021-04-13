using Rain.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;

public class SpotTeleportSystem : SystemBase
{

    private bool teleport;
    protected override void OnUpdate()
    {
        NativeArray<float> checkPointsArray = new NativeArray<float>(GameManager.Instance.backPoints, Allocator.TempJob);
        
        /*NativeArray<float> backPoints =new NativeArray<float>(4, Allocator.TempJob) ;
        backPoints[0] = 10;
        backPoints[1] = 0;
        backPoints[2] = -10;
        backPoints[3] = GameManager.Instance.halfDelta;*/

        NativeArray<bool> teleportArray = new NativeArray<bool>(1, Allocator.TempJob);
        teleportArray[0] = teleport;
        var ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        var cmb = ecbSystem.CreateCommandBuffer();
        var playerData = GetComponentDataFromEntity<PlayerComponent>();
        var teleportJob = Entities.WithAll<SpotComponent>().ForEach(
            (int entityInQueryIndex,Entity entity, ref Translation translation, ref SpotComponent spotComponent) =>
            {
                if (playerData.HasComponent(entity))
                {
                    if (!teleportArray[0])
                    {
                        if (translation.Value.y <= checkPointsArray[2])
                        {
                            teleportArray[0] = true;
                        }
                    }
                    else
                    {
                        teleportArray[0] = false;
                    }
                }
                if (teleportArray[0])
                {
                    float halfDelta = checkPointsArray[3];
                    if (translation.Value.y <=
                        checkPointsArray[1] - halfDelta)
                        translation.Value.y += halfDelta * 4;
                    else if (translation.Value.y > checkPointsArray[0] - halfDelta &&
                             translation.Value.y < checkPointsArray[0] + halfDelta)
                    {
                        cmb.DestroyEntity(entity);
                    }
                }
            }).WithDisposeOnCompletion(checkPointsArray).WithBurst().Schedule(Dependency);
        Dependency = teleportJob;
        ecbSystem.AddJobHandleForProducer(Dependency);
        teleportJob.Complete();
        teleport = teleportArray[0];
        teleportArray.Dispose();
    }
}