using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obj1, obj2; //���� ���� ������Ʈ
    public GameObject rock, hole, water, wall; //������ ������Ʈ
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
        ChooseLocation(hole, 3, 6, 2);
        ChooseLocation(water, 2, 6, 2); //�� 2��, ������� 6 x 2
        ChooseLocation(wall, 2, 6, 2);
    }

    private void ChooseLocation(GameObject go, int count, int sizeX, int sizeY)
    {
        int attempt = 10;
        for (int i = 0; i < count; i++)
        {
            if (attempt <= 0) break; //�õ� Ƚ���� 0 �Ǵ� �� ���ϰ� �Ǹ� ����

            x = Random.Range(minX, maxX); //x�� ������Ʈ1�� 2�� x��ǥ ����
            y = Random.Range(minY, maxY); //y�� ������Ʈ1�� 2�� y��ǥ ����

            int indexX = x - minX; //������Ʈ�� x���� ������ �����ϵ��� ��. 
            int indexY = y - minY; //������Ʈ�� y���� ������ �����ϵ��� ��. 

            if (IsAreaAvailable(indexX, indexY, sizeX, sizeY))
            {
                SetArea(indexX, indexY, sizeX, sizeY); //���� ����
                Vector2 goLocation = new Vector2(x, y); //������Ʈ�� ��ġ�� x, y�� ����
                Instantiate(go, goLocation, Quaternion.identity, this.transform); //������Ʈ �ν��Ͻ�ȭ
                //Debug.Log($"{width}, {height}�� {go.name} ������Ʈ �ν��Ͻ�ȭ ����");
            }
            else
            {
                i--;
                attempt--;
                continue;
            }
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

    private void SetArea(int startX, int startY, int sizeX, int sizeY)
    {
        for (int i = startX; i < startX + sizeX; i++)
        {
            for (int j = startY; j < startY + sizeY; j++)
            {
                int realWidth = width - 2;
                int realHeight = height - 2;
                nodes[i, j] = 1; //nodes [i, j]�� ���� 1���� �����Ͽ� �ٸ� ��ֹ��� ��ġ���� ���ϰ� ����
                if (i < realWidth) nodes[i + 1, j] = 2; //������
                if (i < realWidth && j < realHeight) nodes[i + 1, j + 1] = 2; //���� �ϴ�
                if (j < realHeight) nodes[i, j + 1] = 2; //�ϴ�
                if (i > 0 && j < realHeight) nodes[i - 1, j + 1] = 2; //���� �ϴ�
                if (i > 0) nodes[i - 1, j] = 2; //����
                if (i > 0 && j > 0) nodes[i - 1, j - 1] = 2; //���� ���
                if (j > 0) nodes[i, j - 1] = 2; //��
                if (i < realWidth && j > 0) nodes[i + 1, j - 1] = 2; //�������

                //Debug.Log($"{nodes}�� {i}, {j} ��ǥ�� ������"); //��� ��ǥ�� 1�� �����Ǿ����� Ȯ��
                locations.Add((i, j));
            }
        }
    }
}
