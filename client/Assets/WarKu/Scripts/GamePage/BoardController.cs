using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

    #region attribute
    private float Y_REAL_OFFSET = 3.5f;
    public GameObject[] tilePrototype,unitPrototype;
    GameObject[,] boardFloor, boardUnit;
    #endregion

    #region mono
    // Use this for initialization
    void Start () {
        boardFloor = new GameObject[16, 16];
        boardUnit = new GameObject[16, 16];
        DGTProxyRemote.GetInstance().RequestBoard();
	}

	// Update is called once per frame
	void Update () {
		
	}
    #endregion

    #region initialize

    public void UpdateBoard(string floors)
    {
        int x = 0;
        int y = 0;
        foreach (string element in floors.Split(' '))
        {
            int index;
            int.TryParse(element, out index);
            PlaceTile(x, y, tilePrototype[index]);
            y++;
            if (y == 16)
            {
                y = 0;
                x++;
            }
            if (x == 16)
                break;
        }
    }

    public Vector3 GetPosition(int x, int y)
    {
        return new Vector3(y * 0.65f + x * -0.65f, y * -0.325f + x * -0.325f + Y_REAL_OFFSET, -1 * (x + y));
    }

    public Vector3 GetPositionOfTile(int x, int y)
    {
        return boardFloor[x, y].transform.position;
    }

    void PlaceTile(int x, int y, GameObject tile)
    {
        float offsetY = 0f;
        if (tile.GetComponent<SpriteRenderer>().sprite.bounds.size.y > tilePrototype[0].GetComponent<SpriteRenderer>().sprite.bounds.size.y)
            offsetY = tile.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 10f;
        boardFloor[x, y] = Instantiate(tile, GetPosition(x, y) + new Vector3(0, offsetY), Quaternion.identity);
        boardFloor[x, y].GetComponent<TileBehaviour>().SetPosition(x, y);
        boardFloor[x, y].transform.SetParent(transform);
    }
    #endregion

    #region unit
    public void UpdateUnit(int x,int y,int changeX,int changeY,int type,int direction)
    {
        if (!boardUnit[x, y])
        {
            boardFloor[x, y].GetComponent<TileBehaviour>().OnSpawnUnit(type);
            if (type == -1) return;
            boardUnit[x, y] = Instantiate(unitPrototype[type], GetPositionOfTile(x,y), Quaternion.identity);
            boardUnit[x, y].transform.SetParent(boardFloor[x, y].transform);
            boardUnit[x, y].GetComponent<UnitBehaviour>().SetPosition(x,y);
            DGTProxyRemote.GetInstance().RequestChangeDirection(x, y, 3);
        }else
        {
            boardUnit[x, y].GetComponent<UnitBehaviour>().SetDirection(direction);
        }
        if (x != changeX || y != changeY)
        {
            boardUnit[changeX, changeY] = boardUnit[x, y];
            boardUnit[changeX, changeY].transform.position = GetPositionOfTile(changeX, changeY) + boardUnit[changeX, changeY].GetComponent<UnitBehaviour>().offsetVector;
            boardUnit[changeX, changeY].transform.SetParent(boardFloor[changeX, changeY].transform);
            boardUnit[changeX, changeY].GetComponent<UnitBehaviour>().SetPosition(changeX, changeY);
            boardUnit[changeX, changeY].GetComponent<UnitBehaviour>().UpdateDirection();
            boardUnit[x, y] = null;
        }
    }
    #endregion

    public bool CanMoveInto(int x,int y)
    {
        return x>=0&&y>=0&&x<16&&y<16&&this.boardFloor[x, y].GetComponent<TileBehaviour>().canMove;
    }
}
