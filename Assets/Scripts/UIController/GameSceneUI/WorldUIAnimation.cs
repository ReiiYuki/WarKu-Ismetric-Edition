using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUIAnimation : MonoBehaviour {

    float size = 0;
    bool disappear = false;
    float time = 0;

    public bool isText;

	// Use this for initialization
	void Start () {
        SetSortingLayer();
    }
	
    void SetSortingLayer()
    {
        if (isText)
            GetComponentInChildren<MeshRenderer>().sortingLayerName = "UpperUI";
    }

	// Update is called once per frame
	void Update () {
        IncreaseScale();
        Disappear();
	}

    void IncreaseScale()
    {
        if (transform.localScale.y < 1)
        {
            transform.localScale += new Vector3(0f, Time.deltaTime * 10);
        }
        else
        {
            disappear = true;
        }
    }

    void Disappear()
    {
        if (disappear)
        {
            time += Time.deltaTime;
            if (time >= 1)
                gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        Reset();
    }

    void Reset()
    {
        transform.localScale = new Vector3(1f, 0f);
        size = 0;
        disappear = false;
        time = 0;
    }
}
