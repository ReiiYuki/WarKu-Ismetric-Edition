using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelJoining : MonoBehaviour {

    private void Update()
    {
        if (transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Done"))
        {
            transform.parent.gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        DGTProxyRemote.GetInstance().CancelRoom();
    }

    public void Cancel()
    {
        transform.parent.GetComponent<Animator>().SetTrigger("Cancel");
        GameObject.FindObjectOfType<CreateRoom>().Enable();
    }
}
