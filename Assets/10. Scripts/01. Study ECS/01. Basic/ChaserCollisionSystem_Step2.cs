using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Rendering;
using Unity.Transforms;
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
            var transitionLookup = SystemAPI.GetComponentLookup<HitColorTransition>();
            var transformLookup = SystemAPI.GetComponentLookup<LocalTransform>();

            CollisionColorJob colorJob = new CollisionColorJob
            {
                // colorJob이 임무 수행을위해 참고해야할 자료를 주입해줍니다
                ColorLookup = colorLookup,
                TargetLookup = targetLookup,
                TransitionLookup = transitionLookup,
                LocalTransformLookup = transformLookup,
                TimeSeed = (uint)(SystemAPI.Time.ElapsedTime * 1000)
            };

            // 생성한 Job을 처리할수 있도록 예약을 해둡니다.
            JobHandle collisionHandle = colorJob.Schedule(simulation, state.Dependency);
            
            // Interpolation Job을 예약해 줍니다
            ColorInterpolationJob interpolationJob = new ColorInterpolationJob()
            {
                DeltaTime = SystemAPI.Time.DeltaTime
            };
            
            state.Dependency = interpolationJob.ScheduleParallel(collisionHandle);
        }
    }
    
    
    // 단계별 구현은 Step1 가서 보세요
    // Step1 상태에서 컬러 보간을 이용해서 색을 빼는 기능을 추가해야 한다고 시나리오를 설정해보겠습니다
    [BurstCompile]
    public struct CollisionColorJob : ITriggerEventsJob
    {
        public ComponentLookup<URPMaterialPropertyBaseColor> ColorLookup;
        public ComponentLookup<TargetTag> TargetLookup;
        
        // HitColorTransition을 찾을 lookup
        public ComponentLookup<HitColorTransition> TransitionLookup;
        public ComponentLookup<LocalTransform> LocalTransformLookup;

        public float TimeSeed;
        
        [BurstCompile]
        public void Execute(TriggerEvent triggerEvent)
        {
            Entity entityA = triggerEvent.EntityA;
            Entity entityB = triggerEvent.EntityB;
            
            if (TargetLookup.HasComponent(entityA) && ColorLookup.HasComponent(entityB))
            {
                ApplyCollisionHit(entityB);
                TeleportTarget(entityA);
            }
            else if (TargetLookup.HasComponent(entityB) && ColorLookup.HasComponent(entityA))
            {
                ApplyCollisionHit(entityA);
                TeleportTarget(entityB);
            }
        }

        [BurstCompile]
        private void ApplyCollisionHit(Entity entity)
        {
            ColorLookup[entity] = 
                new URPMaterialPropertyBaseColor { Value = new float4(1, 0, 0,1) };
            
            // ColorInterpolationJob을 설정해 줍니다.
            if (TransitionLookup.HasComponent(entity))
            {
                TransitionLookup[entity] = new HitColorTransition
                {
                    Timer = 1.0f,
                    IsActive = true,
                };
            }
        }
        
        // Job 안에서 난수를 생성하는것을 짧게 배워봅시다.
        // 충돌이 일어나면 TargetTag를 가진 개체를 랜덤한 범위로 텔레포트를 시켜 봅시다
        [BurstCompile]
        private void TeleportTarget(Entity entity)
        {
            uint seed = (uint)(entity.Index + TimeSeed);
            var random = new Unity.Mathematics.Random(seed); // UnityEngine.Random()을 사용할수가 없습니다 
            
            // TargetTag 엔티티의 x, z값만 랜덤으로 바꾸겠습니다
            float randomX = random.NextFloat(0, 50f);
            float randomZ = random.NextFloat(0, 50f);

            // TargetTag의 LocalTransform 컴포넌트를 가져 옵니다
            LocalTransform transform = LocalTransformLookup[entity];
            transform.Position = new float3(randomX, 0, randomZ);
            LocalTransformLookup[entity] = transform;
        }
        
    }

    [BurstCompile]
    public partial struct ColorInterpolationJob : IJobEntity
    {
        public float DeltaTime; // 생성자를 통해서 주입받을겁니다

        [BurstCompile]
        public void Execute(ref URPMaterialPropertyBaseColor color, ref HitColorTransition transition)
        {
            if (transition.IsActive == false) return;

            transition.Timer -= DeltaTime;

            if (transition.Timer <= 0.0f) // Interpolation이 종료된 경우
            {
                transition.IsActive = false;
                transition.Timer = 0.0f;
                color.Value = new float4(1, 1, 1, 1);
            }
            else // Interpolation을 진행해야 할 경우
            {
                // Interpolation 시간은 1초로 설정하겠습니다
                float t = 1.0f - transition.Timer;
                float4 startColor = new float4(1, 0, 0, 1); //스타트 컬러는 red부터 시작
                float4 endColor = new float4(1, 1, 1, 1);
                
                color.Value = math.lerp(startColor, endColor, t);
            }
        }

    }
    
}