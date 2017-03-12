using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer : MonoBehaviour {

    public float scrollSpeed = 10f;
    public float cameraDistance = 7f;
    public float maxCameraDistance = 7f;
    public float minCameraDistance = 3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        cameraDistance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        cameraDistance = Mathf.Clamp(cameraDistance, minCameraDistance, maxCameraDistance);
        Debug.Log(cameraDistance);
        Camera.main.orthographicSize = cameraDistance;
    }
}
