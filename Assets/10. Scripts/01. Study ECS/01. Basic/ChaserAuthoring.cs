using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;

namespace ECS_Basic
{
    // 제가 쓰고 싶은 필드는 두가지 입니다.
    // float MoveSpeed, float RotationSpeed
    
    // 추적자는 이동속도와
    // 회전 속도를 가지고 있다
    public struct ChaserProperties : IComponentData
    {
        public float MoveSpeed;
        public float RotationSpeed;
    }

    public class ChaserAuthoring : MonoBehaviour
    {
        // 현재는 인스펙터 이지만
        // 데이터를 주입을 해야하는 경우(테이블 데이터 -> 객체의 필드로)
        // Authoring에서 처리를 하시면 됩니다.
        public float MoveSpeed;
        public float RotationSpeed;
        
        public class ChaserBaker : Baker<ChaserAuthoring>
        {
            public override void Bake(ChaserAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new ChaserProperties
                {
                    MoveSpeed = authoring.MoveSpeed,
                    RotationSpeed =  authoring.RotationSpeed
                });
                
                AddComponent(entity, new URPMaterialPropertyBaseColor
                {
                    // 여기서 Value는 RGBA 컬러(1,1,1,1)를 의미합니다.
                    Value = new float4(1,1,1,1) // 흰색을 의미합니다.
                });
            }
        }
    }
    
}
