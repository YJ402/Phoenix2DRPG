using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public int obstacleCount = 5;

    public GameObject obj1, obj2;
    public GameObject rockPrefab, holePrefab, waterPrefab, wallPrefab;
    public int rockCount = 5;
    public int holeCount = 3;
    public int waterCount = 2;
    public int wallCount = 2;
    public List<Vector2> listofObstacles;

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

            float minX = Mathf.Min(pos1.x, pos2.x);
            float maxX = Mathf.Max(pos1.x, pos2.x);
            float minY = Mathf.Min(pos1.y, pos2.y);
            float maxY = Mathf.Max(pos1.y, pos2.y);

            HashSet<Vector2> occupiedPositions = new HashSet<Vector2>();

            PlaceObstacle(rockPrefab, rockCount, minX, maxX, minY, maxY, occupiedPositions);
            PlaceObstacle(holePrefab, holeCount, minX, maxX, minY, maxY, occupiedPositions);
            PlaceObstacle(waterPrefab, waterCount, minX, maxX, minY, maxY, occupiedPositions);
            PlaceObstacle(wallPrefab, wallCount, minX, maxX, minY, maxY, occupiedPositions);
        }
    }

    private void PlaceObstacle(GameObject prefab, int count, float minX, float maxX, float minY, float maxY, HashSet<Vector2> occupiedPositions)
    {
        int attempts = 0;
        int maxAttempts = count * 5;

        for (int i = 0; i < count; i++)
        {
            if (attempts > maxAttempts) break; // 너무 많이 시도하면 중단

            Vector2 spawnPosition;
            do
            {
                float x = Random.Range(minX, maxX);
                float y = Random.Range(minY, maxY);
                spawnPosition = new Vector2(x, y);
                attempts++;
            }
            while (occupiedPositions.Contains(spawnPosition)); // 중복 방지

            occupiedPositions.Add(spawnPosition);
            Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
    }
}
