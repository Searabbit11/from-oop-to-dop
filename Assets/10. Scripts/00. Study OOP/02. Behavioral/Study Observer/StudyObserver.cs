using System;
using UnityEngine;

namespace Study.OOP._02._Behavioral
{
    // Observer Pattern
    // 어떤 객체의 상태가 변할 때, 그 객체에 관심이 있는(구독 중인) 다른 여러 객체들에게
    // 상태 변화를 자동으로 알려주는 1:N 구조를 갖는 행위 패턴 종류 중 하나입니다.
    
    // 중요한것은 관찰을 하는 객체를 설계하는 것입니다.
    
    // - 유튜버 <-> 유튜브 시스템 <-> 유저 의 관계
    // - 특정 스탯이 변경되었을때 UI에게 알림을 보내는 것
    // - LevelingSystem에서 Player의 레벨 변화를 체크하고 관련 객체들에게 알림을 보내는 것
    
    public class StudyObserver : MonoBehaviour
    {
        private ObservableValue<float> hp = new ObservableValue<float>(100);

        private Leveling levelingSystem = new Leveling();
        
        private void Start()
        {
            hp.OnValueChanged += value => { Debug.Log($"hp가 {value}로 바뀌었습니다!"); };

            levelingSystem.Exp.OnValueChanged += exp =>
            {
                Debug.Log($"경험치가 {exp}로 바뀌었다!");
            };
            
            levelingSystem.Level.OnValueChanged += level =>
            {
                Debug.Log($"레벨이 {level}로 바뀌었다!");
            };
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                levelingSystem.AddExp(50);
                //hp.Value += 0.5f;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                levelingSystem.AddExp(1000);
                //hp.Value -= 0.5f;
            }
        }
    }
}