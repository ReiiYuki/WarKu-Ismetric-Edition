using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardEnvironmentController : MonoBehaviour {

    //Prefabs
    public GameObject landTile, rockTile, forestTile;
    public GameObject[] riverTile;
    public GameObject[] mountainTile;

    //Board Object
    GameObject[,] boardFloor,boardUnit;

    //Constant
    public int BOARD_SIZE = 16;
    const float Y_REAL_OFFSET = 3.5f;

	// Use this for initialization
	void Start () {
        GenerateBoard();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Utility for game
    public GameObject GetUnit(int x,int y)
    {
        return boardUnit[x, y];
    }

    public bool SpawnUnit(int x,int y,GameObject unit,string type)
    {
        if (!GetUnit(x, y) && IsSpawnZone(x, y,type) && CanMoveInto(x, y))
        {
            boardUnit[x, y] = Instantiate(unit, GetPositionOfTile(x, y), Quaternion.identity);
            boardUnit[x, y].GetComponent<UnitMovement>().SetPosition(x, y);
            boardUnit[x, y].tag = type;
            boardUnit[x, y].transform.SetParent(boardFloor[x, y].transform);
            return true;
        }
        return false;
    }

    public bool IsSpawnZone(int x,int y,string type)
    {
        return type=="PlayerUnit"?y == BOARD_SIZE - 1:IsEnemySpawnZone(x,y);
    }

    public bool IsUpperBound(int position)
    {
        return position == BOARD_SIZE-1;
    }

    public bool IsLowerBound(int position)
    {
        return position == 0;
    }

    public void MoveSprite(int x,int y,int changeX,int changeY)
    {
        boardUnit[changeX, changeY] = boardUnit[x, y];
        boardUnit[changeX, changeY].transform.position = GetPositionOfTile(changeX, changeY) + boardUnit[x, y].GetComponent<UnitMovement>().offsetVector;
        boardUnit[changeX, changeY].transform.SetParent(boardFloor[changeX, changeY].transform);
        boardUnit[x, y] = null;
    }

    public bool CanMoveInto(int x,int y)
    {
        return !(boardFloor[x, y].tag == "Ridge" || boardFloor[x, y].tag == "River" || boardFloor[x, y].tag == "RiverCurve" || boardFloor[x,y].tag == "Stone")&&!GetUnit(x,y);
    }

    public Vector3 GetPositionOfTile(int x,int y)
    {
        return boardFloor[x, y].transform.position;
    }

    public bool IsEnemySpawnZone(int x,int y)
    {
        return y == 0;
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
        PlaceRiver();
        PlaceMountain();
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

    void PlaceMountain()
    {
        for (int i = 0; i < Random.Range(0, 10);i++)
        {
            int x = Random.Range(0, BOARD_SIZE);
            int y = Random.Range(0, BOARD_SIZE);
            if (!boardFloor[x, y] || boardFloor[x, y].tag == "Slope" || boardFloor[x, y].tag == "Ridge")
            {
                if (boardFloor[x, y])
                    Destroy(boardFloor[x, y]);
                PlaceTile(x, y, mountainTile[4]);
                PlaceTileWithCondition(x - 1, y, mountainTile[7], x - 1 >= 0);
                PlaceTileWithCondition(x + 1, y, mountainTile[1], x + 1 < boardFloor.GetLength(0));
                PlaceTileWithCondition(x, y-1, mountainTile[3], y - 1 >= 0);
                PlaceTileWithCondition(x , y+1, mountainTile[5], y + 1 < boardFloor.GetLength(1));
                PlaceTileWithCondition(x - 1, y-1, mountainTile[6], x - 1 >= 0 && y - 1 >= 0);
                PlaceTileWithCondition(x - 1, y + 1, mountainTile[8], x - 1 >= 0 && y + 1 < boardFloor.GetLength(1));
                PlaceTileWithCondition(x + 1, y-1, mountainTile[0], x + 1 < boardFloor.GetLength(0) && y - 1 >= 0);
                PlaceTileWithCondition(x + 1, y + 1, mountainTile[2], x + 1 < boardFloor.GetLength(0) && y + 1 < boardFloor.GetLength(1));
            }
        }
    }

    void PlaceRiver()
    {
        if (hasRiver())
        {
            int y = Random.Range(2, boardFloor.GetLength(1)-2);
            int x = 0;
            int state = 0;
            PlaceRiverTile(ref x, ref y,new int[]{ 0, 3, 4 }, ref state);
            while (x < boardFloor.GetLength(0) && y >= 0 && y < boardFloor.GetLength(1))
            {
                if (state == 0 || state == 5)
                    PlaceRiverTile(ref x, ref y, new int[] { 2, 5 }, ref state);
                else if (state == 1 || state == 2)
                    PlaceRiverTile(ref x, ref y, new int[] { 0, 4 }, ref state);
                else if (state == 3)
                    PlaceRiverTile(ref x, ref y, new int[] { 1 }, ref state);
                else if (state == 4)
                    PlaceRiverTile(ref x, ref y, new int[] { 0, 3, 4 }, ref state);
            }
        }

    }

    bool hasRiver()
    {
        return Random.Range(0, 2)==1;
    }

    void UpdateRiverPosition(ref int state,ref int x,ref int y)
    {
        if (state == 0 || state == 5)
            y++;
        else if (state == 1 || state == 2 || state == 4)
            x++;
        else if (state == 3)
            y--;
    }

    void PlaceRiverTile(ref int x,ref int y,int[] tilesIndex,ref int state)
    {
        PlaceRandomTile(ref x,ref  y,tilesIndex, riverTile,ref  state);
        UpdateRiverPosition(ref state, ref x, ref y);
    }

    //Generator Utility
    public Vector3 GetPosition(int x, int y)
    {
        return new Vector3(y * 0.65f + x * -0.65f, y * -0.325f + x * -0.325f + Y_REAL_OFFSET, -1 * (x + y));
    }

    void PlaceTile(int x, int y, GameObject tile)
    {
        float offsetY = 0f;
        if (tile.GetComponent<SpriteRenderer>().sprite.bounds.size.y > landTile.GetComponent<SpriteRenderer>().sprite.bounds.size.y)
            offsetY = tile.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 10f;
        boardFloor[x, y] = Instantiate(tile, GetPosition(x, y)+new Vector3(0,offsetY), Quaternion.identity);
        boardFloor[x, y].GetComponent<TileBehaviour>().SetPosition(x, y);
        boardFloor[x, y].transform.SetParent(transform);
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
    void PlaceTileWithCondition(int x,int y,GameObject tile,bool condition)
    {
        if (condition)
            if (!boardFloor[x,y])
                PlaceTile(x, y, tile);
    }

    void PlaceRandomTile(ref int x,ref int y,int[] stateSets,GameObject[] tileSet,ref int state)
    {
        int selectedIndex = Random.Range(0, stateSets.Length);
        PlaceTile(x, y, tileSet[stateSets[selectedIndex]]);
        state = stateSets[selectedIndex];
    }

}
