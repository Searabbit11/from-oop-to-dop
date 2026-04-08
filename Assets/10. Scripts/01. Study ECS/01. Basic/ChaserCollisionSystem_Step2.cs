using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Rendering;
using UnityEngine.Rendering.Universal;

namespace ECS_Basic
{
    [BurstCompile]
    public partial struct ChaserCollisionSystem_Step1 : ISystem
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
            
            // SystemAPI.GetComponentLookup<T>() : T 컴포넌트를 식별할수있는 해시테이블을 요청합니다 
            var colorLookup = SystemAPI.GetComponentLookup<URPMaterialPropertyBaseColor>();
            var targetLookup = SystemAPI.GetComponentLookup<TargetTag>();

            CollisionColorJob colorJob = new CollisionColorJob
            {
                // colorJob이 임무 수행을위해 참고해야할 자료를 주입해줍니다
                ColorLookup = colorLookup,
                TargetLookup = targetLookup, 
            };

            // 생성한 Job을 처리할수 있도록 예약을 해둡니다.
            state.Dependency = colorJob.Schedule(simulation, state.Dependency);
        }
    }
    
    // 단계별 구현은 Step1 가서 보시오
    // Step1 상태에서 컬러 보간을 이용해서 색을 빼는 기능을 추가해야 한다고 시나리오를 설정해보겠습니다
    [BurstCompile]
    public struct CollisionColorJob : ITriggerEventsJob
    {
        // URPMaterialPropertyBaseColor 컴포넌트를 찾기 위한 해시 태이블.
        // ComponentLookup은 보통 단일개체를 검색할때 사용합니다
        public ComponentLookup<URPMaterialPropertyBaseColor> ColorLookup;
        public ComponentLookup<TargetTag> TargetLookup; 
        // 사실 TargetTag가 싱글톤이라서 Entity 형태로 주입해도 됩니다
        // Ex) public Entity Target;
        
        // "triggerEvent가 발생했을때 수행해야할 함수다"라고 생각하신면 됩니다
        public void Execute(TriggerEvent triggerEvent)
        {
            Entity entityA = triggerEvent.EntityA;
            Entity entityB = triggerEvent.EntityB;
            
            // 내가 생성자를 통해 전달받은 lookUp에 해당 entity가 포함되는지를 검사합니다.
            // entityA가 TargetTag를 가지고 있는지 TargetLookup에게 물어봅니다 && 
            // entityB가 URPMaterialPropertyBaseColor를 가지고 있는지 ColorLookup에게 물어봅니다
            if (TargetLookup.HasComponent(entityA) && ColorLookup.HasComponent(entityB))
            {
                // 충돌이 일어났으면, 색을 변경해줍니다
                ColorLookup[entityB] = 
                    new URPMaterialPropertyBaseColor { Value = new float4(1, 0, 0,1) };
            }
            // 이제부터는 양방향 검사를 해야합니다.
            else if (TargetLookup.HasComponent(entityB) && ColorLookup.HasComponent(entityA))
            {
                ColorLookup[entityA] = 
                    new URPMaterialPropertyBaseColor { Value = new float4(1, 0, 0,1) };
            }
        }
    }

    public partial struct ColorInterpolationJob : IJobEntity
    {
        public float DeltaTime; // 생성자를 통해서 주입받을 것 

        public void Execute(ref URPMaterialPropertyBaseColor color,  ref HitColorTransition transition)
        {
            if (transition.IsActive == false) return;

            transition.Timer -= DeltaTime;
            
            if (transition.Timer <= 0.0f) //컬러 전환 트랜지션이 종료가 되었다면
            {
                transition.IsActive = false;
                transition.Timer = 0.0f;
                color.Value = new float4(1,1,1,1);
            }
            else // Interpolation을 진행해야 할 경우
            {
                // Interpolation 시간은 1초로 설정하겠습니다.
                float t = 1.0f - transition.Timer;
                float4 startColor = new float4(0, 1, 0, 1);
                float4 endColor = new float4(1, 1, 1, 1);
                color.Value = math.lerp(startColor, endColor, t);
            }
        }
    }
}