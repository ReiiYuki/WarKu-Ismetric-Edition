using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour {

    public GameObject tooltipText;

    int x, y;

	// Use this for initialization
	void Start () {
        InitializeToolTip();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitializeToolTip()
    {
        GameObject tooltip = Instantiate(tooltipText, transform.position + new Vector3(0f, 1f), Quaternion.identity);
        tooltip.transform.SetParent(transform);
        tooltip.SetActive(false);
    }

    void OnMouseDown()
    {
        if (GameObject.FindGameObjectWithTag("Core").GetComponent<Selector>().state == 1)
        {
            if (!transform.parent.GetComponent<BoardEnvironmentController>().GetUnit(x, y) && GameObject.FindGameObjectWithTag("Board").GetComponent<BoardEnvironmentController>().IsSpawnZone(x, y))
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.GetComponentInChildren<TextMesh>().text = "Unit Spawn!";
                GameObject.FindGameObjectWithTag("Board").GetComponent<BoardEnvironmentController>().SpawnUnit(x, y, GameObject.FindGameObjectWithTag("Core").GetComponent<Selector>().GetSelectUnit());
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.GetComponentInChildren<TextMesh>().text = "Invalid Tile!";
            }
        }
        else if (GameObject.FindGameObjectWithTag("Core").GetComponent<Selector>().state == 0)
            if (!transform.parent.GetComponent<BoardEnvironmentController>().GetUnit(x, y))
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.GetComponentInChildren<TextMesh>().text = "No Action!";
            }
        GameObject.FindGameObjectWithTag("Core").GetComponent<Selector>().state = 0;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void SetPosition(int x,int y)
    {
        this.x = x;
        this.y = y;
    }
}
