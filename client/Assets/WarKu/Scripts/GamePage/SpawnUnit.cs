using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnit : MonoBehaviour {

    public int unitType;

	public void SelectUnit()
    {
        GameObject.FindObjectOfType<Selector>().SelectUnit(unitType);
    }

}
