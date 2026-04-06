using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace ECS_Preview
{
    // Authoring 파일에서는 엔티티가 사용할 컴포넌트 데이터와
    // Bake 할 수 있는 Mono 객체에 관한 정의를 작성합니다.

    // 먼저 사용할 데이터 컴포넌트를 정의합니다.
    public struct RotationSpeed : IComponentData
    {
        public float RadiansPerSecond;
    }
    
    // 데이터 컴포넌트를 Bake 하는 로직은 모노 클래스를 상속받아서 처리합니다
    // 해당 개체를 ~Authoring(접미사) 이라고 부릅니다

    // Authoring = 저작
    // 접미사 Authoring은 저작 용도의 표현.
    // ECS 엔티티 생성을 위한 Mono 클래스. 인스펙터와 연결을 해주는 역할.
    // 내부에 선언된 Baker<T> (Inner or 중첩 클래스)를 통해서
    // ECS Baking 프로세스를 동적(Runtime)으로 진행을 한다.
    
    public class RotationAuthoring : MonoBehaviour
    {
        // 인스펙터서 RotationSpeed에 전달할 매개 변수
        public float DegreesPerSecond;

        public class RotationBaker : Baker<RotationAuthoring>
        {
            public override void Bake(RotationAuthoring authoring)
            {
                // ECS 엔티티 객체에 RotationSpeed 데이터 컴포넌트를 추가하고
                // DegreesPerSecond로 초기화 하는 로직

                // GetEntity() : 현재 GameObject에 대응되는 ECS Entity 객체의
                //              ID를 가져오는 함수. (ID가 번호가 아니라 식별자의 의미)
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                
                // AddComponent() = 순수 데이터 구조체를 엔티티에 부착하는 함수
                AddComponent(entity, new RotationSpeed
                {
                    RadiansPerSecond = math.radians(authoring.DegreesPerSecond) 
                });
            }
        }
    }
}