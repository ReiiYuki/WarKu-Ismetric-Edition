using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour {

    public GameObject tooltipText;

    int x, y;
    Selector selector;
    Zoomer zoomer;
    BoardEnvironmentController boardCon;

	// Use this for initialization
	void Start () {
        ConnectToBoard();
        ConnectToCore();
        InitializeToolTip();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ConnectToCore()
    {
        selector = GameObject.FindGameObjectWithTag("Core").GetComponent<Selector>();
        zoomer = GameObject.FindGameObjectWithTag("Core").GetComponent<Zoomer>();
    }

    void ConnectToBoard()
    {
        boardCon = transform.parent.GetComponent<BoardEnvironmentController>();
    }

    void InitializeToolTip()
    {
        GameObject tooltip = Instantiate(tooltipText, transform.position + new Vector3(0f, 1f), Quaternion.identity);
        tooltip.transform.SetParent(transform);
        tooltip.SetActive(false);
    }

    void OnMouseDown()
    {
        if (selector.state == 1)
        {
            if (!boardCon.GetUnit(x, y) && boardCon.IsSpawnZone(x, y))
            {
                ShowTextToolTip("Unit is spawn!!");
                boardCon.SpawnUnit(x, y, selector.GetSelectUnit());
            }
            else
            {
                ShowTextToolTip("Invalid Tile!");
            }
        }
        else if (selector.state == 0)
            if (!boardCon.GetUnit(x, y))
            {
                ShowTextToolTip("No Action!");
            }
        selector.state = 0;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void SetPosition(int x,int y)
    {
        this.x = x;
        this.y = y;
    }

    void OnMouseOver()
    {
        zoomer.onTile = true;
    }

    void OnMouseExit()
    {
        zoomer.onTile = false;
    }

    void ShowTextToolTip(string text)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.GetComponentInChildren<TextMesh>().text = text;
    }
}
