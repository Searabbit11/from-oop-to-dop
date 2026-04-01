using System;
using UnityEngine;
using Study.OOP.Study_Factory;

namespace Study.OOP.Study_Abstract_Factory
{
    // AbstractFactory Pattern
    // 단일 객체를 생성하는 팩토리 패턴과 달리 서로 연관되거나 의존적인
    // 여러 객체들(그룹, 세트)을 생성하는 생성 패턴의 한 종류.
    
    // 구현 핵심은 요청 객체는 구체적인 클래스가 무엇인지 알 필요 없이
    // 추상 팩토리 인터페이스를 통해 일관된 테마나 스타일을 가진 객체 세트를 공급받게끔
    // 설계하는게 핵심
    
    // 새로운 테마가 추가되더라도, 기존의 핵심 코드는 건드리지 안혹
    // 새로운 팩토리 클래스와 해당 테마의 부품 클래스들만 추가하면 됩니다.
    
    // - 맵의 테마등을 한번에 바꿀때
    // - 새로운 패키지(상자깡에서 그 상자)를 만들어야 할때
    // - 크로스 플랫폼 프로젝트에서 플랫폼별 인풋맵을 제작해야 할 때
    //      - New Input System에서 추상팩토리를 사용했음
    
    
    
    public class StudyAbstractFactory : MonoBehaviour
    {
        // 팩토리 패턴의 Card.Factory 계열의 클래스들을 이용해서
        // 카드깡 시스템을 제작해봅시다

        private void Awake()
        {
           
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var factory = new SilverCardPackageFactory();
                var package = factory.CreateCardPackage();
                package.Open();
            }
        }
    }
}