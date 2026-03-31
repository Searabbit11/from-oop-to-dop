using UnityEngine;

namespace Study.OOP.Study_Factory
{

    public partial class NormalCard
    {
        public class NormalCardFactory : Card.Factory
        {
            public NormalCardFactory(int minValue, int maxValue) : base(minValue, maxValue)
            {
                MinValue = minValue;
                MaxValue = maxValue;
            }

            protected override string[] Names { get; set; } = new string[]
            {
                "훈련병 브랜",
                "산적 추격자",
                "숲의 감시자",
                "방패병 라무스",
                "의용군 미라",
                "마을 경비병",
                "불꽃 던지기꾼",
                "수습 마법사",
                "들개 사냥꾼",
                "골목대장 코브"
            };

            protected override int MinValue { get; set; }
            protected override int MaxValue { get; set; }

            public override Card CreateCard()
            {
                string randName = Names[Random.Range(0, Names.Length)];
                Card card = new NormalCard(randName, Random.Range(MinValue, MaxValue));
                card.Color = Color.gray;
                return card;
            }
        }
    }
}