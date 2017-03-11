using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour {

    public GameObject unit;
    public Texture2D cursorTexture;

	// Use this for initialization
	void Start () {
		
	}
	
	public void SpawnSprite()
    {
        Cursor.SetCursor(cursorTexture,Vector2.zero,CursorMode.Auto);
        Debug.Log(unit);
        GameObject.FindGameObjectWithTag("Core").GetComponent<Selector>().SelectUnit(unit);
    }
}
