using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer : MonoBehaviour {

    public float scrollSpeed = 10f;
    public float cameraDistance = 7f;
    public float maxCameraDistance = 7f;
    public float minCameraDistance = 3f;
    public bool onTile = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Mouse ScrollWheel") != 0 && onTile)
        {
            cameraDistance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
            cameraDistance = Mathf.Clamp(cameraDistance, minCameraDistance, maxCameraDistance);
            Camera.main.orthographicSize = cameraDistance;
            Camera.main.transform.position += (Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position);
        }
    }
}
