using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private Vector3 ResetCamera;
    private Vector3 Origin;
    private Vector3 Diference;
    private bool Drag = false;
    void Start()
    {
        ResetCamera = transform.position;
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Diference = new Vector3((Camera.main.ScreenToWorldPoint(Input.mousePosition)).x, 0, transform.position.z) - transform.position;
            if (Drag == false)
            {
                Drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Origin = new Vector3(Origin.x, 0f, Origin.z);
            }
        }
        else
        {
            Drag = false;
        }
        if (Drag == true)
        {
            Vector3 diff = Origin - Diference;
            if (diff.x < -8) diff = new Vector3(-8f, 0, diff.z);
            if (diff.x>8) diff = new Vector3(8f, 0, diff.z);
            transform.position = diff;
        }
        //RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
        if (Input.GetMouseButton(1))
        {
            transform.position = ResetCamera;
        }
    }
}
