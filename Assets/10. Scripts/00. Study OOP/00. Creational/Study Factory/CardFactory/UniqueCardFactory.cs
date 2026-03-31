using UnityEngine;

namespace Study.OOP.Study_Factory
{
    public partial class UniqueCard
    {
        public class UniqueCardFactory : Factory
        {
            public UniqueCardFactory(int minValue, int maxValue) : base(minValue, maxValue)
            {
            }

            protected override string[] Names { get; set; } = new string[]
            {
                "유니크 얼음술사 카이라",
                "유니크 그림자 추적자",
                "유니크 전장의 치료사",
                "유니크 붉은 검의 마야",
                "유니크 야성의 전령",
                "유니크 도적 길드 대원",
                "유니크 용맹한 척후병",
                "유니크 독안개 연금술사",
                "유니크 바람을 쫓는 자",
                "유니크 고대의 방패수호자"
            };

            protected override int MinValue { get; set; }
            protected override int MaxValue { get; set; }
            
            public override Card CreateCard()
            {
                string name = Names[Random.Range(0, Names.Length)];
                Card card = new UniqueCard(name, Random.Range(MinValue, MaxValue));
                card.Color = UnityEngine.Color.blueViolet;
                return card;
            }
        }
    }
}