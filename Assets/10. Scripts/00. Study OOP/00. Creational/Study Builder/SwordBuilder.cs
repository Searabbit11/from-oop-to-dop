using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Study.OOP.Builder
{
    public class SwordBuilder
    {
        string[] normalSwordNames = new string[]
        {
            "Rusty Sword",
            "Worn Blade",
            "Iron Shortsword",
            "Bronze Saber",
            "Steel Sword",
            "Old Longsword",
            "Knight's Training Sword",
            "Footman's Blade",
            "Balanced Sword",
            "Plain Broadsword"
        };

        string[] magicSwordNames = new string[]
        {
            "Fiery Longsword",
            "Glacial Blade",
            "Shocking Saber",
            "Venomfang Sword",
            "Cruel Edge",
            "Swift Rapier",
            "Burning Broadsword",
            "Icy Cutlass",
            "Deadly Shortsword",
            "Mystic Sword"
        };

        string[] rareSwordNames = new string[]
        {
            "Vicious Flaming Saber",
            "Glorious Frosted Blade",
            "Savage Thunder Edge",
            "Ancient Venomous Sword",
            "Cursed Knight's Longsword",
            "Brutal Crimson Broadsword",
            "Swift Shadowfang Saber",
            "Blazing Storm Cutlass",
            "Piercing Glacial Rapier",
            "Ruthless Ember Sword"
        };

        string[] uniqueSwordNames = new string[]
        {
            "Doomfang",
            "Nightbringer",
            "Flametongue",
            "Frostmourne",
            "Stormreaver",
            "Shadowrend",
            "Bloodletter",
            "Ashbringer",
            "Dreadfang",
            "Blade of the First King"
        };

        private string[] weaponOptions = new string[]
        {
            // 기본 공격 관련 옵션
            "+10 to Maximum Damage",
            "+5 to Minimum Damage",
            "+20% Increased Attack Speed",
            "+5% Critical Hit Chance",
            "+50% Critical Damage",
            "Adds 10–20 Physical Damage",
            "+150 to Attack Rating",

            // 원소 속성 부여
            "Adds 15–30 Fire Damage",
            "Adds 10–20 Cold Damage and Slows Target by 10%",
            "Adds 1–100 Lightning Damage",
            "Adds 30 Poison Damage over 5 seconds",
            "+20% Fire Damage",

            // 상태 이상 및 특수 효과
            "10% Chance to cause Bleed on Hit",
            "5% Chance to Stun",
            "+5 Life on Hit",
            "+3 Mana after each Kill",
            "10% Chance to lower enemy Fire Resistance",

            // 유틸성 및 자원 관련 옵션
            "4% Life Leech",
            "3% Mana Leech",
            "+10% Resource Gain",
            "-10% Skill Cooldown",
            "-5 Mana Cost to Skills",

            // 스킬 관련 옵션
            "+2 to Whirlwind",
            "+1 to All Skills",
            "+25% Minion Damage",
            "+3 to Fire Skills",

            // 조건부 및 고유 효과
            "+50% Damage to Low Life Enemies",
            "+15% Crit Chance when at Full Life",
            "+40% Damage against Stunned Enemies",
            "Cast Frost Nova when switching weapon",
            "5% Chance to Cast Chain Lightning on Hit",

            // 확률 기반 효과
            "5% Chance to Cast Fireball on Hit",
            "On Crit: Gain 20% Attack Speed for 3s",
            "10% Chance to Apply Bleed",

            // 마법부여 및 룬워드 스타일
            "Runeword: 'Fury' grants +40% IAS, +2 to Feral Rage",
            "+2 to Paladin Offensive Auras",
            "Ancient Weapon: +100% Base Damage"
        };

        
        private Sword sword;

        public int AttackValue => sword.AttackValue;

        public SwordBuilder()
        {
            sword = new Sword();
            sword.Name = "미완성의 칼";
            sword.AttackValue = 1;
            sword.Type = ElementType.None;
            sword.Grade = Grade.Normal;
            sword.Options = new List<string>();
        }

        public Sword CreateSword()
        {
            return sword;
        }
        
        // Builder Pattern을 사용할때는 만들고자 하는 객체의 필드에
        // 1:1 대응되도록 빌더 함수를 디자인하는게 핵심입니다.

        public SwordBuilder SetRandomGrade()
        {
            Grade randomGrade = (Grade)UnityEngine.Random.Range(0, 4);
            return SetGrade(randomGrade);;
        }
        
        public SwordBuilder SetGrade(Grade grade)
        {
            sword.Grade = grade;
            return this;
        }
        
        public SwordBuilder SetName()
        {
            string[] targetNames;

            switch (sword.Grade)
            {
                case Grade.Normal:
                    targetNames = normalSwordNames;
                    break;
                case Grade.Magic:
                    targetNames = magicSwordNames;
                    break;
                case Grade.Rare:
                    targetNames = rareSwordNames;
                    break;
                case Grade.Unique:
                    targetNames = uniqueSwordNames;
                    break;
                default:
                    targetNames = normalSwordNames;
                    break;
            }
            
            int randomIndex = UnityEngine.Random.Range(0, targetNames.Length);
            sword.Name = targetNames[randomIndex];
            return this;
        }

        public SwordBuilder SetRandomAttackValue()
        {
            sword.AttackValue = Random.Range(0, 10000);
            return this;
        }
        
        public SwordBuilder AddAttackValue()
        {
            sword.AttackValue += 500;
            return this;
        }
        
        public SwordBuilder SetRandomElementType()
        {
            sword.Type = (ElementType)Random.Range(0, 4);
            return this;
        }
        
        public SwordBuilder SetRandomRandomOption()
        {
            int randomIndex = Random.Range(0, weaponOptions.Length);
            sword.Options.Add(weaponOptions[randomIndex]);
            return this;
        }

        public SwordBuilder SetSword(Sword sword)
        {
            this.sword = sword;
            return this;
        }
    }
}