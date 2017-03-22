using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

    /**
     * 0 = Non Select
     * 1 = Spawn button clicked
     * 2 = Unit clicked
     * 3 = Move Listen  
     */
    public int state = 0;
    GameObject selectedUnit,currentUnit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SelectUnit(GameObject unit)
    {
        selectedUnit = unit;
        state = 1;
    }

    public GameObject GetSelectUnit()
    {
        return selectedUnit;
    }

    public void MoveListen(GameObject unit)
    {
        state = 2;
        currentUnit = unit;
    }

    public void Move()
    {
        state = 3;
    }
}
