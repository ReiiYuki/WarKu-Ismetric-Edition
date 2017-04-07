using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoom : MonoBehaviour {

    private void OnMouseDown()
    {
        Create();
    }

    public void Create()
    {
        DGTProxyRemote.GetInstance().CreateRoom(0);
        GameObject.FindObjectOfType<ConnectionManager>().ShowCancel();
    }
}
