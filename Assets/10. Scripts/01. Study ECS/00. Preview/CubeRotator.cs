using UnityEngine;

namespace ECS_Preview
{
    public class CubeRotator : MonoBehaviour
    {
        public float DegreesPerSecond;

        private void Update()
        {
            transform.Rotate(0f, DegreesPerSecond * Time.deltaTime, 0f);
        }
    }
}

