using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject block;
    public static ObstacleManager Instance { get; private set; }

    [Header("범위 오브젝트")]
    public GameObject leftObj, rightObj;

    [Header("장애물 프리팹")]
    public GameObject rock, spike, water, wall;

    [Header("인접 영역 설정")]
    public bool dontOverlapAdj = true; //장애물 주변이 겹치지 않는지 확인
    public int AdjPadding = 1; ////장애물 여백

    public int[,] grid; //장애물 생성 가능 범위
    //int cellValue = obstacleManager.GetGridValue(x, y); 코드를 사용해 gridValue를 가져갈 수 있음
    //cellValue가 1이면 장애물, 2면 장애물 주변, 3이면 물, -1이라면 유효하지 않음

    private int minX, minY, maxX, maxY; //범위 최소치, 최대치
    private int gridWidth, gridHeight; //생성 범위의 가로, 세로 길이

    private EnemyManager enemyManager;

    private Item item;

    public struct GridObstacle
    {
        public int x, y; //그리드 장애물의 x, y 좌표
        public int width, height; //장애물의 가로, 세로 크기
        public GameObject instance; // 생성된 게임 오브젝트 인스턴스
    }

    public List<GridObstacle> obstacles = new(); //위의 struct를 저장할 리스트 초기화

    public int[,] Grid => grid; //{get;} 대신 람다식 사용

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeGrid(); //그리드 초기화
        block.gameObject.SetActive(true); //출구 막는 장애물 활성화
        SpawnObstacle(rock, 1, 1, 3); //돌, 1 x 1 크기, 3개
        SpawnObstacle(spike, 1, 1, 3); //가시, 1 x 1 크기, 3개
        SpawnObstacle(water, 6, 2, 2); //물, 6 x 2 크기, 2개
        SpawnObstacle(wall, 5, 2, 2); //벽, 5 x 2 크기, 2개
    }

    private void InitializeGrid()
    {
        Vector2 pos1 = leftObj.transform.position; //왼쪽 오브젝트의 x, y 가져오기
        Vector2 pos2 = rightObj.transform.position; //오른쪽 오브젝트의 x, y 가져오기

        minX = Mathf.FloorToInt(Mathf.Min(pos1.x, pos2.x)); //오브젝트의 x 값 소수점을 버리고 가장 적은 수를 가져옴
        maxX = Mathf.CeilToInt(Mathf.Max(pos1.x, pos2.x)); //오브젝트의 x 값 소수점을 버리고 가장 큰 수를 가져옴
        minY = Mathf.FloorToInt(Mathf.Min(pos1.y, pos2.y)); //오브젝트의 x 값 소수점을 버리고 가장 적은 수를 가져옴
        maxY = Mathf.CeilToInt(Mathf.Max(pos1.y, pos2.y)); //오브젝트의 x 값 소수점을 버리고 가장 큰 수를 가져옴

        gridWidth = maxX - minX; //최대x 값에서 최소x 값을 뺀 거리
        gridHeight = maxY - minY; //최대y 값에서 최소y 값을 뺀 거리

        grid = new int[gridWidth, gridHeight]; //그리드의 크기는 gridWidth, gridHeight 만큼의 칸을 지님
    }

    private void SpawnObstacle(GameObject prefab, int width, int height, int count) //특정 프리팹을 (n * n 사이즈) n개 생성
    {
        const int maxAttempts = 100; //최대 시도 횟수는 100회
        int placedCount = 0; //현재 배치된 장애물의 개수는 0개
        int attempts = 0; //현재 시도 된 횟수는 0회

        while (placedCount < count && attempts < maxAttempts) //배치된 개수가 count보다 적고 attempt 횟수가 남았다면
        {
            //Random.Range(0, 2)로 적을 경우 0 ~ 1사이만 나오게 되므로 +1을 넣어줘야 함
            int gridX = Random.Range(0, gridWidth - width + 1); //gridX는 0부터 (그리드 가로 - 오브젝트 가로 + 1)한 값. 
            int gridY = Random.Range(0, gridHeight - height + 1); //gridY는 0부터 (그리드 세로 - 오브젝트 세로 + 1)한 값. 

            if (AreaAvailable(gridX, gridY, width, height))
            {
                MarkArea(gridX, gridY, width, height, prefab);

                Vector2 worldPos = GridToWorld(gridX + width / 2.0f, gridY + height / 2.0f);
                //장애물이 3, 4에 위치하는데 크기가 2, 2라면 중심점은 (3+2)/2, (4+2)/2로 4, 5가 됨. 
                //GridToWorld는 그리드 좌표를 게임 월드의 좌표로 변환
                GameObject instance = Instantiate(prefab, worldPos, Quaternion.identity, transform);

                GridObstacle obstacle = new GridObstacle
                //gridX, gridY, width, height를 토대로 장애물 struct의 x, y, width, height를 추가
                {
                    x = gridX,
                    y = gridY,
                    width = width,
                    height = height,
                    instance = instance // 생성된 게임 오브젝트 저장
                };
                obstacles.Add(obstacle); //해당 장애물을 obstacles라는 리스트에 추가
                Debug.Log($"{obstacle.x}, {obstacle.y} 위치에 장애물 생성됨");

                placedCount++; //배치된 장애물 개수 1 증가
            }
            attempts++; //시도 횟수 증가
        }
    }


    private bool AreaAvailable(int objPosX, int objPosY, int width, int height)
    {
        if (objPosX < 0 || objPosX + width > gridWidth || objPosY < 0 || objPosY + height > gridHeight) return false;
        //그리드 범위를 벗어나게 되면 false를 반환함
        if (dontOverlapAdj) //겹치지 않았을 경우
        {
            int checkObjPosX = Mathf.Max(0, objPosX - AdjPadding);
            int checkObjPosY = Mathf.Max(0, objPosY - AdjPadding);
            int checkEndX = Mathf.Min(gridWidth, objPosX + width + AdjPadding);
            int checkEndY = Mathf.Min(gridHeight, objPosY + height + AdjPadding);
            //오브젝트의 끝 부분 검사. gridWidth부터 objPosX의 크기에 여백을 더한만큼. 
            //예시로 gridHeight가 10, objPosY가 4, height가 2, adjPadding이 1이라면 4에서 7까지 검사함. 

            for (int x = checkObjPosX; x < checkEndX; x++)
            {
                for (int y = checkObjPosY; y < checkEndY; y++)
                {
                    if (grid[x, y] != 0) //x, y가 0이 아닐 경우 즉, 장애물 배치 불가 칸이라면
                    {
                        return false; //영역에 장애물 배치 불가로 여김
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
                    if (grid[x, y] != 0) //x, y가 0이 아닐 경우 즉, 장애물 배치 불가 칸이라면
                    {
                        return false;  //영역에 장애물 배치 불가로 여김
                    }
                }
            }
        }
        return true; //이외의 경우 true로 반환하여 장애물을 배치할 수 있게 해줌
    }

    private void MarkArea(int objPosX, int objPosY, int width, int height, GameObject prefab) //장애물의 x, y 좌표 및 장애물 크기, 프리팹 타입
    {
        int obstacleValue = (prefab == water) ? 3 : 1; // 물 장애물인 경우 그리드 값을 3으로 설정, 아닌 경우 1로 설정

        for (int x = objPosX; x < objPosX + width; x++) //x는 장애물의 x좌표, x좌표 + 오브젝트 사이즈 만큼 반복
        {
            for (int y = objPosY; y < objPosY + height; y++) //y는 장애물의 y좌표, y좌표 + 오브젝트 사이즈 만큼 반복
            {
                grid[x, y] = obstacleValue; //장애물 타입에 따라 그리드 값 설정
            }
        }

        if (dontOverlapAdj)
        {
            for (int x = objPosX - AdjPadding; x < objPosX + width + AdjPadding; x++)
            {
                for (int y = objPosY - AdjPadding; y < objPosY + height + AdjPadding; y++)
                {
                    // 장애물 주변으로 인식되는 칸이 그리드 안에 있는지 확인
                    if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
                    {
                        // 장애물 내부가 아니라면
                        bool isInsideObstacle = (x >= objPosX && x < objPosX + width &&
                                               y >= objPosY && y < objPosY + height);

                        if (!isInsideObstacle && grid[x, y] == 0)
                        {
                            grid[x, y] = 2; //인접 영역은 2로 지정함
                        }
                    }
                }
            }
        }
    }
    public Vector2 GridToWorld(float gridX, float gridY)
    {
        return new Vector2(minX + gridX, minY + gridY);
        //minX라는 최소 x값에 gridX만큼을 추가한 값을 반환
    }

    public Vector2Int WorldToGrid(Vector2 worldPos) //worldPos를 그리드로 반환
    {
        int x = Mathf.FloorToInt(worldPos.x - minX); //소수점 아래는 떼어버림
        int y = Mathf.FloorToInt(worldPos.y - minY);
        return new Vector2Int(x, y);
    }

    // 그리드 상태 확인 (0: 빈 공간, 1: 일반 장애물, 2: 인접 영역, 3: 물 장애물, -1: 유효하지 않은 위치)
    public int GetGridValue(int x, int y)
    {
        if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
        {
            return grid[x, y]; //x와 y가 그리드 범위 내에 있다면 좌표값을 반환함
        }
        return -1; //아닐 경우 -1를 반환
    }

    public void BlockRemove()
    {
        block.gameObject.SetActive(false); //출구 막는 장애물 비활성화
    }
}