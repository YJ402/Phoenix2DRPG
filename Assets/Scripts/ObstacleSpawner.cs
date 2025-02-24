using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public int obstacleCount = 5;

    public GameObject obj1, obj2;
    public GameObject[] obstaclePrefabs;

    private void Start()
    {
        SpawnObstacle();
    }

    private void SpawnObstacle()
    {
        Vector2 pos1 = obj1.transform.position;
        Vector2 pos2 = obj2.transform.position;

        for (int i = 0; i < obstacleCount; i++)
        {
            float x = Random.Range(pos1.x, pos2.x);
            float y = Random.Range(pos1.y, pos2.y);
            Vector2 spawnPosition = new Vector2(x, y);

            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        }

    }


}
