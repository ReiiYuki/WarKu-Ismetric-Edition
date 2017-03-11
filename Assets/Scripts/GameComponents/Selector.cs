using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

    /**
     * 0 = Non Select
     * 1 = Spawn button clicked
     * 2 = Unit clicked
     */
    public int state = 0;
    GameObject selectedUnit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
