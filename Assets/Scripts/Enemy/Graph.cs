//using System;
//using System.Collections.Generic;
//using Unity.Mathematics;
//using UnityEditor.Search;
//using UnityEngine;
//using UnityEngine.UIElements;
//using static UnityEngine.RuleTile.TilingRuleOutput;
//using Transform = UnityEngine.Transform;

//public class Graph
//{
//    private int[,] graph;
//    Queue<(int, int)> queue;
//    List<(int, int)> way;

//    public Graph(int row, int col)
//    {
//        graph = new int[row, col];

//        for (int i = 0; i < graph.GetLength(0); i++)
//            for (int j = 0; j < graph.GetLength(1); j++)
//                graph[i, j] = 0;
//    }

//    public Graph SetObstacle() // 그냥 땅은 0이고, 통과 불가능한 장애물은 1이고, 
//    {
//        //필요하시면 작성 ㄱㄱ
//    }

//    public List<(int, int)> WayToTarget(Transform origin, Transform target)
//    {
//        //origin이랑 target을 그래프에 위치.
//        Vector2Int GridTarget = new Vector2Int((int)Math.Round(target.position.x), (int)Math.Round(target.position.y));
//        Vector2Int GridOrigin = new Vector2Int((int)Math.Round(origin.position.x), (int)Math.Round(origin.position.y));

//        graph[(int)GridOrigin.x, (int)GridOrigin.y] = 2; // 그래프에 출발 위치 표시
//        graph[(int)GridTarget.x, (int)GridTarget.y] = 3; // 그래프에 타겟 위치 표시
//        queue = new Queue<(int, int)>();

//        //way = new List<(int, int)>();
//        queue.Enqueue(((int)GridTarget.x, (int)GridOrigin.y));
//        while (queue.Count > 0)
//        {
            
//            if (BFSInit(queue.Dequeue()))
//            {
//                break;
//            }
//        }
//        //return 경로를 반환해야하는데 어떻게 하지?
//    }

//    public bool BFSInit((int x, int y) position) 
//    {
//        int x = position.x;
//        int y = position.y;

//        if (graph[x, y] == 1 || graph[x, y] == 2 || x >= 0 && x < graph.GetLength(0) && y >= 0 && y < graph.GetLength(1))
//            return false;

//        if (graph[x, y] == 3)
//        {
//            return true;
//        }
//        else
//        {
//            way.Add((x, y));
//            graph[x, y] = 1; // 방문 처리
//        }

//        //주변 노드를 큐에 추가
//        queue.Enqueue((x + 1, y + 1));
//        queue.Enqueue((x + 1, y));
//        queue.Enqueue((x + 1, y - 1));
//        queue.Enqueue((x, y + 1));
//        queue.Enqueue((x, y - 1));
//        queue.Enqueue((x - 1, y + 1));
//        queue.Enqueue((x - 1, y));
//        queue.Enqueue((x - 1, y - 1));

//        return false;
//    }
//}
