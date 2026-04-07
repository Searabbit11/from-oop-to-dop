using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS_Preview
{
    [BurstCompile]
    public partial struct SpawnerSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = 
                SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (properties, entity) 
                     in SystemAPI.Query<RefRO<SpawnerProperties>>().WithEntityAccess())
            {
                for (int i = 0; i < properties.ValueRO.SpawnCount; i++)
                {
                    Entity instance = ecb.Instantiate(properties.ValueRO.Prefab);
                    
                    // 포지션 셋업
                    float x = (i % 100) * 1.25f;
                    float z = (i / 100) * 1.25f;
                    float3 offsetPosition = new float3(x, 0, z); //float3를 포지션에 매핑하는 느낌으로 사용합니다
                    ecb.SetComponent(
                        instance, 
                        LocalTransform.FromPosition(properties.ValueRO.SpawnPosition + offsetPosition)
                        );
                }
                
                ecb.DestroyEntity(entity);
            }

        }
    }
}