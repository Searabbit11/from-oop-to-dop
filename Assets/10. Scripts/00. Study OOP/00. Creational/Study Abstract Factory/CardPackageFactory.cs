using System.Collections.Generic;
using Study.OOP.Study_Factory;
using UnityEngine;

namespace Study.OOP.Study_Abstract_Factory
{
    public abstract class CardPackageFactory
    {
        public abstract CardPackage CreateCardPackage();
        
        // 기획 : 패키지에서 뽑을수있는 모든 타입의 카드중 하나를 보너스 카드로 추가해주세요
        // 나는 - 안돼요, 팀장 - 그냥 해 
        public abstract Card CreateBonusCard();
        
        protected Card.Factory SelectRandomFactory(Card.Factory[] factories)
        {
            Card.Factory returnFactory = factories[0];
            int randNum = Random.Range(0, 100);
                
            for (int i = 1; i < factories.Length; i++)
            {
                if(factories[i].InRange(randNum))
                    returnFactory = factories[i];
            }
                
            return returnFactory;
        }
    }
    
    public class SilverCardPackageFactory : CardPackageFactory
    {
        // 실버 패키지 구성 : 레어 1장, 노멀 2장
        
        private NormalCard.NormalCardFactory normalCardFactory = new(0, 30);
        private RareCard.RareCardFactory rareCardFactory = new(30, 60);

        public override CardPackage CreateCardPackage()
        {
            var package = new SilverCardPackage();
            package.PackageName = "실버카드패키지";

            package.Cards.Add(normalCardFactory.CreateCard());
            package.Cards.Add(normalCardFactory.CreateCard());
            package.Cards.Add(rareCardFactory.CreateCard());

            return package;
        }

        public override Card CreateBonusCard()
        {
            Card.Factory[] factories = { normalCardFactory, rareCardFactory };
            return SelectRandomFactory(factories).CreateCard();
        }
    }

    public class GoldCardPackageFactory : CardPackageFactory
    {
        // 골드 패키지 구성 : 에픽 2장, 유니크1장, 올랜덤 2장(새로운 특징)
        
        private EpicCard.EpiCardFactory epicCardFactory = new(80, 100);
        private UniqueCard.UniqueCardFactory uniqueCardFactory = new(50, 80);
        private RareCard.RareCardFactory rareCardFactory = new(25, 50);
        private NormalCard.NormalCardFactory normalCardFactory = new(0, 25);

        public override CardPackage CreateCardPackage()
        {
            var package = new GoldCardPackage();
            package.PackageName = "골드카드패키지";

            package.Cards.Add(epicCardFactory.CreateCard());
            package.Cards.Add(epicCardFactory.CreateCard());
            package.Cards.Add(uniqueCardFactory.CreateCard());
            
            Card.Factory[] factories = { normalCardFactory, rareCardFactory, epicCardFactory, normalCardFactory };
            package.Cards.Add(SelectRandomFactory(factories).CreateCard());
            package.Cards.Add(SelectRandomFactory(factories).CreateCard());

            return package;
        }
        
        public override Card CreateBonusCard()
        {
            Card.Factory[] factories = { normalCardFactory, rareCardFactory, epicCardFactory, normalCardFactory };
            return SelectRandomFactory(factories).CreateCard();
        }
    }
}