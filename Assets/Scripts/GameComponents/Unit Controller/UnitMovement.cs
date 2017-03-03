using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour {

    public float speed;

    public string direction;
    Vector3 right = new Vector3(2f, 1f);
    Vector3 down = new Vector3(1f, -2f);

    // Use this for initialization
    void Start () {
        direction = "s";
	}
	
	// Update is called once per frame
	void Update () {
        if (direction == "r")
            transform.Translate(right * Time.deltaTime * speed);
        else if (direction == "l")
            transform.Translate(right * Time.deltaTime * speed * -1);
        else if (direction == "d")
            transform.Translate(down * Time.deltaTime * speed);
        else if (direction == "u")
            transform.Translate(down * Time.deltaTime * speed * -1);
	}
}
