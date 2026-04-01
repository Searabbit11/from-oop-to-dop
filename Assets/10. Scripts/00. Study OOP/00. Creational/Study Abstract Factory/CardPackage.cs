using System.Collections.Generic;
using Study.OOP.Study_Factory;

namespace Study.OOP.Study_Abstract_Factory
{
    public abstract class CardPackage
    {
        public string PackageName { get; protected set; }
        public List<Card> Cards { get; protected set; } = new List<Card>();

        public abstract void Open();
    }
}