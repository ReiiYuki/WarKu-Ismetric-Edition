using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyCloudBehaviour : MonoBehaviour {

    bool isReady = false;
    bool isStart = false;
    bool isStarting = false;
    bool isStarted = false;
    public GameObject bottomPanel,headerPanel;

    // Update is called once per frame
    private void Start()
    {
        bottomPanel.SetActive(false);
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
        if ( transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("PullOut") && !isStarting) 
        {
            headerPanel.SetActive(true);
            headerPanel.GetComponent<Animator>().SetTrigger("Start");
            isStarting = true;
        }
        if (headerPanel.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Start") && !isStarted)
        {
            bottomPanel.SetActive(true);
            bottomPanel.GetComponent<Animator>().SetTrigger("Start");
            isStarted = true;
        }
	}
    
}
