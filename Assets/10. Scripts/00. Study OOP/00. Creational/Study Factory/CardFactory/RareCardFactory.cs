using Random = UnityEngine.Random;

namespace Study.OOP.Study_Factory
{
    public partial class RareCard
    {
        public class RareCardFactory : Factory
        {
            public RareCardFactory(int minValue, int maxValue) : base(minValue, maxValue)
            {
                MinValue = minValue;
                MaxValue = maxValue;
            }

            protected override string[] Names { get; set; } = new string[]
            {
                "얼음술사 카이라",
                "그림자 추적자",
                "전장의 치료사",
                "붉은 검의 마야",
                "야성의 전령",
                "도적 길드 대원",
                "용맹한 척후병",
                "독안개 연금술사",
                "바람을 쫓는 자",
                "고대의 방패수호자"
            };
            protected override int MinValue { get; set; }
            protected override int MaxValue { get; set; }

            public override Card CreateCard()
            {
                string randName = Names[Random.Range(0, Names.Length)];
                Card card = new RareCard(randName, Random.Range(MinValue, MaxValue));
                card.Color = UnityEngine.Color.yellow;
                return card;
            }
        }
    }
    
    
  
}