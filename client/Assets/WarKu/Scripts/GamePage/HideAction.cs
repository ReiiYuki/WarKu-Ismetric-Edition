using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAction : MonoBehaviour {

    public void Perform()
    {
        GameObject.FindObjectOfType<Selector>().GetCurrentTile().GetComponentInChildren<UnitBehaviour>().HideRequest();
        GameObject.FindObjectOfType<ToolTipManager>().HideToolTip();
    }

}
