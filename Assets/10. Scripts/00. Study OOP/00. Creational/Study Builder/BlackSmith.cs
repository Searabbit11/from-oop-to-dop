using System;

namespace Study.OOP.Builder
{
    public class BlackSmith
    {
        public int Level = 1;

        public Sword GenerateSword(Grade grade)
        {
            // 3가지 기본 특징을 부여를 합니다.
            SwordBuilder builder = 
                new SwordBuilder()
                    .SetGrade(grade)
                    .SetName()
                    .SetRandomAttackValue();;

            switch (grade)
            {
                case Grade.Magic:
                    //매직 단계는 랜덤한 옵션 1개
                    builder.SetRandomRandomOption();
                    break;
                case Grade.Rare:
                    builder
                        .SetRandomElementType()
                        .SetRandomRandomOption()
                        .SetRandomRandomOption();
                    break;
                case Grade.Unique:
                    builder.SetRandomElementType();

                    if (builder.AttackValue < 3000)
                    {
                        builder.AddAttackValue();
                    }
                    
                    for (int i = 0; i < 3; i++)
                    {
                        builder.SetRandomRandomOption();
                    }
                    break;
                default:
                    break;
            }

            return builder.CreateSword();
        }


        public Sword UpgradeSword(Sword sword)
        {
            var builder = new SwordBuilder().SetSword(sword);
            for (int i = 0; i < Level; i++)
            {
                builder.SetRandomRandomOption();
            }
            
            return builder.CreateSword();            
        }
    }
}