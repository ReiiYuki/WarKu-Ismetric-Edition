using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour {

    #region attribute
    enum Direction
    {
        STOP = 0,
        RIGHT = 1,
        LEFT = 2,
        UP = 3,
        DOWN = 4
    }
    int x, y,targetX,targetY;
    Vector3 offsetVector;
    #endregion

    // Use this for initialization
    void Start () {
        offsetVector = new Vector3(0, GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2);
        transform.position += offsetVector;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


}
