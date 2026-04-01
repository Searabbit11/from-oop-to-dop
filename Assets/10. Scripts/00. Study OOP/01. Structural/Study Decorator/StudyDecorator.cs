using System;
using System.Collections.Generic;
using UnityEngine;

namespace Study.OOP._01._Structural.Study_Decorator
{
    // Decorator Pattern
    // 객체의 결합을 통해 기능을 런타임에 동적으로 그리고 유연하게
    // 확장할 수 있게 해주는 구조 패턴의 한 종류.
    
    // 타깃 객체를 수정하지 않고, 그 객체를 감싸는 장식자(데코)객체를
    // 만들어서 덧붙이는 방식으로 새로운 행동이나 상태를 추가합니다.
    
    // 컴파일 타임에 결정되는 상속과는 달리 게임 도중에 언제든지
    // 장식을 추가(버프 적용)하거나 벗겨(버프 해제)낼 수 있습니다.
    
    // - 캐릭터의 스탯
    // - 스킬의 강화나 동적 기능의 추가.
    //      - 파이어볼이 여러 갈래로 갈린다던가
    //      - 파이어볼이 유도탄이 된다던가 하는
    
    public class StudyDecorator : MonoBehaviour
    {
        private IStat unitStat;

        private void Awake()
        {
            unitStat = new BaseStat(100, 10, 13.0f);
            PrintStat("초기 상태");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                unitStat = new AttackBoost(unitStat, 5);
                PrintStat("공격력 강화 적용!");
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                unitStat = new AttackDecrease(unitStat, 5);
                PrintStat("공격력 감소 적용!");
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                unitStat = new HpBoost(unitStat, 10);
                PrintStat("체력 강화 적용!");
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                unitStat = new HpDecrease(unitStat, 10);
                PrintStat("체력 감소 적용!");
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                unitStat = new SpeedBoost(unitStat, 5);
                PrintStat("속도 증가 적용!");
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                unitStat = new SpeedDecrease(unitStat, 10);
                PrintStat("속도 감소 적용!");
            }

            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                unitStat = new GoldBoost(unitStat, 100, 30.0f);
                PrintStat("황금 증강 적용!");
            }
        }
        
        private void PrintStat(string stateLabel)
        {
            string desc = string.Join(" > ", unitStat.Descriptions);
            Debug.Log($"[{stateLabel}] {desc} | HP: {unitStat.Hp}, ATK: {unitStat.Attack}");
        }
    }
}