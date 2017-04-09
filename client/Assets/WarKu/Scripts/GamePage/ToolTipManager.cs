using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipManager : MonoBehaviour {

    public GameObject tooltip;

	public void ShowToolTip()
    {
        tooltip.SetActive(true);
        if (GameObject.FindObjectOfType<Selector>().GetCurrentTile().GetComponent<TileBehaviour>().canHide) tooltip.GetComponentsInChildren<Button>()[2].interactable = true;
        else tooltip.GetComponentsInChildren<Button>()[2].interactable = false;
        if (GameObject.FindObjectOfType<Selector>().GetCurrentTile().GetComponentInChildren<UnitBehaviour>().type==0) tooltip.GetComponentsInChildren<Button>()[1].interactable = true;
        else tooltip.GetComponentsInChildren<Button>()[2].interactable = false;
    }

    public void HideToolTip()
    {
        tooltip.SetActive(false);
    }
}
