using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

    //Prefabs
    public GameObject landTile, rockTile, forestTile;
    public GameObject[] riverTile;
    public GameObject[] mountainTile;

    //Board Object
    GameObject[,] boardFloor,boardUnit;

    //Constant
    const int BOARD_SIZE = 12;
    const float Y_REAL_OFFSET = 3.5f;

	// Use this for initialization
	void Start () {
        GenerateBoard();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Generator
    void GenerateBoard()
    {
        InitializeEmptyBoard(BOARD_SIZE);
        GenerateFloor();
    }

    void InitializeEmptyBoard(int size)
    {
        boardFloor = new GameObject[size, size];
        boardUnit = new GameObject[size, size];
    }

    void GenerateFloor()
    {
        PlaceForestTile();
        PlaceRockTile();
        PlaceLandFloor();
    }

    void PlaceLandFloor()
    {
        for (int x = 0; x < boardFloor.GetLength(0); x++)
            for (int y = 0; y < boardFloor.GetLength(1); y++)
                if (!boardFloor[x, y])
                    PlaceTile(x,y,landTile);
    }

    void PlaceRockTile()
    {
        PlaceSpecificTiles(Random.Range(0, 10), rockTile);
    }

    void PlaceForestTile()
    {
        PlaceSpecificTiles(Random.Range(0, 20), forestTile);
    }

    //Generator Utility
    Vector3 GetPosition(int x, int y)
    {
        return new Vector3(y * 0.65f + x * -0.65f, y * -0.325f + x * -0.325f + Y_REAL_OFFSET, -1 * (x + y));
    }

    void PlaceTile(int x, int y, GameObject tile)
    {
        float offsetY = 0f;
        if (tile.GetComponent<SpriteRenderer>().sprite.bounds.size.y > landTile.GetComponent<SpriteRenderer>().sprite.bounds.size.y)
            offsetY = tile.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 10f;
        boardFloor[x, y] = Instantiate(tile, GetPosition(x, y)+new Vector3(0,offsetY), Quaternion.identity);
    }

    void PlaceSpecificTiles(int size,GameObject tile)
    {
        for (int i = 0; i < size; i++)
        {
            int x = Random.Range(0, boardFloor.GetLength(0));
            int y = Random.Range(0, boardFloor.GetLength(1));
            if (!boardFloor[x, y])
                PlaceTile(x, y, tile);
        }
    }
}
