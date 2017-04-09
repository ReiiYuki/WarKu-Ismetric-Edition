using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringToFront : MonoBehaviour {

	// Use this for initialization
	void Start () {
        foreach (SpriteRenderer r in GetComponentsInChildren<SpriteRenderer>()) r.sortingLayerName = "Ready";
        foreach (MeshRenderer r in GetComponentsInChildren<MeshRenderer>()) r.sortingLayerName = "Toppest";		
	}
	
}
