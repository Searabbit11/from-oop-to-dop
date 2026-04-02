using UnityEngine;

namespace Study.OOP.Bridge
{

    public interface IEnchantment
    {
        void ApplyEffect();
        string GetEffectName();
    }

    public class FireEnchantment : IEnchantment
    {
        public void ApplyEffect()
        {
            Debug.Log($"<color=red>화염 데미지를 입힙니다!</color>");
        }

        public string GetEffectName() => "Fire";
    }

    public class IceEnchantment : IEnchantment
    {
        public void ApplyEffect()
        {
            Debug.Log($"<color=cyan>이동 속도를 감소 시킵니다!</color>");
        }

        public string GetEffectName() => "Ice";
    }
    
}