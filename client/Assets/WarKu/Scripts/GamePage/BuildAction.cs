using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildAction : MonoBehaviour {

    public void Perform()
    {
        GameObject.FindObjectOfType<Selector>().ReadyToBuild(GameObject.FindObjectOfType<Selector>().GetCurrentTile());
        GameObject.FindObjectOfType<ToolTipManager>().HideToolTip();
    }
}
