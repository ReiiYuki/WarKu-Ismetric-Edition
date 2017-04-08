using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyCloudBehaviour : MonoBehaviour {

    bool isReady = false;
    bool isStart = false;
	
	// Update is called once per frame
	void Update () {
		if (DGTProxyRemote.GetInstance().IsStart() && !isStart)
        {
            isStart = true;
            transform.parent.GetComponent<Animator>().SetTrigger("Start");
        }
	}

    void OnMouseDown()
    {
        isReady = true;
        transform.parent.GetComponent<Animator>().SetTrigger("Ready");
    }
}
