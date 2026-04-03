using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Burst;

namespace ECS_Preview
{
    // ECS에서 System은 로직을 담당합니다.
    public partial struct RotationSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            // 특정 개체를 회전 시키는 로직을 작성해봅시다.

            float deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (localTransform,speed) in 
                     SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotationSpeed>>())
            {
                localTransform.ValueRW =
                    localTransform.ValueRO.RotateY(speed.ValueRO.RadiansPerSecond * deltaTime);
            }
        }
    }
}