using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBackgroundGenerator : MonoBehaviour {

    public GameObject woodObj;

	// Use this for initialization
	void Start () {
        GenerateWoodBackground();
	}

    public void GenerateWoodBackground()
    {
        for (float i = -20; i <= 20; i++)
        {
            for (float j = -20; j <= 20; j++)
            {
                GameObject wood = Instantiate(woodObj, new Vector3(i, j), Quaternion.identity);
                wood.transform.Rotate(new Vector3(0f, 0f, 90f));
                wood.transform.SetParent(transform);
            }
        }
    }
}
