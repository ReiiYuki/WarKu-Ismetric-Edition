using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildAction : MonoBehaviour {

    public Texture2D cursor;

    public void Perform()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        GameObject.FindObjectOfType<Selector>().ReadyToBuild(GameObject.FindObjectOfType<Selector>().GetCurrentTile());
        GameObject.FindObjectOfType<ToolTipManager>().HideToolTip();
    }
}
