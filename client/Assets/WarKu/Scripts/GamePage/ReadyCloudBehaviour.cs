using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyCloudBehaviour : MonoBehaviour {

    bool isReady = false;
    bool isStart = false;

    // Update is called once per frame
    private void Start()
    {
        transform.parent.GetComponent<Animator>().SetTrigger("Ready");
    }

    void Update () {
		if (DGTProxyRemote.GetInstance().IsStart() && !isStart)
        {
            isStart = true;
            transform.parent.GetComponent<Animator>().SetTrigger("Start");
        }
        if (transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("PreState")&&!isReady)
        {
            isReady = true;
            DGTProxyRemote.GetInstance().Ready();
        }
	}
    
}
