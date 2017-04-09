using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoom : MonoBehaviour {

    private bool canCreate = true;

    private void OnMouseDown()
    {
        if (canCreate) Create();
    }

    public void Create()
    {
        DGTProxyRemote.GetInstance().CreateRoom(0);
        GameObject.FindObjectOfType<ConnectionManager>().ShowCancel();
    }

    public void Enable()
    {
        StartCoroutine(ToEnable());
    }

    IEnumerator ToEnable()
    {
        yield return new WaitForSeconds(2f);
        canCreate = true;
    }
}
