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
            GetComponent<Animator>().SetTrigger("Start");
        }
	}

    void OnMouseDown()
    {
        isReady = true;
        GetComponent<Animator>().SetTrigger("Ready");
    }
}
