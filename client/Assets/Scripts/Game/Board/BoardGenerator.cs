using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour {

    Board board;

	// Use this for initialization
	void Start () {
        board = GetComponent<Board>();
	}
	
    void PlaceRiver()
    {
        if (hasRiver())
        {

        }
    }

    bool hasRiver()
    {
        return Random.Range(0, 2) == 1;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
