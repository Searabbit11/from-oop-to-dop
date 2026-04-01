using System.Collections.Generic;

namespace Study.OOP._01._Structural.Study_Decorator
{
    // BaseStat에 변화를 주는 장식 클래스들 입니다

    public abstract class StatDecorator : IStat
    {
        public virtual int Hp => decoratedStat.Hp;
        public virtual int Attack => decoratedStat.Attack;
        public virtual float Speed => decoratedStat.Speed;
        
        public List<string> Descriptions => decoratedStat.Descriptions;

        protected readonly IStat decoratedStat;

        protected StatDecorator(IStat stat)
        {
            decoratedStat = stat;
        }
    }

    public class AttackBoost : StatDecorator
    {
        private readonly int bonus;

        public AttackBoost(IStat stat, int bonus) : base(stat)
        {
            this.bonus = bonus;
            Descriptions.Add($"공격력 강화 {bonus}");
        }

        public override int Attack => decoratedStat.Attack + bonus;
    }

    public class AttackDecrease : StatDecorator
    {
        private readonly int penalty;

        public AttackDecrease(IStat stat, int penalty) : base(stat)
        {
            this.penalty = penalty;
            Descriptions.Add($"공격력 감소 {penalty}");
        }

        public override int Attack => decoratedStat.Attack - penalty;
    }

    public class HpBoost : StatDecorator
    {
        private readonly int bonus;

        public HpBoost(IStat stat, int bonus) : base(stat)
        {
            this.bonus = bonus;
            Descriptions.Add($"체력 증가 {bonus}");
        }

        public override int Hp => decoratedStat.Hp + bonus;
    }
    
    public class HpDecrease : StatDecorator
    {
        private readonly int penalty;

        public HpDecrease(IStat stat, int penalty) : base(stat)
        {
            this.penalty = penalty;
            Descriptions.Add($"체력 감소 {penalty}");
        }

        public override int Hp => decoratedStat.Hp - penalty;
    }
    
    public class SpeedBoost : StatDecorator
    {
        private readonly float bonus;

        public SpeedBoost(IStat stat, float bonus) : base(stat)
        {
            this.bonus = bonus;
            Descriptions.Add($"속도 증가 {bonus}");
        }

        public override float Speed =>  decoratedStat.Speed + bonus;
    }
    
    public class SpeedDecrease : StatDecorator
    {
        private readonly float penalty;

        public SpeedDecrease(IStat stat, float penalty) : base(stat)
        {
            this.penalty = penalty;
            Descriptions.Add($"속도 감소 {penalty}");
        }

        public override float Speed =>  decoratedStat.Speed - penalty;
    }

    public class GoldBoost : StatDecorator
    {
        private readonly int hpBonus;
        private readonly float speedBonus;
        
        public GoldBoost(IStat stat, int hpBonus, float speedBonus) : base(stat)
        {
            this.hpBonus = hpBonus;
            this.speedBonus = speedBonus;
            Descriptions.Add($"체력&공격력 증가 {hpBonus}, {speedBonus}");
        }
        
        public override int Hp =>  decoratedStat.Hp + hpBonus;
        public override float Speed => decoratedStat.Speed + speedBonus;
    }
    
}