using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour {

    public Texture2D cursor; 

    public void Perform()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        GameObject.FindObjectOfType<Selector>().ReadyToMove(GameObject.FindObjectOfType<Selector>().GetCurrentTile().GetComponentInChildren<UnitBehaviour>().gameObject);
        GameObject.FindObjectOfType<ToolTipManager>().HideToolTip();
    }
}
