using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obj1, obj2; //범위 지정 오브젝트
    public GameObject rock, spike, water, wall; //생성할 오브젝트
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
        ChooseLocation(spike, 3, 6, 2);
        ChooseLocation(water, 2, 6, 2); //물 2개, 사이즈는 6 x 2
        ChooseLocation(wall, 2, 6, 2);
    }

    private void ChooseLocation(GameObject go, int count, int sizeX, int sizeY)
    {
        const int maxAttempts = 10;  // 최대 시도 횟수 상수로 정의
        int placedCount = 0;         // 성공적으로 배치된 오브젝트 수
        int attemptsMade = 0;        // 시도 횟수

        while (placedCount < count && attemptsMade < maxAttempts)
        {
            // 랜덤 위치 생성
            int randomX = Random.Range(minX, maxX);
            int randomY = Random.Range(minY, maxY);

            // 로컬 좌표로 변환
            int indexX = randomX - minX;
            int indexY = randomY - minY;

            // 해당 위치에 배치 가능한지 확인
            if (IsAreaAvailable(indexX, indexY, sizeX, sizeY))
            {
                // 성공적으로 배치
                SetArea(indexX, indexY, sizeX, sizeY);
                Vector2 goLocation = new Vector2(randomX, randomY);
                Instantiate(go, goLocation, Quaternion.identity, transform);

                // 성공 카운터 증가
                placedCount++;
            }
            else
            {
                // 실패한 경우 시도 횟수만 증가
                attemptsMade++;
            }
        }

        // 모든 오브젝트를 배치하지 못했을 경우 로그 출력 (선택사항)
        if (placedCount < count)
        {
            Debug.LogWarning($"{go.name} 오브젝트 {count}개 중 {placedCount}개만 배치 성공 (최대 시도 횟수 도달)");
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
