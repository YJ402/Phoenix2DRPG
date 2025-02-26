using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("������ ������")]
    public GameObject itemPrefab;

    [Header("������ ����")]
    public float spawnChance = 10f; // �⺻ 20% Ȯ���� ������ ����
    public int maxItems = 3; // �ִ� ������ ���� ����

    private ObstacleManager obstacleManager;
    private List<Vector2Int> emptySpaces = new List<Vector2Int>(); //���� int ���� ������ List
    private List<GameObject> spawnedItems = new List<GameObject>(); //������ ������ ����

    void Start()
    {
        obstacleManager = GetComponent<ObstacleManager>();

        if (obstacleManager == null) //obstacleManager�� �� ���̸� ��������
        {
            obstacleManager = FindObjectOfType<ObstacleManager>();
            Debug.LogError("ObstacleManager�� ã�� �� �����ϴ�!");
            return;
        }

        // ��� ��ֹ��� ��ġ�� �� ������ ���� ����
        Invoke("SpawnItems", 0.1f); // �ణ�� ������ �ΰ� ����. obstacleManager�� ������ �ð� �ʿ�
    }

    void SpawnItems()
    {
        FindEmptySpaces(); //������ ��ġ ���� ã��
        PlaceItems(); //������ ��ġ
    }

    void FindEmptySpaces()
    {
        int[,] grid = obstacleManager.Grid; //obstacleManager�κ��� grid �޾ƿ���
        int gridWidth = grid.GetLength(0); //���� ����
        int gridHeight = grid.GetLength(1); //���� ����

        emptySpaces.Clear(); //List ����α�

        for (int x = 0; x < gridWidth; x++) // grid�� ��ȸ�ϸ鼭 ���� 0�� �� ���� ã��
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (grid[x, y] == 0) // ���� 0�̸� �� ����
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
            Debug.LogError("������ ������ �ʿ�");
            return;
        }

        int itemCount = 0; //������ ������ ����
        // �� ���� ����Ʈ�� ��� ������ ��ġ�� ������ ��ġ
        List<Vector2Int> randomList = new List<Vector2Int>(emptySpaces);

        Shuffle(randomList);

        foreach (Vector2Int emptySpace in randomList)
        {
            // �ִ� ������ ���� üũ
            if (itemCount >= maxItems)
            {
                break;
            }

            // spawnChance�� Ȯ���� ������ ����
            if (Random.Range(0f, 100f) < spawnChance)
            {
                // �׸��� ��ǥ�� ���� ��ǥ�� ��ȯ (�������� �߽��� ��ġ)
                Vector2 worldPos = obstacleManager.GridToWorld(emptySpace.x + 0.5f, emptySpace.y + 0.5f);

                // ������ ����
                GameObject newItem = Instantiate(itemPrefab, worldPos, Quaternion.identity, transform);
                spawnedItems.Add(newItem);
                itemCount++;

                Debug.Log($"��ġ ({emptySpace.x}, {emptySpace.y})�� ������ ������ ({itemCount}/{maxItems})");
            }
        }

        Debug.Log($"�� {spawnedItems.Count}���� �������� �����Ǿ����ϴ�.");
    }

    // ������ ��� ������ ���� (�ʿ�� ���)
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

    // ������ ����� (�ʿ�� ���)
    public void RespawnItems()
    {
        ClearItems();
        SpawnItems();
    }

    // ����Ʈ�� �������� ���� ���� �޼��� (Fisher-Yates �˰���)
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