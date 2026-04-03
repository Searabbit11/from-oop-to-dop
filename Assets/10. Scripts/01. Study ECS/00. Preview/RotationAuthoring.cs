using Unity.Entities;
using UnityEngine;
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
    // 해당 개체를 ~Authoring(접미사) 이라고 부릅니다.

    public class RotationAuthoring : MonoBehaviour
    {
        // 인스펙터서 RotationSpeed에 전달할 매개변수
        public float DegreesPerSecond;
        
        public class RotationBaker : Baker<RotationAuthoring>
        {
            public override void Bake(RotationAuthoring authoring)
            {
                // ECS 엔티티 객체에 RotationSpeed 데이터 컴포넌트를 추가하고
                // DegreesPerSecond로 초기화 하는 로직
                
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity,new RotationSpeed
                {
                    RadiansPerSecond = math.radians(authoring.DegreesPerSecond)
                });
            }
        }
    }

}