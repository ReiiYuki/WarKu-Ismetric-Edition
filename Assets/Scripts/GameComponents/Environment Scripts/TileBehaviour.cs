using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour {

    int x, y;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        Debug.Log(x+ " " + y);
    }

    public GameObject SetPosition(int x,int y)
    {
        this.x = x;
        this.y = y;
        return gameObject;
    }
}
