using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
    
public class GameManager : MonoBehaviour,IConvertGameObjectToEntity,IDeclareReferencedPrefabs{

    public static GameManager Instance;
    [SerializeField]private GameObject SpotPrefab;
    public Entity spotEntity;
    public float spawnDelay=0.5f;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        Instance = this;
        spotEntity = conversionSystem.GetPrimaryEntity(SpotPrefab);
    }
    
  
    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(SpotPrefab);
    }
}
