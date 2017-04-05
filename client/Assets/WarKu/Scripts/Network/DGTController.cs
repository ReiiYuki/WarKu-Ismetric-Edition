using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DGTController : MonoBehaviour {

    DGTProxyRemote remote;

	// Use this for initialization
	void Start () {
        StartCoroutine(ConnectToServer());
    }
	
	// Update is called once per frame
	void Update () {
        remote.ProcessEvents();
	}

    IEnumerator ConnectToServer()
    {
        DGTPacket.Config pc = new DGTPacket.Config("54.169.98.129", 1111);
        remote = DGTProxyRemote.GetInstance();
        remote.Connect(pc.host, pc.port);
        remote.ProcessEvents();
        yield return new WaitForSeconds(0.1f);
        for (int i  = 0; i < 10; i++)
        {
            if (remote.IsConnected() || remote.IsConnectionFailed()) break;
            remote.ProcessEvents();
            yield return new WaitForSeconds(0.1f);
        }
        if (remote.IsConnected())
        {
            remote.Login("Kuy");
        }
        else
            Debug.Log("Failed");
        yield break;
    }
}
