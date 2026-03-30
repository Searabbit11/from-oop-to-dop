using System;

namespace Study.OOP
{
    // OOP설명을 위한 가상의 메인함수
    public static class StudyProgram
    {
        private static string playerName = "Goblin";
        private static int playerHp = 100;
        private static int playerAtk = 5;
        
        private static string monsterA_Name = "Goblin";
        private static int monsterA_HP = 100;
        private static int monsterA_Atk = 5;
        
        private static string monsterB_Name = "Hob Goblin";
        private static int monsterB_HP = 100;
        private static int monsterB_Atk = 5;

        

        public class Monster
        {
            public string Name;
            public virtual int Hp { get; private set; }
            public int Atk;

            public Monster(string name, int hp, int atk)
            {
                Name = name;
                Hp = hp;
                Atk = atk;
            }

            public virtual void Attack()
            {
                Console.WriteLine("{0} attacks {1}", Name, Atk);
            }
        }

        public class GoblinTrio : Monster
        {
            public override int Hp => base.Hp * Count;

            public int Count = 3;
            
            public GoblinTrio(string name, int hp, int atk) : base(name, hp, atk)
            {
                hp *= Count;
            }
            
            public override void Attack()
            {
                base.Attack();
                base.Attack();
                base.Attack();
            }
        }

        
        public static void Main()
        {
           
        }
    }
    
    
    public class Study_OOP
    {
        
    }
}