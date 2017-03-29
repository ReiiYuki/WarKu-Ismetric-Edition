using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateRoom : MonoBehaviour {

	public void Create()
    {
        DGTProxyRemote.GetInstance().CreateRoom(0);
        SceneManager.LoadScene(1);
    }
}
