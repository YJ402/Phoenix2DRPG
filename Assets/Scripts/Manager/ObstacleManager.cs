using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [Header("���� ������Ʈ")]
    public GameObject leftObj, rightObj;

    [Header("��ֹ� ������")]
    public GameObject rock, spike, water, wall;

    [Header("���� ���� ����")]
    public bool dontOverlapAdj = true; //��ֹ� �ֺ��� ��ġ�� �ʴ��� Ȯ��
    public int AdjPadding = 1; //��ֹ� ����

    private int[,] grid; //��ֹ� ���� ���� ����. 
    //int cellValue = obstacleManager.GetGridValue(x, y);�� �������� ����
    //cellValue�� 1�̸� ��ֹ��� �ְ�, 2�� ��ֹ��� ��ġ���� ���ϵ� ������ �� �ְ�, 3�̸� ����ü�� ��� �����ϰ�

    private int minX, minY, maxX, maxY; //���� �ּ�ġ, �ִ�ġ
    private int gridWidth, gridHeight; //���� ������ ����, ���� ����

    public struct GridObstacle
    {
        public int x, y; //�׸��� ��ֹ��� x, y ��ǥ
        public int width, height; //��ֹ��� ����, ���� ũ��
    }

    public List<GridObstacle> obstacles = new(); //���� struct�� ������ ����Ʈ �ʱ�ȭ

    public int[,] Grid => grid; //{get;} ��� ���ٽ� ���

    private void Start()
    {
        InitializeGrid(); //�׸��� �ʱ�ȭ
        SpawnObstacle(rock, 1, 1, 3); //��, 1 x 1 ũ��, 3��
        SpawnObstacle(spike, 1, 1, 3); //����, 1 x 1 ũ��, 3��
        SpawnObstacle(water, 6, 2, 2); //��, 6 x 2 ũ��, 2��
        SpawnObstacle(wall, 5, 2, 2); //��, 5 x 2 ũ��, 2��
    }
    
    private void InitializeGrid()
    {
        Vector2 pos1 = leftObj.transform.position; //���� ������Ʈ�� x, y ��������
        Vector2 pos2 = rightObj.transform.position; //������ ������Ʈ�� x, y ��������

        minX = Mathf.FloorToInt(Mathf.Min(pos1.x, pos2.x)); //������Ʈ�� x �� �Ҽ����� ������ ���� ���� ���� ������
        maxX = Mathf.CeilToInt(Mathf.Max(pos1.x, pos2.x)); //������Ʈ�� x �� �Ҽ����� ������ ���� ū ���� ������
        minY = Mathf.FloorToInt(Mathf.Min(pos1.y, pos2.y)); //������Ʈ�� x �� �Ҽ����� ������ ���� ���� ���� ������
        maxY = Mathf.CeilToInt(Mathf.Max(pos1.y, pos2.y)); //������Ʈ�� x �� �Ҽ����� ������ ���� ū ���� ������

        gridWidth = maxX - minX; //�ִ�x ������ �ּ�x ���� �� �Ÿ�
        gridHeight = maxY - minY; //�ִ�y ������ �ּ�y ���� �� �Ÿ�

        grid = new int[gridWidth, gridHeight]; //�׸����� ũ��� gridWidth, gridHeight ��ŭ�� ĭ�� ����
    }

    private void SpawnObstacle(GameObject prefab, int width, int height, int count) //Ư�� �������� (n * n ������) n�� ����
    {
        const int maxAttempts = 100; //�ִ� �õ� Ƚ���� 100ȸ
        int placedCount = 0; //���� ��ġ�� ��ֹ��� ������ 0��
        int attempts = 0; //���� �õ� �� Ƚ���� 0ȸ

        while (placedCount < count && attempts < maxAttempts) //��ġ�� ������ count���� ���� attempt Ƚ���� ���Ҵٸ�
        {
            //Random.Range(0, 2)�� ���� ��� 0 ~ 1���̸� ������ �ǹǷ� +1�� �־���� ��
            int gridX = Random.Range(0, gridWidth - width + 1); //gridX�� 0���� (�׸��� ���� - ������Ʈ ���� + 1)�� ��. 
            int gridY = Random.Range(0, gridHeight - height + 1); //gridY�� 0���� (�׸��� ���� - ������Ʈ ���� + 1)�� ��. 

            if (AreaAvailable(gridX, gridY, width, height))
            {
                MarkArea(gridX, gridY, width, height, prefab);

                Vector2 worldPos = GridToWorld(gridX + width / 2.0f, gridY + height / 2.0f);
                //��ֹ��� 3, 4�� ��ġ�ϴµ� ũ�Ⱑ 2, 2��� �߽����� (3+2)/2, (4+2)/2�� 4, 5�� ��. 
                //GridToWorld�� �׸��� ��ǥ�� ���� ������ ��ǥ�� ��ȯ
                Instantiate(prefab, worldPos, Quaternion.identity, transform);

                GridObstacle obstacle = new GridObstacle
                //gridX, gridY, width, height�� ���� ��ֹ� struct�� x, y, width, height�� �߰�
                {
                    x = gridX,
                    y = gridY,
                    width = width,
                    height = height,
                };
                obstacles.Add(obstacle); //�ش� ��ֹ��� obstacles��� ����Ʈ�� �߰�
                Debug.Log($"{obstacle.x}, {obstacle.y} ��ġ�� ��ֹ� ������");

                placedCount++; //��ġ�� ��ֹ� ���� 1 ����
            }
            attempts++; //�õ� Ƚ�� ����
        }
    }
    private bool AreaAvailable(int objPosX, int objPosY, int width, int height)
    {
        if(objPosX < 0 || objPosX + width > gridWidth || objPosY < 0 || objPosY + height > gridHeight) return false;
        //�׸��� ������ ����� �Ǹ� false�� ��ȯ��
        if (dontOverlapAdj) //��ġ�� �ʾ��� ���
        {
            int checkObjPosX = Mathf.Max(0, objPosX - AdjPadding);
            int checkObjPosY = Mathf.Max(0, objPosY - AdjPadding);
            int checkEndX = Mathf.Min(gridWidth, objPosX + width + AdjPadding);
            int checkEndY = Mathf.Min(gridHeight, objPosY + height + AdjPadding);
            //������Ʈ�� �� �κ� �˻�. gridWidth���� objPosX�� ũ�⿡ ������ ���Ѹ�ŭ. 
            //���÷� gridHeight�� 10, objPosY�� 4, height�� 2, adjPadding�� 1�̶�� 4���� 7���� �˻���. 

            for (int x = checkObjPosX; x < checkEndX; x++)
            {
                for (int y = checkObjPosY; y < checkEndY; y++)
                {
                    if (grid[x, y] != 0) //x, y�� 0�� �ƴ� ��� ��, ��ֹ� ��ġ �Ұ� ĭ�̶��
                    {
                        return false; //������ ��ֹ� ��ġ �Ұ��� ����
                    }
                }
            }
        }
        else
        {
            for (int x = objPosX; x < objPosX + width; x++)
            {
                for (int y = objPosY; y < objPosY + height; y++)
                {
                    if (grid[x, y] != 0) //x, y�� 0�� �ƴ� ��� ��, ��ֹ� ��ġ �Ұ� ĭ�̶��
                    {
                        return false;  //������ ��ֹ� ��ġ �Ұ��� ����
                    }
                }
            }
        }
        return true; //�̿��� ��� true�� ��ȯ�Ͽ� ��ֹ��� ��ġ�� �� �ְ� ����
    }

    private void MarkArea(int objPosX, int objPosY, int width, int height, GameObject prefab) //��ֹ��� x, y ��ǥ �� ��ֹ� ũ��
    {
        int obstacleValue = (prefab == water) ? 3 : 1; //�������� ���� ��� obstacleValue�� 1�� �ƴ� 3���� ������

        for (int x = objPosX; x < objPosX + width; x++) //x�� ��ֹ��� x��ǥ, x��ǥ + ������Ʈ ������ ��ŭ �ݺ�
        {
            for (int y = objPosY; y < objPosY + height; y++) //y�� ��ֹ��� y��ǥ, y��ǥ + ������Ʈ ������ ��ŭ �ݺ�
            {
                grid[x, y] = obstacleValue; //��ֹ��� ��ġ�� ������ 1�� ����
            }
        }

        if (dontOverlapAdj)
        {
            for (int x = objPosX - AdjPadding; x < objPosX + width + AdjPadding; x++)
            {
                for (int y = objPosY - AdjPadding; y < objPosY + height + AdjPadding; y++)
                {
                    // ��ֹ� �ֺ����� �νĵǴ� ĭ�� �׸��� �ȿ� �ִ��� Ȯ��
                    if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
                    {
                        // ��ֹ� ���ΰ� �ƴ϶��
                        bool isInsideObstacle = (x >= objPosX && x < objPosX + width &&
                                               y >= objPosY && y < objPosY + height);

                        if (!isInsideObstacle && grid[x, y] == 0)
                        {
                            grid[x, y] = 2; // ���� ������ 2�� ������
                        }
                    }
                }
            }
        }
    }
    private Vector2 GridToWorld(float gridX, float gridY)
    {
        return new Vector2(minX + gridX, minY + gridY);
        //minX��� �ּ� x���� gridX��ŭ�� �߰��� ���� ��ȯ
    }

    public Vector2Int WorldToGrid(Vector2 worldPos) //worldPos�� �׸���� ��ȯ
    {
        int x = Mathf.FloorToInt(worldPos.x - minX); //�Ҽ��� �Ʒ��� �������
        int y = Mathf.FloorToInt(worldPos.y - minY); 
        return new Vector2Int(x, y);
    }

    // �׸��� ���� Ȯ�� (0: �� ����, 1: ��ֹ�, 2: ���� ����, 3: ��, -1: ��ȿ���� ���� ��ġ)
    public int GetGridValue(int x, int y)
    {
        if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
        {
            return grid[x, y]; //x�� y�� �׸��� ���� ���� �ִٸ� ��ǥ���� ��ȯ��
        }
        return -1; //�ƴ� ��� -1�� ��ȯ
    }

    // Ư�� ��ġ�� �ִ� ��ֹ� ã��
    //public GridObstacle? GetObstacleAt(int x, int y)
    //{
    //    foreach (var obstacle in obstacles)
    //    {
    //        if (x >= obstacle.x && x < obstacle.x + obstacle.width &&
    //            y >= obstacle.y && y < obstacle.y + obstacle.height)
    //        {
    //            return obstacle;
    //        }
    //    }
    //    return null;
    //}
}