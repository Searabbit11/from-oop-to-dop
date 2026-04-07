using Unity.Burst;
using Unity.Entities;

namespace ECS_Basic
{
    // 체이서의 움직임 로직을 작성 합니다
    [BurstCompile]
    public partial struct ChaserMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            
        }
    }
}