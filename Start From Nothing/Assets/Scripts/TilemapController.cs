using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    GameController gameController;
    SunController sun;

    public int tilemapWidth = 50;
    public int tilemapHeight = 50;
    public Tilemap tilemap;
    public Sprite[] growthSprites;
    public Tile[] growthTiles;

    [HideInInspector]
    public GroundTileData[,] groundTileData;
    
    

    // Start is called before the first frame update
    private void Awake()
    {
        FillGroundTileData();

        gameController =  GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        sun = gameController.sun;
        sun.ReceiveSun += ReceiveSun;
        
    }

    void Start()
    {
    }

    private void ReceiveSun(Vector2 location, float amount)
    {
        if (!IsWithinBounds(location))
        {
            return;
        }

        Vector3Int cell = tilemap.WorldToCell(location);

        int growthResult = groundTileData[cell.x, cell.y].AddGrowth(amount);
        tilemap.SetTile(cell, growthTiles[(int)((growthTiles.Length - 1) * groundTileData[cell.x, cell.y].primaryGrowth)]);

        if(growthResult == 1)
        {
            gameController.tilesRestored++;
        }
        else if(growthResult == 2)
        {
            if(Random.value > 0.8f)
            {
                gameController.itemSpawner.SpawnItem(location, ItemType.Seed);
            }
        }
    }
    



    public bool IsWithinBounds(Vector2 location)
    {
        if(location.x <= tilemap.transform.position.x || location.x >= tilemap.transform.position.x + ((float)tilemap.size.x * (float)tilemap.cellSize.x) || location.y <= tilemap.transform.position.y || location.y >= tilemap.transform.position.y + (float)tilemap.size.y * tilemap.cellSize.y)
        {
            return false;
        }

        return true;
    }

    private void FillGroundTileData()
    {
        groundTileData = new GroundTileData[tilemapWidth, tilemapHeight];

        for (int i = 0; i < tilemapWidth; i++)
        {
            for (int j = 0; j < tilemapHeight; j++)
            {
                groundTileData[i, j] = new GroundTileData(GroundType.Ground);
                tilemap.SetTile(new Vector3Int(i, j, 0), growthTiles[0]);
            }
        }
    }
}
