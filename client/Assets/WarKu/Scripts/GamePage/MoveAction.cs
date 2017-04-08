using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour {

    public void Perform()
    {
        GameObject.FindObjectOfType<Selector>().ReadyToMove(GameObject.FindObjectOfType<Selector>().GetCurrentTile().GetComponentInChildren<UnitBehaviour>().gameObject);
        GameObject.FindObjectOfType<ToolTipManager>().HideToolTip();
    }
}
