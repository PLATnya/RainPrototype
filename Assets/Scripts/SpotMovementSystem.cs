using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
public class SpotMovementSystem : ComponentSystem
{
    
    private Random random;
    private int startCount = 5;
    private float timeDelay;
    protected override void OnCreate()
    {
        random = new Random((uint)GetHashCode());
        
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
            SpawnSpot(5.0f);
            timeDelay = GameManager.Instance.spawnDelay;
        }
        Entities.WithAll<SpotComponent>().ForEach((Entity spot,ref Translation translation, ref SpotComponent spotComponent) =>
        {
            translation.Value += spotComponent.Velocity;
            if (translation.Value.y < -5.0f)
            {
                World.EntityManager.DestroyEntity(spot);
            }
        });

    }

    void SpawnSpot(float y)
    {
        
        Entity spawnedSpot = World.EntityManager.Instantiate(GameManager.Instance.spotEntity);
        World.EntityManager.SetComponentData(spawnedSpot,new Translation {
            Value = new float3(random.NextFloat(-5,5),y,0)
        });
        World.EntityManager.AddComponentData(spawnedSpot, new SpotComponent
        {
            Velocity = new float3(0, random.NextFloat(-0.1f,0), 0)
        });
    }
    
}
