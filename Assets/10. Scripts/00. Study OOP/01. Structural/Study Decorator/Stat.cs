using System.Collections.Generic;
using UnityEngine;

namespace Study.OOP._01._Structural.Study_Decorator
{
    public interface IStat
    {
        int Hp { get; }
        int Attack { get; }
        float Speed { get; }
        List<string> Descriptions { get; }
    }

    public class BaseStat : IStat
    {
        public int Hp => baseHp;
        public int Attack => baseAttack;
        
        public float Speed => baseSpeed;
        public List<string> Descriptions { get; } = new List<string>();

        private readonly int baseHp;
        private readonly int baseAttack;
        private readonly float baseSpeed;

        public BaseStat(int hp, int attack, float speed)
        {
            baseHp = hp;
            baseAttack = attack;
            baseSpeed = speed;
        }
    }

    public class PlayerStat : BaseStat
    {
        public PlayerStat(int hp, int attack, float speed) : base(hp, attack, speed)
        {
        }
    }

    public class EnemyStat : BaseStat
    {
        public EnemyStat(int hp, int attack, float speed) : base(hp, attack, speed)
        {
        }
    }
}