using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour {

    public GameObject tooltipText,tooltipAction;

    public int x, y;
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
        GameObject noActionTooltip = Instantiate(tooltipText, transform.position + new Vector3(0f, 1f), Quaternion.identity);
        noActionTooltip.transform.SetParent(transform);
        noActionTooltip.SetActive(false);
        GameObject actionTooltip = Instantiate(tooltipAction, transform.position + new Vector3(0f, 1f), Quaternion.identity);
        actionTooltip.transform.SetParent(transform);
        actionTooltip.SetActive(false);
    }

    void OnMouseDown()
    {
        if (selector.state == 1)
        {
            SpawnUnit(selector.GetSelectUnit(),"PlayerUnit");
        }
        else if (selector.state == 0)
        {
            if (!boardCon.GetUnit(x, y))
                ShowTextToolTip("No Action!");
            else
            {
                ShowActionToolTip();
                boardCon.GetUnit(x, y).GetComponent<UnitMovement>().Stop();
            }
        }else if (selector.state == 2)
        {
            if (boardCon.CanMoveInto(x, y))
                selector.GetCurrentUnit().GetComponent<UnitMovement>().SetTarget(x, y);
            else
                ShowTextToolTip("Can't Move to here");
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

    void ShowActionToolTip()
    {
        transform.GetChild(1).position = boardCon.GetUnit(x, y).GetComponent<UnitMovement>().transform.position+boardCon.GetUnit(x, y).GetComponent<UnitMovement>().offsetVector*2;
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void SpawnUnit(GameObject unit,string type)
    {
        if (boardCon.SpawnUnit(x, y, unit,type))
            ShowTextToolTip("Unit is spawn!!");
        else
            ShowTextToolTip("Invalid Tile!");
    }
}
