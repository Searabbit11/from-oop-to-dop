using UnityEngine;

namespace Study.OOP.Study_Factory
{
    public abstract partial class Card
    {
        public string Name;
        public int Value;
        public Color Color; // 여기서 컬러는 등급 역할을 합니다

        protected Card(string name, int value)
        {
            Name = name;
            Value = value;
        }
        
        public abstract string GetFace();
    }

    public partial class NormalCard : Card
    {
        protected NormalCard(string name, int value) : base(name, value)
        {
        }

        public override string GetFace()
        {
            throw new System.NotImplementedException();
        }
    }

    public partial class RareCard : Card
    {
        protected RareCard(string name, int value) : base(name, value)
        {
        }

        public override string GetFace()
        {
            throw new System.NotImplementedException();
        }
    }

    public partial class UniqueCard : Card
    {
        protected UniqueCard(string name, int value) : base(name, value)
        {
        }

        public override string GetFace()
        {
            throw new System.NotImplementedException();
        }
    }

    public partial class LegendaryCard : Card
    {
        protected LegendaryCard(string name, int value) : base(name, value)
        {
        }

        public override string GetFace()
        {
            throw new System.NotImplementedException();
        }
    }

    public partial class EpicCard : Card
    {
        protected EpicCard(string name, int value) : base(name, value)
        {
        }

        public override string GetFace()
        {
            throw new System.NotImplementedException();
        }
    }
}