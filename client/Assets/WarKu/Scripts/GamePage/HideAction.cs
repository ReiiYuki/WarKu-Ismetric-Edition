using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAction : MonoBehaviour {

    void OnMouseDown()
    {
        transform.parent.parent.GetComponentInChildren<UnitBehaviour>().HideRequest();
        transform.parent.gameObject.SetActive(false);
    }

}
