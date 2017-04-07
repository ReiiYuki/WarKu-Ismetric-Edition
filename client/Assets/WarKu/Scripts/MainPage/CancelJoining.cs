using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelJoining : MonoBehaviour {

    private void OnMouseDown()
    {
        transform.parent.GetComponent<Animator>().SetTrigger("Cancel");
        GameObject.FindObjectOfType<CreateRoom>().Enable();
    }
}
