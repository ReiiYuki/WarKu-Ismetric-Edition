using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildAction : MonoBehaviour {

    void OnMouseDown()
    {
        GameObject.FindObjectOfType<Selector>().ReadyToBuild(transform.parent.parent.gameObject);
        transform.parent.parent.GetChild(0).gameObject.SetActive(false);
    }
}
