using Unity.Entities;
using UnityEngine;

namespace ECS_Basic
{
    /// <summary>
    /// 특정 Entity를 식별하는데 사용되는 ComponentData  
    /// </summary>
    public struct TargetTag : IComponentData { }

    public class TargetAuthoring : MonoBehaviour
    {
        public class TargetBaker : Baker<TargetAuthoring>
        {
            public override void Bake(TargetAuthoring authoring)
            {
                // 1. 먼저 엔티티를 가져오고
                // 2. 컴포넌트 데이터를 추가해준다.
                
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new TargetTag());
            }
        }
    }

}