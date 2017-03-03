using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour {

    public float speed;

    string direction;
    Vector3 right = new Vector3(2f, 1f);
    Vector3 down = new Vector3(1f, -2f);

    // Use this for initialization
    void Start () {
        direction = "s";
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(1f, -2f) * Time.deltaTime*speed);
	}
}
