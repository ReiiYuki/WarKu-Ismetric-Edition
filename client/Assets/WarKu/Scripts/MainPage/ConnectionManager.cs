using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionManager : MonoBehaviour {

    public GameObject join, lost,askname,cancel;

	public void ShowJoin()
    {
        join.SetActive(true);
    }

    public void ShowConnectionLost()
    {
        lost.SetActive(true);
    }

    public void AskName()
    {
        askname.SetActive(true);
    }

    public void ShowCancel()
    {
        cancel.SetActive(true);
    }
}
