using UnityEngine;

namespace Study.OOP
{
    public class GameManager : SingletonBase<GameManager>
    {
        
    }
    
    public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;
        
        private static readonly object lockObj = new object();
        
        private static bool applicationIsQuitting = false;
        private static bool isInitilized = false;
        
        
        
    }
}