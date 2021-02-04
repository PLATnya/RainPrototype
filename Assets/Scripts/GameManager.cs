using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
    
public class GameManager : MonoBehaviour,IConvertGameObjectToEntity,IDeclareReferencedPrefabs{

    [ReadOnly] public static GameManager Instance;
    [SerializeField]private GameObject SpotPrefab;
    public Entity spotEntity;
    public float spawnDelay=0.25f;

    public Entity cameraEntity;

    public float halfDelta = 5;
    public float spawnHeight;
    public float deadHeight;
    public NativeArray<float> backPoints;

    public void Awake()
    {
        Instance = this;
        backPoints = new NativeArray<float>(3,Allocator.Temp);
        backPoints[0] = halfDelta * 2;
        backPoints[1] = 0.0f;
        backPoints[2] = -halfDelta * 2;
        spawnHeight = backPoints[0] + halfDelta;
        deadHeight = backPoints[2] - halfDelta;
        backPoints.Dispose();   
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        
        spotEntity = conversionSystem.GetPrimaryEntity(SpotPrefab);
        cameraEntity = entity;
    }
    
    
    
    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(SpotPrefab);
    }
}
