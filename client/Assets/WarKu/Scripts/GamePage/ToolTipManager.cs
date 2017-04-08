using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipManager : MonoBehaviour {

    public GameObject tooltip;

	public void ShowToolTip()
    {
        tooltip.SetActive(true);
    }

    public void HideToolTip()
    {
        tooltip.SetActive(false);
    }
}
