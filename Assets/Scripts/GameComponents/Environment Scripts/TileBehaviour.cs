using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour {

    public GameObject selectText;

    int x, y;

	// Use this for initialization
	void Start () {
        GameObject selectBox = Instantiate(selectText,transform.position+new Vector3(0f,1f),Quaternion.identity);
        selectBox.transform.SetParent(transform);
        selectBox.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void SetPosition(int x,int y)
    {
        this.x = x;
        this.y = y;
    }
}
