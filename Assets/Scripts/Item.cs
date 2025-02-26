using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("아이템 프리팹")]
    public GameObject itemPrefab;

    [Header("아이템 설정")]
    public float spawnChance = 10f; // 기본 20% 확률로 아이템 생성
    public int maxItems = 3; // 최대 아이템 개수 제한

    private ObstacleManager obstacleManager;
    private List<Vector2Int> emptySpaces = new List<Vector2Int>(); //벡터 int 값을 저장할 List
    private List<GameObject> spawnedItems = new List<GameObject>(); //생성된 아이템 보관

    void Start()
    {
        obstacleManager = GetComponent<ObstacleManager>();

        if (obstacleManager == null) //obstacleManager가 안 보이면 가져오기
        {
            obstacleManager = FindObjectOfType<ObstacleManager>();
            Debug.LogError("ObstacleManager를 찾을 수 없습니다!");
            return;
        }

        // 모든 장애물이 배치된 후 아이템 생성 실행
        Invoke("SpawnItems", 0.1f); // 약간의 지연을 두고 실행. obstacleManager가 생성될 시간 필요
    }

    void SpawnItems()
    {
        FindEmptySpaces(); //아이템 배치 공간 찾기
        PlaceItems(); //아이템 배치
    }

    void FindEmptySpaces()
    {
        int[,] grid = obstacleManager.Grid; //obstacleManager로부터 grid 받아오기
        int gridWidth = grid.GetLength(0); //가로 범위
        int gridHeight = grid.GetLength(1); //세로 범위

        emptySpaces.Clear(); //List 비워두기

        for (int x = 0; x < gridWidth; x++) // grid를 순회하면서 값이 0인 빈 공간 찾기
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (grid[x, y] == 0) // 값이 0이면 빈 공간
                {
                    emptySpaces.Add(new Vector2Int(x, y));
                }
            }
        }
    }

    void PlaceItems()
    {
        if (itemPrefab == null)
        {
            Debug.LogError("아이템 프리팹 필요");
            return;
        }

        int itemCount = 0; //생성된 아이템 개수
        // 빈 공간 리스트를 섞어서 랜덤한 위치에 아이템 배치
        List<Vector2Int> randomList = new List<Vector2Int>(emptySpaces);

        Shuffle(randomList);

        foreach (Vector2Int emptySpace in randomList)
        {
            // 최대 아이템 개수 체크
            if (itemCount >= maxItems)
            {
                break;
            }

            // spawnChance의 확률로 아이템 생성
            if (Random.Range(0f, 100f) < spawnChance)
            {
                // 그리드 좌표를 월드 좌표로 변환 (아이템의 중심점 위치)
                Vector2 worldPos = obstacleManager.GridToWorld(emptySpace.x + 0.5f, emptySpace.y + 0.5f);

                // 아이템 생성
                GameObject newItem = Instantiate(itemPrefab, worldPos, Quaternion.identity, transform);
                spawnedItems.Add(newItem);
                itemCount++;

                Debug.Log($"위치 ({emptySpace.x}, {emptySpace.y})에 아이템 생성됨 ({itemCount}/{maxItems})");
            }
        }

        Debug.Log($"총 {spawnedItems.Count}개의 아이템이 생성되었습니다.");
    }

    // 생성된 모든 아이템 삭제 (필요시 사용)
    public void ClearItems()
    {
        foreach (GameObject item in spawnedItems)
        {
            if (item != null)
            {
                Destroy(item);
            }
        }
        spawnedItems.Clear();
    }

    // 아이템 재생성 (필요시 사용)
    public void RespawnItems()
    {
        ClearItems();
        SpawnItems();
    }

    // 리스트를 무작위로 섞는 헬퍼 메서드 (Fisher-Yates 알고리즘)
    private void Shuffle<T>(List<T> list)
    {
        int num = list.Count;
        for (int i = 0; i < num; i++)
        {
            int random = i + Random.Range(0, num - i);
            T temp = list[random];
            list[random] = list[i];
            list[i] = temp;
        }
    }
}