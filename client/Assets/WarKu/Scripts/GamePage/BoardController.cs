using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

    private float Y_REAL_OFFSET = 3.5f;
    public GameObject[] tilePrototype;
    GameObject[,] boardFloor, boardUnit;

    // Use this for initialization
    void Start () {
        boardFloor = new GameObject[16, 16];
        boardUnit = new GameObject[16, 16];
        DGTProxyRemote.GetInstance().RequestBoard();
	}
	
    public void UpdateBoard(string floors,string units)
    {
        int x = 0;
        int y = 0;
        Debug.Log(floors.Split(' ').Length);
        foreach (string element in floors.Split(' '))
        {
            int index;
            int.TryParse(element,out index);
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

	// Update is called once per frame
	void Update () {
		
	}

    Vector3 GetPosition(int x, int y)
    {
        return new Vector3(y * 0.65f + x * -0.65f, y * -0.325f + x * -0.325f + Y_REAL_OFFSET, -1 * (x + y));
    }

    void PlaceTile(int x, int y, GameObject tile)
    {
        //Debug.Log(x + " " + y);
        float offsetY = 0f;
        if (tile.GetComponent<SpriteRenderer>().sprite.bounds.size.y > tilePrototype[0].GetComponent<SpriteRenderer>().sprite.bounds.size.y)
            offsetY = tile.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 10f;
        boardFloor[x, y] = Instantiate(tile, GetPosition(x, y) + new Vector3(0, offsetY), Quaternion.identity);
    //    boardFloor[x, y].GetComponent<TileBehaviour>().SetPosition(x, y);
        boardFloor[x, y].transform.SetParent(transform);
    }
}
