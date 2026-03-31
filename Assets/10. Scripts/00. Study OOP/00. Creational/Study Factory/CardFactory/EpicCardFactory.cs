using UnityEngine;

namespace Study.OOP.Study_Factory
{
    public partial class EpicCard
    {
        public class EpiCardFactory : Factory
        {
            public EpiCardFactory(int minValue, int maxValue) : base(minValue, maxValue)
            {
            }

            protected override string[] Names { get; set; } = new string[]
            {
                "검은 학살자 아르돈",
                "차원 문지기 리비스",
                "불멸의 방패 기사단",
                "낙뢰의 창술사",
                "고대 숲의 정령술사",
                "천둥발톱 그리핀",
                "유령의 인도자",
                "피의 맹세자",
                "심연의 흑마술사",
                "황금눈 사냥꾼"
            };
            
            protected override int MinValue { get; set; }
            protected override int MaxValue { get; set; }
            public override Card CreateCard()
            {
                string name = Names[Random.Range(0, Names.Length)];
                Card card = new EpicCard(name, Random.Range(MinValue, MaxValue));
                card.Color = UnityEngine.Color.hotPink;
                return card;
            }
        }
        
    }
}