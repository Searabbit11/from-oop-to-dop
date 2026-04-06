using System;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Burst;
using UnityEngine;

namespace ECS_Preview
{
    // ECS에서 System은 로직을 담당합니다.
    
    // BurstCompile? : 컴파일러에게 힌트를 주는 키워드.
    // 컴파일러에게 특정 코드 블록을 고도로 최적화된
    // 네이티브 기계 코드로 컴파일하도록 지시하는 C# 어트리뷰트
    // 해당 블록안에 참조 타입 객체들(힙메모리를 사용하는 것들)이 있으면
    // 컴파일이 안될수도 있음.
    // IJob, IJobEntity, IJobChunk 등을 구현하는 구조체, 또는 정적 메서드에 사용됨
    
    [BurstCompile]
    public partial struct RotationSystem : ISystem
    {
        // C#의 한정자 (ref, out, in)
        
        // ref?
        // 변수의 실제 데이터 값(Value)을 복사하는것 대신
        // 해당 데이터가 위치한 메모리 주소(Reference) 자체를 전달하거나 반환 할 때 사용하는 한정자
        // 넘겨줄때 초기화가 되어 있어야 합니다
        // (이상한 메모리 주소를 넘길수가 없다! 실제 할당이 되어있어야 한다. 근데 보통 Value 타입은 자동할당이 되어있음) 
        // 양방향으로 데이터 전달 및 원본 변수를 직접 수정하기 위해 사용을 합니다.
        
        // C#에서 Reference Type의 객체는 거의 쓸일이 없음. 메모리적 스왑? 이런거?
        // 거의다 Value Type에 사용한다 라고 생각하시면 됨
        
        // out
        // 호출된 메서드에서 데이터를 외부로 내보내기 위해 사용하는 한정자 입니다.
        // 메서드에서 여러개의 결과값을 반환해야할 때 사용합니다.

        // in
        // 매개변수를 참조로 전달을 하되, 메서드 내부에서 값을 수정할 수 없도록
        // 강제하는 한정자 입니다. 읽기전용으로 사용이 됩니다.
        // 큰 구조체의 복사 비용을 업애면서, 원본 수정을 방지해야할 때 사용합니다.
        // readonly ref 라고 생각하시면됩니다.
        // PS : foreach의 in과는 다른 개념입니다. foreach의 in은 "Collection 내부의" 의미가 맞습니다 
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // 특정 개체를 회전 시키는 로직을 작성해봅시다.
            float deltaTime = SystemAPI.Time.DeltaTime;

            // Tuple?
            // 튜플은 여러개의 데이터 요소들을 하나의 그룹화 하는 기능을 합니다.
            // 클래스나 구조체를 복잡하게 새로 정의할 필요 없이 관련된 데이터를
            // 일시적으로 그룹화 할때 사용합니다.
            // 값 타입입니다. 동등비교가 가능하다
            
            // Tuple ex)            
            // (int BirthYear, string Name) developerInfoA = (1989, "KooHoo");
            // var developerInfoB = (BirthYear : 1989, Name : "KooHoo"); // => 제일 많이 씀
            // (int, string) developerInfoC = (1989, "KooHoo"); // => 이거 잘 안씀
            //
            // Console.WriteLine(developerInfoB.BirthYear);
            // Console.WriteLine(developerInfoB.Name);
            //
            // // 동등 비교
            // bool isSame = (developerInfoC == developerInfoB);
            
            // ECS에서의 foreach문은 컴파일 할때 for문으로 변환됩니다.
            foreach (var (localTransform, speed) 
                     in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotationSpeed>>())
                        // LocalTransform 타입, RotationSpeed 타입 두 타입을 모두 가지고 있는 
                        // 개체 전부 연산을 진행합니다.
            {
                localTransform.ValueRW = 
                    localTransform.ValueRO.RotateY(speed.ValueRO.RadiansPerSecond * deltaTime);
            }
            
            // RefRW?
            // Read-Write가 가능한 Struct의 reference.
            // RW로 특정 타입(IComponentData)을 읽으면 읽기와 쓰기가 가능해 진다.
            
            // RefRO?
            // ReadOnly. 오직 읽기전용으만 활용할 수 있는 Struct의 reference.
            // RO로 특정 타입(IComponentData)을 읽으면 오직 읽기만 가능하다.
            
            // PS : ReadOnly형태로 메모리 주소를 읽으면 더 빠름.
            // Write가 편하다고 난사해서 사용하게 되면 Job시스템의 퍼포먼스가 결국 떨어집니다. 
            
        }
    }
}