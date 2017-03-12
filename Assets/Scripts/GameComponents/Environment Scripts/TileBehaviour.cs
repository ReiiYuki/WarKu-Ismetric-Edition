using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour {

    public GameObject selectText;

    GameObject selectBox;
    int x, y;

	// Use this for initialization
	void Start () {
        selectBox = Instantiate(selectText,transform.position+new Vector3(0f,1f),Quaternion.identity);
        selectBox.transform.SetParent(transform);
        selectBox.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        if (GameObject.FindGameObjectWithTag("Core").GetComponent<Selector>().state == 1)
        {
            if (!transform.parent.GetComponent<BoardEnvironmentController>().GetUnit(x, y))
                GameObject.FindGameObjectWithTag("Board").GetComponent<BoardEnvironmentController>().SpawnUnit(x, y, GameObject.FindGameObjectWithTag("Core").GetComponent<Selector>().GetSelectUnit());
        }
        else if (GameObject.FindGameObjectWithTag("Core").GetComponent<Selector>().state == 0)
            transform.GetChild(0).gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("Core").GetComponent<Selector>().state = 0;
    }

    public void SetPosition(int x,int y)
    {
        this.x = x;
        this.y = y;
    }
}
