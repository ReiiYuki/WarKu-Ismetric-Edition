using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnit : MonoBehaviour {
    public Texture2D cursor;
    public int unitType;

	public void SelectUnit()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        GameObject.FindObjectOfType<Selector>().SelectUnit(unitType);
    }

}
