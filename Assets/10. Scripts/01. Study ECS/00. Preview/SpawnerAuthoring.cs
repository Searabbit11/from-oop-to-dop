using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ECS_Preview
{
    //어떤것을, 얼마나 생성할지에 대한 정보를 정의합니다.
    public struct SpawnerProperties : IComponentData
    {
        public Entity Prefab;
        public int SpawnCount;
        // float3는 3개의 float를 저장하는 타입입니다. 여기서는 x,y,z가 대입됩니다.
        public float3 SpawnPosition;  
    }
    
    public class SpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public int SpawnCount;
        // SpawnPosition은 동적으로 변경하면서 대입해줄겁니다.
        
        public class SpawnerBaker : Baker<SpawnerAuthoring>
        {
            public override void Bake(SpawnerAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.None);

                SpawnerProperties spawnerProperties;
                spawnerProperties.Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic);
                spawnerProperties.SpawnCount = authoring.SpawnCount;
                spawnerProperties.SpawnPosition = authoring.transform.position;
                
                AddComponent(entity, spawnerProperties);
            }
        }
        
    }
}