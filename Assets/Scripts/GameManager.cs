using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour,IConvertGameObjectToEntity,IDeclareReferencedPrefabs{

    [ReadOnly] public static GameManager Instance;
    [SerializeField]private GameObject SpotPrefab;
    public Entity spotEntity;
    public float spawnDelay=0.25f;

    public Entity cameraEntity;

    public float halfDelta = 5;
    public float spawnHeight;
    public float deadHeight;
    public float[] backPoints;

    public float3 CameraOffset;
    
    public float CameraDistance;

    public void Awake()
    {
        Instance = this;
        backPoints = new float[4]
        {
            halfDelta * 2, 0.0f,  -halfDelta * 2, halfDelta
        };
        spawnHeight = backPoints[0] + halfDelta*2;
        deadHeight = backPoints[2] - halfDelta*2;
       
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
