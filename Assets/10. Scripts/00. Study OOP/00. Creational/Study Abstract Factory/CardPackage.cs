using System.Collections.Generic;
using Study.OOP.Study_Factory;
using UnityEngine;

namespace Study.OOP.Study_Abstract_Factory
{
    public abstract class CardPackage
    {
        public string PackageName { get; set; }
        public List<Card> Cards { get; protected set; } = new List<Card>();

        // 핵심 로직 중 하나
        public abstract void Open();
    }
    
    public class SilverCardPackage : CardPackage
    {
        public override void Open()
        {
            Debug.Log($"<color=black> Opening Silver Card Package!!  </color>");

            for (int i = 0; i < Cards.Count; i++)
            {
                string hexColor = ColorUtility.ToHtmlStringRGBA(Cards[i].Color);
                Debug.Log($"- <color=#{hexColor}>{Cards[i].Name}, {Cards[i].Value}</color>");
            }
        }
    }
}