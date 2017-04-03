using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoom : MonoBehaviour {

	public void Create()
    {
        DGTProxyRemote.GetInstance().CreateRoom(0);
        
    }
}
