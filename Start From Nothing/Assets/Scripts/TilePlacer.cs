using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePlacer : MonoBehaviour
{

    public Tilemap tilemap;
    public TileBase groundTile;

    public Vector3Int location;
    // Start is called before the first frame update
    void Start()
    {
        tilemap.SetTile(location, groundTile);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
