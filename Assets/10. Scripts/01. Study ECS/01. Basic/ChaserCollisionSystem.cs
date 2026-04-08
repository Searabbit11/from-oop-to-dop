using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Rendering;
using UnityEngine.Rendering.Universal;

namespace ECS_Basic
{
    [BurstCompile]
    public partial struct ChaserCollisionSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            // App의 수명 주기 동안(게임일수도 있고, ECS 시스템) 유지가 되어야 하는
            // 메모리를 예약하는 단계 입니다. (데이터가 아님에 유의)
            // 매 프레임 발생하는 동적할당 등의 작업을 방지하기 위해
            // 무거운 작업들이 OnCreate 함수에서 정의되고 수행이 됩니다.
            
            // 시스템의 상태 정의나 특정 메모리 공간등을 확보 예약하는데에 씀.
            // ex) 쿼리빌더 같은것들
            
            state.RequireForUpdate<SimulationSingleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var simulation = SystemAPI.GetSingleton<SimulationSingleton>();

            var chaserLookup = SystemAPI.GetComponentLookup<ChaserProperties>();
            var targetLookup = SystemAPI.GetComponentLookup<TargetTag>();
            
            var job = new CollisionColorJob
            {
                ChaserLookup =  chaserLookup,
                TargetLookup =  targetLookup,
            };
            
            state.Dependency = job.Schedule(simulation, state.Dependency);
        }
    }
    
    // Target과 충돌했을 때 Chaser의 Color를 변경해 봅시다
    // 1. Target과 Chaser의 충돌을 검사하고 싶다면
    // 2. TargetTag와 URPMaterialPropertyBaseColor 두가지의 컴포넌트 데이터
    // 3. 해당 컴포넌트를 기반으로 만족하는 Entity를 찾아냅니다
    // 4. void Execute(TriggerEvent triggerEvent)의 triggerEvent로 전달된 Entity들의 검사를 진행합니다.
    [BurstCompile]
    public struct CollisionColorJob : ITriggerEventsJob
    {
        public ComponentLookup<ChaserProperties> ChaserLookup;
        public ComponentLookup<TargetTag> TargetLookup;
        
        // "triggerEvent가 발생했을때 수행해야할 함수다"라고 생각하신면 됩니다
        public void Execute(TriggerEvent triggerEvent)
        {
            Entity entityA = triggerEvent.EntityA;
            Entity entityB = triggerEvent.EntityB;
            
            //이제부터는 양방향 검사를 해야합니다.

            if (TargetLookup.HasComponent(entityA) && ColorLookup.HasComponent(entityB))
            {
               
            }
            else if (ChaserLookup.HasComponent(entityB) && TargetLookup.HasComponent(entityA))
            {
                // 충돌판정 2
            }
            
        }
    }
}