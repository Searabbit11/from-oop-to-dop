using System.Collections.Generic;
using UnityEngine;

namespace Study.OOP.Bridge
{
    public abstract class Weapon
    {
        public int AtkValue;
        protected List<IEnchantment> enchantmentList = new List<IEnchantment>();

        // 내부 구현은 상속받는 대상이 하는걸로~
        public abstract void Attack();
        public abstract void AddEnchantment(IEnchantment enchantment);

        protected void ApplyEffects()
        {
            for (int i = 0; i < enchantmentList.Count; i++)
            {
                enchantmentList[i].ApplyEffect();
            }
        }
    }

    public class Sword : Weapon
    {
        public Sword(int atkValue)
        {
            atkValue = atkValue;
        }
        
        public override void Attack()
        {
            Debug.Log($"[검] : 베기 공격");
            ApplyEffects();
        }

        public override void AddEnchantment(IEnchantment enchantment)
        {
            enchantmentList.Add(enchantment);
        }
    }

    public class Bow : Weapon
    {
        public Bow(int atkValue)
        {
            AtkValue = atkValue;
        }
        
        public override void Attack()
        {
            Debug.Log($"[활] : 활쏘기 공격");
            ApplyEffects();
        }

        public override void AddEnchantment(IEnchantment enchantment)
        {
            enchantmentList.Add(enchantment);
        }
    }

    public class Dagger : Weapon
    {
        public override void Attack()
        {
            Debug.Log($"[단검] : 단검 공격");
        }

        public override void AddEnchantment(IEnchantment enchantment)
        {
            Debug.Log($"[단검] : 단검에는 속성 추가 불가능합니다");
        }
    }
}