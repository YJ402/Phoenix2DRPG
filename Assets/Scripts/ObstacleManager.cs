using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obj1, obj2; //���� ���� ������Ʈ
    public GameObject rockPrefab, holePrefab, waterPrefab, wallPrefab; //������ ������
    public GameObject[] prefabs; //������ ��� ������
    public int rockCount = 5; //���� ����
    public int holeCount = 3; //������ ����
    public int waterCount = 2; //������ ����
    public int wallCount = 2; //���� ����
    public float minDistance = 20f; //��ֹ� ���� �Ÿ�
    public LayerMask obstacleLayer;
    public List<Vector2> listofObstacles = new List<Vector2>();

    private void Start()
    {
        SpawnObstacle();
    }
    private void SpawnObstacle()
    {
        Vector2 pos1 = obj1.transform.position; //���� ���� ������Ʈ(���� ���)�� ��ǥ
        Vector2 pos2 = obj2.transform.position; //���� ���� ������Ʈ(���� �ϴ�)�� ��ǥ

        //�Ʒ��� Ȥ�ö� ������Ʈ1�� 2�� ��ġ�� �ٲ� ��츦 ������ �� �� �� ���� ���� min �Ǵ� max�� ����
        float minX = Mathf.Min(pos1.x, pos2.x); 
        float maxX = Mathf.Max(pos1.x, pos2.x); 
        float minY = Mathf.Min(pos1.y, pos2.y);
        float maxY = Mathf.Max(pos1.y, pos2.y);

        float x = Random.Range(minX, maxX); //x�� ������Ʈ1�� 2�� x��ǥ ����
        float y = Random.Range(minY, maxY); //y�� ������Ʈ1�� 2�� y��ǥ ����

        for (int i = 0; i < prefabs.Length; i++)
        {
            PlaceObstacle(prefabs[i], wallCount, minX, maxX, minY, maxY);
        }
    }

    private void PlaceObstacle(GameObject prefab, int count, float minX, float maxX, float minY, float maxY)
    {
        int attempts = 0; //�õ�
        int maxAttempts = count * 5; //�ִ� �õ� Ƚ���� count * 5

        for (int i = 0; i < count; i++)
        {
            if (attempts > maxAttempts) break; //�ִ� �õ� Ƚ���� �ѱ�� �ߴ�

            Vector2 spawnPosition; //���� ��ġ ����2
            bool validPosition; //������ ��ġ���� Ȯ���ϴ� bool��
            do
            {
                //���� ��������Ʈ�� 16px�� �Ǿ� �����Ƿ�, �ݿø��� �� �ٽ� 0.16�� ���� 16px ������ �����ǰ� ��
                float x = Mathf.Round(Random.Range(minX, maxX) / 0.16f) * 0.16f;
                float y = Mathf.Round(Random.Range(minY, maxY) / 0.16f) * 0.16f;

                spawnPosition = new Vector2(x, y); //���� ��ġ�� ���� ���� ������ ����
                attempts++; //�õ� Ƚ�� �߰�

                Collider2D[] hits = Physics2D.OverlapCircleAll(spawnPosition, minDistance, obstacleLayer);
                validPosition = (hits.Length == 0);
                //��ֹ� ���� ��ġ�� Ư�� �Ÿ� ���� ��ֹ� ���̾ �����ϴ� ��ֹ��� �ִ��� Ȯ��

            }
            while (!validPosition);

            listofObstacles.Add(spawnPosition);
            GameObject obj = Instantiate(prefab, spawnPosition, Quaternion.identity, transform);
            //Instantiate���� Ư�� �������� ������ ��ġ�� �ʱ�ȭ�� ������ �ڽ� ������Ʈ�� ����

            if (obj.GetComponent<BoxCollider2D>() == null)
            {
                obj.AddComponent<BoxCollider2D>().isTrigger = false;
            }

            foreach (Transform child in obj.transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("Obstacle");
            }
            obj.layer = LayerMask.NameToLayer("Obstacle"); //���̾��� �̸��� Obstacle
        }
    }
}
