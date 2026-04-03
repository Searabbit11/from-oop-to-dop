using Unity.VisualScripting;

namespace Study.OOP._02._Behavioral
{
    public class Leveling
    {
        private int[] levelUpExpTable = new[]
        {
            100,
            200,
            300,
            400,
            500,
            600,
            700,
            800,
            900,
            1000
        };

        public ObservableValue<int> Exp = new ObservableValue<int>(0);
        public ObservableValue<int> Level =  new ObservableValue<int>(0);
        
        // 경험치가 레벨 경험치를 초과하게되면 알림을 보내는 구조를 만들어 볼겁니다
        public void AddExp(int expAmount)
        {
            if (expAmount <= 0) return;
            Exp.Value += expAmount;

            // 반복문을 돌아서 레벨업 계산을 해줄겁니다.
            while (true)
            {
                int requiredExp = levelUpExpTable[Level.Value];
                if (Exp < requiredExp) break;
                
                // 레벨업 로직
                Exp.Value -= requiredExp;
                Level.Value++;
            }
        }
    }
}