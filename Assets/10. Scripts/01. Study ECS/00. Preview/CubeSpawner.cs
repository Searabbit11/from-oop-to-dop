using UnityEngine;

namespace ECS_Preview
{
    public class CubeSpawner : MonoBehaviour
    {
        public GameObject CubePrefab;
        public int SpawnCount = 1000;

        private void Start()
        {
            SpawnCubes();
        }

        private void SpawnCubes()
        {
            for (int i = 0; i < SpawnCount; i++)
            {
                float x = (i % 100) * 1.25f;
                float z = (i / 100) * 1.25f;
                
                Vector3 spawnPosition = transform.position + new Vector3(x, 0, z);
            
                // 큐브 생성
                GameObject instance = Instantiate(CubePrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}

