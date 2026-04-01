using Study.OOP.Study_Factory;

namespace Study.OOP.Study_Abstract_Factory
{
    public class CardPackageFactory
    {
        
    }
    
    public class SilverCardPackageFactory : CardPackageFactory
    {
        // 실버 패키지 구성 : 레어 1장, 노멀 2장
        
        private NormalCard.NormalCardFactory normalCardFactory = new(0, 30);
        private RareCard.RareCardFactory rareCardFactory = new(30, 60);


        public CardPackage CreateCardPackage()
        {
            var package = new SilverCardPackage();
            package.PackageName = "실버카드패키지";

            package.Cards.Add(normalCardFactory.CreateCard());
            package.Cards.Add(normalCardFactory.CreateCard());
            package.Cards.Add(rareCardFactory.CreateCard());

            return package;
        }
    }
}