using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obj1, obj2; //범위 지정 오브젝트
    public GameObject rockPrefab, holePrefab, waterPrefab, wallPrefab; //생성할 프리팹
    public GameObject[] prefabs; //생성할 모든 프리팹
    public int rockCount = 5; //돌의 개수
    public int holeCount = 3; //구멍의 개수
    public int waterCount = 2; //수로의 개수
    public int wallCount = 2; //벽의 개수
    public float minDistance = 20f; //장애물 간의 거리
    public LayerMask obstacleLayer;
    public List<Vector2> listofObstacles = new List<Vector2>();

    private void Start()
    {
        SpawnObstacle();
    }
    private void SpawnObstacle()
    {
        Vector2 pos1 = obj1.transform.position; //범위 지정 오브젝트(좌측 상단)의 좌표
        Vector2 pos2 = obj2.transform.position; //범위 지정 오브젝트(우측 하단)의 좌표

        //아래는 혹시라도 오브젝트1과 2의 위치가 바뀔 경우를 생각해 둘 중 더 적은 쪽을 min 또는 max로 지정
        float minX = Mathf.Min(pos1.x, pos2.x); 
        float maxX = Mathf.Max(pos1.x, pos2.x); 
        float minY = Mathf.Min(pos1.y, pos2.y);
        float maxY = Mathf.Max(pos1.y, pos2.y);

        float x = Random.Range(minX, maxX); //x는 오브젝트1과 2의 x좌표 사이
        float y = Random.Range(minY, maxY); //y는 오브젝트1과 2의 y좌표 사이

        for (int i = 0; i < prefabs.Length; i++)
        {
            PlaceObstacle(prefabs[i], wallCount, minX, maxX, minY, maxY);
        }
    }

    private void PlaceObstacle(GameObject prefab, int count, float minX, float maxX, float minY, float maxY)
    {
        int attempts = 0; //시도
        int maxAttempts = count * 5; //최대 시도 횟수는 count * 5

        for (int i = 0; i < count; i++)
        {
            if (attempts > maxAttempts) break; //최대 시도 횟수를 넘기면 중단

            Vector2 spawnPosition; //생성 위치 벡터2
            bool validPosition; //가능한 위치인지 확인하는 bool값
            do
            {
                //현재 스프라이트가 16px로 되어 있으므로, 반올림한 후 다시 0.16을 곱해 16px 단위로 생성되게 함
                float x = Mathf.Round(Random.Range(minX, maxX) / 0.16f) * 0.16f;
                float y = Mathf.Round(Random.Range(minY, maxY) / 0.16f) * 0.16f;

                spawnPosition = new Vector2(x, y); //생성 위치는 위의 랜덤 값으로 지정
                attempts++; //시도 횟수 추가

                Collider2D[] hits = Physics2D.OverlapCircleAll(spawnPosition, minDistance, obstacleLayer);
                validPosition = (hits.Length == 0);
                //장애물 생성 위치의 특정 거리 내에 장애물 레이어를 공유하는 장애물이 있는지 확인

            }
            while (!validPosition);

            listofObstacles.Add(spawnPosition);
            GameObject obj = Instantiate(prefab, spawnPosition, Quaternion.identity, transform);
            //Instantiate으로 특정 프리팹을 정해진 위치에 초기화된 각도로 자식 오브젝트로 생성

            if (obj.GetComponent<BoxCollider2D>() == null)
            {
                obj.AddComponent<BoxCollider2D>().isTrigger = false;
            }

            foreach (Transform child in obj.transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("Obstacle");
            }
            obj.layer = LayerMask.NameToLayer("Obstacle"); //레이어의 이름은 Obstacle
        }
    }
}
