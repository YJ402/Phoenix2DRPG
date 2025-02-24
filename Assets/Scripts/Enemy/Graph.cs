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

//    public Graph SetObstacle() // �׳� ���� 0�̰�, ��� �Ұ����� ��ֹ��� 1�̰�, 
//    {
//        //�ʿ��Ͻø� �ۼ� ����
//    }

//    public List<(int, int)> WayToTarget(Transform origin, Transform target)
//    {
//        //origin�̶� target�� �׷����� ��ġ.
//        Vector2Int GridTarget = new Vector2Int((int)Math.Round(target.position.x), (int)Math.Round(target.position.y));
//        Vector2Int GridOrigin = new Vector2Int((int)Math.Round(origin.position.x), (int)Math.Round(origin.position.y));

//        graph[(int)GridOrigin.x, (int)GridOrigin.y] = 2; // �׷����� ��� ��ġ ǥ��
//        graph[(int)GridTarget.x, (int)GridTarget.y] = 3; // �׷����� Ÿ�� ��ġ ǥ��
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
//        //return ��θ� ��ȯ�ؾ��ϴµ� ��� ����?
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
//            graph[x, y] = 1; // �湮 ó��
//        }

//        //�ֺ� ��带 ť�� �߰�
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
