using UnityEngine;

namespace Study.OOP.Bridge
{
    // Bridge Pattern
    //  '기능의 클래스 계층'(추상화)과 그 기능을 실제로 '구현하는 클래스 계층'(구현부)을 분리하고,
    // 이 둘을 상속이 아닌 합성을 통해 연결(다리)하는 구조 패턴의 한 종류.
    // 연관있는 두 대상을 결합할때 사용한다고 생각하면 됩니다.
    
    // - 크로스 플랫폼 입력 시스템과 조작 대상을 연결 하는것
    // - 총기와 총기 내부 모듈(여기서는 총이 브릿지 역할을 하는겁니다)
    
    
    public class StudyBridge : MonoBehaviour
    {
        private Weapon currentWeapon;
        
        // Update is called once per frame
        void Update()
        {
            // 무기 교체 : 대거
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                currentWeapon = new Dagger();
            }
            
            // 무기 교체 : 검
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentWeapon = new Sword(5);
            }
            
            // 무기 교체 : 활
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentWeapon = new Bow(5);
            }
            
            // 화염 속성 부여
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                currentWeapon?.AddEnchantment(new FireEnchantment());
            }
            
            // 빙결 속성 부여
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                currentWeapon?.AddEnchantment(new IceEnchantment());
            }

            // 현재 무기로 공격
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                currentWeapon?.Attack();
            }
        }
    }

}


