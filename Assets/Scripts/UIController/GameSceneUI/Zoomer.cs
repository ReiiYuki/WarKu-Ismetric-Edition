using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer : MonoBehaviour {

    float scrollSpeed = 10f;
    float cameraDistance = 7f;
    float maxCameraDistance = 7f;
    float minCameraDistance = 3f;

    public bool onTile = false;

    Camera camera;

	// Use this for initialization
	void Start () {
        BindCamera();
	}
	
    void BindCamera()
    {
        camera = Camera.main;
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Mouse ScrollWheel") != 0 && onTile)
            Zoom();
    }

    void Zoom()
    {
        cameraDistance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        cameraDistance = Mathf.Clamp(cameraDistance, minCameraDistance, maxCameraDistance);
        camera.orthographicSize = cameraDistance;
        UpdatePosition();
    }

    void UpdatePosition()
    {
        camera.transform.position += (camera.ScreenToWorldPoint(Input.mousePosition) - camera.transform.position);
    }
}
