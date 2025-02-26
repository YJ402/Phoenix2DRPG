using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obj1, obj2; //���� ���� ������Ʈ
    public GameObject rock, spike, water, wall; //������ ������Ʈ
    private int[,] nodes; //��ǥ�� ������ array
    int minX, maxX, minY, maxY; //��ֹ� ���� ����
    Vector2 pos1, pos2; //���� ���� ������Ʈ�� ��ǥ��
    List<(int, int)> locations = new List<(int, int)>(); //��ǥ�� ������ List
    int width, height;

    int x, y = 0; //x, y ��ǥ�� 0���� �ʱ�ȭ

    private void Start()
    {
        SpawnObstacle(); //��ֹ� ����
    }
    private void SpawnObstacle()
    {
        pos1 = obj1.transform.position; //���� ���� ������Ʈ(���� ���)�� ��ǥ
        pos2 = obj2.transform.position; //���� ���� ������Ʈ(���� �ϴ�)�� ��ǥ

        //�Ʒ��� Ȥ�ö� ������Ʈ1�� 2�� ��ġ�� �ٲ� ��츦 ������ �� �� �� ���� ���� min �Ǵ� max�� ����
        minX = (int)Mathf.Min(pos1.x, pos2.x);
        maxX = (int)Mathf.Max(pos1.x, pos2.x);
        minY = (int)Mathf.Min(pos1.y, pos2.y);
        maxY = (int)Mathf.Max(pos1.y, pos2.y);
        width = maxX - minX; //������ ���� ���� ���� (����� ����)
        height = maxY - minY; //������ ���� ���� ���� (����� ����)
        nodes = new int[width, height]; 
        //Debug.Log($"{width}, {height}");

        ChooseLocation(rock, 3, 6, 2); //�� 3��, ������� 1 x 1
        ChooseLocation(spike, 3, 6, 2);
        ChooseLocation(water, 2, 6, 2); //�� 2��, ������� 6 x 2
        ChooseLocation(wall, 2, 6, 2);
    }

    private void ChooseLocation(GameObject go, int count, int sizeX, int sizeY)
    {
        const int maxAttempts = 10;  // �ִ� �õ� Ƚ�� ����� ����
        int placedCount = 0;         // ���������� ��ġ�� ������Ʈ ��
        int attemptsMade = 0;        // �õ� Ƚ��

        while (placedCount < count && attemptsMade < maxAttempts)
        {
            // ���� ��ġ ����
            int randomX = Random.Range(minX, maxX);
            int randomY = Random.Range(minY, maxY);

            // ���� ��ǥ�� ��ȯ
            int indexX = randomX - minX;
            int indexY = randomY - minY;

            // �ش� ��ġ�� ��ġ �������� Ȯ��
            if (IsAreaAvailable(indexX, indexY, sizeX, sizeY))
            {
                // ���������� ��ġ
                SetArea(indexX, indexY, sizeX, sizeY);
                Vector2 goLocation = new Vector2(randomX, randomY);
                Instantiate(go, goLocation, Quaternion.identity, transform);

                // ���� ī���� ����
                placedCount++;
            }
            else
            {
                // ������ ��� �õ� Ƚ���� ����
                attemptsMade++;
            }
        }

        // ��� ������Ʈ�� ��ġ���� ������ ��� �α� ��� (���û���)
        if (placedCount < count)
        {
            Debug.LogWarning($"{go.name} ������Ʈ {count}�� �� {placedCount}���� ��ġ ���� (�ִ� �õ� Ƚ�� ����)");
        }
    }

    private bool IsAreaAvailable(int startX, int startY, int sizeX, int sizeY)
    {
        for (int i = startX; i < startX + sizeX; i++) //int i ���� startX ��ǥ ���̶� ġ��, i���� startX�� sizeX�� ���� ������ ���� ��
        {
            for (int j = startY; j < startY + sizeY; j++) //int j���� startY��� ġ��, j���� startY�� sizeY�� ���� ������ ���� ��
            {
                if (i < 0 || i >= width || j < 0 || j >= height || nodes[i, j] != 0)
                //i�� 0���� ���ų�, i�� nodes�� ���� ���̺��� ũ�ų�, j�� 0���� �۰ų�, nodes�� ���� ���̺��� ũ�ų�, nodes[i, j]�� ���� 0�� �ƴ϶��
                return false;
                //Debug.Log($"{i}, {j}���� ���� �õ�");
            }
        }
        return true;
    }

    private void SetArea(int objPosX, int objPosY, int objSizeX, int objSizeY)
    {
        int[] dx = { 1, 1, 0, -1, -1, -1, 0, 1 }; //
        int[] dy = { 0, 1, 1, 1, 0, -1, -1, -1 };

        int i = objPosX;
        int j = objPosY;

        while(i < objPosX + objSizeX)
        {
            j = objPosY;
            while (j < objPosY + objSizeY)
            {
                nodes[i, j] = 1;
                locations.Add((i, j));

                for (int dir = 0; dir < 8; dir++)
                {
                    int newX = i + dx[dir];
                    int newY = j + dy[dir];

                    if (newX >= 0 && newX < width && newY >=0 && newY < height && nodes[newX, newY] == 0)
                    {
                        nodes[newX, newY] = 2;
                    }
                }
                j++;
            }
            i++;
        }
    }
}
