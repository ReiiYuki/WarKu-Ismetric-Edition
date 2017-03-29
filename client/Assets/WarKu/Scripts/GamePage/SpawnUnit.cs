using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnit : MonoBehaviour {

    public int unitType;

	public void SelectUnit()
    {
        //TODO Add Cursor Changing Here
        GameObject.FindObjectOfType<Selector>().SelectUnit(unitType);
    }

}
