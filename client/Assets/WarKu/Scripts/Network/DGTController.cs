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
        DGTPacket.Config pc = new DGTPacket.Config("localhost", 1111);
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
            string name = PlayerPrefs.GetString("name");
            Debug.Log(name);
            if (name != "") remote.Login(name);
            else GameObject.FindObjectOfType<ConnectionManager>().AskName();
        }
        else
        {
            GameObject.FindObjectOfType<ConnectionManager>().ShowConnectionLost();
        }
        yield break;
    }
}
