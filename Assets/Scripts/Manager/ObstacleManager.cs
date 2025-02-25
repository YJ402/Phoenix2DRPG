using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obj1, obj2; //범위 지정 오브젝트
    public GameObject rock, hole, water, wall; //생성할 오브젝트
    private int[,] nodes; //좌표를 저장할 array
    int minX, maxX, minY, maxY; //장애물 생성 범위
    Vector2 pos1, pos2; //범위 지정 오브젝트의 좌표값
    List<(int, int)> locations = new List<(int, int)>(); //좌표를 전달할 List
    int width, height;

    int x, y = 0; //x, y 좌표를 0으로 초기화

    private void Start()
    {
        SpawnObstacle(); //장애물 생성
    }
    private void SpawnObstacle()
    {
        pos1 = obj1.transform.position; //범위 지정 오브젝트(좌측 상단)의 좌표
        pos2 = obj2.transform.position; //범위 지정 오브젝트(우측 하단)의 좌표

        //아래는 혹시라도 오브젝트1과 2의 위치가 바뀔 경우를 생각해 둘 중 더 적은 쪽을 min 또는 max로 지정
        minX = (int)Mathf.Min(pos1.x, pos2.x);
        maxX = (int)Mathf.Max(pos1.x, pos2.x);
        minY = (int)Mathf.Min(pos1.y, pos2.y);
        maxY = (int)Mathf.Max(pos1.y, pos2.y);
        width = maxX - minX; //범위의 가로 길이 측정 (양수로 나옴)
        height = maxY - minY; //범위의 세로 길이 측정 (양수로 나옴)
        nodes = new int[width, height]; 
        //Debug.Log($"{width}, {height}");

        ChooseLocation(rock, 3, 6, 2); //돌 3개, 사이즈는 1 x 1
        ChooseLocation(hole, 3, 6, 2);
        ChooseLocation(water, 2, 6, 2); //물 2개, 사이즈는 6 x 2
        ChooseLocation(wall, 2, 6, 2);
    }

    private void ChooseLocation(GameObject go, int count, int sizeX, int sizeY)
    {
        int attempt = 10;
        for (int i = 0; i < count; i++)
        {
            if (attempt <= 0) break; //시도 횟수가 0 또는 그 이하가 되면 중지

            x = Random.Range(minX, maxX); //x는 오브젝트1과 2의 x좌표 사이
            y = Random.Range(minY, maxY); //y는 오브젝트1과 2의 y좌표 사이

            int indexX = x - minX; //오브젝트의 x값에 음수를 포함하도록 함. 
            int indexY = y - minY; //오브젝트의 y값에 음수를 포함하도록 함. 

            if (IsAreaAvailable(indexX, indexY, sizeX, sizeY))
            {
                SetArea(indexX, indexY, sizeX, sizeY); //영역 설정
                Vector2 goLocation = new Vector2(x, y); //오브젝트의 위치를 x, y로 설정
                Instantiate(go, goLocation, Quaternion.identity, this.transform); //오브젝트 인스턴스화
                //Debug.Log($"{width}, {height}에 {go.name} 오브젝트 인스턴스화 성공");
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
        for (int i = startX; i < startX + sizeX; i++) //int i 값이 startX 좌표 값이라 치고, i값이 startX와 sizeX를 더한 값보다 적을 때
        {
            for (int j = startY; j < startY + sizeY; j++) //int j값이 startY라고 치고, j값이 startY와 sizeY를 더한 값보다 적을 때
            {
                if (i < 0 || i >= width || j < 0 || j >= height || nodes[i, j] != 0)
                //i가 0보다 적거나, i가 nodes의 가로 길이보다 크거나, j가 0보다 작거나, nodes의 세로 길이보다 크거나, nodes[i, j]의 값이 0이 아니라면
                return false;
                //Debug.Log($"{i}, {j}에서 생성 시도");
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
                nodes[i, j] = 1; //nodes [i, j]의 값을 1으로 지정하여 다른 장애물이 배치되지 못하게 막음
                if (i < realWidth) nodes[i + 1, j] = 2; //오른쪽
                if (i < realWidth && j < realHeight) nodes[i + 1, j + 1] = 2; //우측 하단
                if (j < realHeight) nodes[i, j + 1] = 2; //하단
                if (i > 0 && j < realHeight) nodes[i - 1, j + 1] = 2; //좌측 하단
                if (i > 0) nodes[i - 1, j] = 2; //왼쪽
                if (i > 0 && j > 0) nodes[i - 1, j - 1] = 2; //왼쪽 상단
                if (j > 0) nodes[i, j - 1] = 2; //위
                if (i < realWidth && j > 0) nodes[i + 1, j - 1] = 2; //우측상단

                //Debug.Log($"{nodes}의 {i}, {j} 좌표에 생성됨"); //어느 좌표가 1로 지정되었는지 확인
                locations.Add((i, j));
            }
        }
    }
}
