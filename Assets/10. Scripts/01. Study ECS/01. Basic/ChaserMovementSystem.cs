using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

namespace ECS_Basic
{
    // 체이서의 움직임 로직을 작성 합니다
    [BurstCompile]
    public partial struct ChaserMovementSystem : ISystem
    {
        // System을 상속받은 타입에는 레퍼런스 타입(string 제외)의
        // 필드를 선언하는걸 지양해야 합니다.

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // TagetTag 컴포넌트 데이터를 예시로
            // ECS에서 싱글톤을 활용하는 방법을 알아봅시다.

            // <T> T 타입을 가지고 있는 Entity 자체를 가지고 옵니다.
            if (SystemAPI.TryGetSingletonEntity<TargetTag>
                    (out Entity targetEntity) == false) return;
            
            // 우리는 추적자를 만들고 있습니다. 두가지의 연산이 필요합니다 
            // 1. 위치 연산 (대상과의 거리를 좁히게)
            // 2. 회전 연산 (대상을 바라보게)
            // 모든 Chaser(추적자) 개체들에게 필요한 데이터 필드는
            // TargetTag를 가진 Entity의 LocalTransform 데이터가 필요 합니다.
            
            // Entity를 통해서 해당 Entity가 가지고 있는 컴포넌트를 가져 오는 방법
            LocalTransform targetTransform = 
                SystemAPI.GetComponent<LocalTransform>(targetEntity);
            
            float3 targetPosition = targetTransform.Position;
            float deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (localTransform, 
                         velocity, properties) 
                     in  SystemAPI.Query<RefRW<LocalTransform>, 
                         RefRW<PhysicsVelocity>, RefRO<ChaserProperties>>())
            {
                // LocalTransform, PhysicsVelocity, ChaserProperties 3개의 타입을 모두
                // 가지고 있는 개체를 검색합니다
                
                // 아래의 로직은 한 개체에 적용이 되는 로직입니다.
                
                float3 currentPosition = localTransform.ValueRO.Position;
                float3 directionToTarget = math.normalize(targetPosition - currentPosition);
                // currentPosition에서 target을 바라보는 방향벡터가 결과로 나옵니다

                // 일단 개체의 포워드 벡터를 가지고 옵니다.
                float3 forward = localTransform.ValueRO.Forward();

                float3 crossProduct = math.cross(forward, directionToTarget);
                float dotProduct = math.dot(forward, directionToTarget);
                float turnDirection = math.sign(crossProduct.y);

                // 내적의 결과를 이용해서 forward와 directionToTarget 방향벡터간의
                // 차이를 구해서, 일정이상의 차이가 있다면 회전을 하도록 합니다.
                if (dotProduct < 0.99f)
                {
                    quaternion rotationDelta =
                        quaternion.AxisAngle(math.up(),
                            turnDirection * properties.ValueRO.RotationSpeed * deltaTime);
                    localTransform.ValueRW.Rotation = 
                        math.mul(localTransform.ValueRO.Rotation, rotationDelta); 
                }
                
                // 마지막으로는 이동처리. 가속운동에 방향을 곱해 줍니다.
                // 실질적인 프레임당 처리는 물리엔진이 알아서 합니다.
                velocity.ValueRW.Linear = 
                    forward * properties.ValueRO.MoveSpeed;
            }
        }
    }
}