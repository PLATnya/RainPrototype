using Rain.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;


namespace Rain.Systems
{
    public class SpotTeleportSystem : SystemBase
    {

        private bool _teleport;

        protected override void OnUpdate()
        {
            //game field limit and spawn points to native array, prepeared for job injection
            NativeArray<float> checkingPointsArray =
                new NativeArray<float>(GameManager.Instance.backPoints, Allocator.TempJob);

            //the boolean meaning of the need for a teleport in the native array, prepeared for job injection
            NativeArray<bool> teleportArray = new NativeArray<bool>(1, Allocator.TempJob) {[0] = _teleport};

            var ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            var cmb = ecbSystem.CreateCommandBuffer();
            var playerData = GetComponentDataFromEntity<PlayerComponent>();
            var teleportJob = Entities.WithAll<SpotComponent>().ForEach(
                (int entityInQueryIndex, Entity entity, ref Translation translation, ref SpotComponent spotComponent) =>
                {
                    if (playerData.HasComponent(entity))
                    {
                        if (!teleportArray[0])
                        {
                            if (translation.Value.y <= checkingPointsArray[2])
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
                        float halfDelta = checkingPointsArray[3];
                        if (translation.Value.y <=
                            checkingPointsArray[1] - halfDelta)
                            translation.Value.y += halfDelta * 4;
                        else if (translation.Value.y > checkingPointsArray[0] - halfDelta &&
                                 translation.Value.y < checkingPointsArray[0] + halfDelta)
                        {
                            cmb.DestroyEntity(entity);
                        }
                    }
                }).WithDisposeOnCompletion(checkingPointsArray).WithBurst().Schedule(Dependency);
            Dependency = teleportJob;
            ecbSystem.AddJobHandleForProducer(Dependency);
            teleportJob.Complete();
            _teleport = teleportArray[0];
            teleportArray.Dispose();
        }
    }
}