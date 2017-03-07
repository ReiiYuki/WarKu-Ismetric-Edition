using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

    GameObject[,] boardFloorObject, boardUnitObject;
    Vector3[,] boardPosition;

	// Use this for initialization
	void Start () {
		
	}
	
    public void SetPosition(GameObject[,] boardFloorObject,GameObject[,] boardUnitObject,Vector3[,] boardPosition)
    {
        this.boardFloorObject = boardFloorObject;
        this.boardUnitObject = boardUnitObject;
        this.boardPosition = boardPosition;
    }

	// Update is called once per frame
	void Update () {
		
	}

    public Vector3 GetPosition(int x,int y)
    {
        return boardPosition[x, y];
    }
}
