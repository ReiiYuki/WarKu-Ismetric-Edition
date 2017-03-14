using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponentInChildren<MeshRenderer>().sortingLayerName = "UpperUI";
    }
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Hide"))
            gameObject.SetActive(false);
	}
}
