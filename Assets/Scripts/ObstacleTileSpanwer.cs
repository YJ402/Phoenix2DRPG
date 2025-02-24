using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObstacleTileSpanwer : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase[] obstacleTiles;
    public int mapWidth = 28;
    public int mapHeight = 18;
    public int rockCount = 5;
    public int waterCount = 10;
    public int holeCount = 5;



    private void Start()
    {
        
    }
}
