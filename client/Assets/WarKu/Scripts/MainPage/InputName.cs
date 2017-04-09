using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputName : MonoBehaviour {


    public void Connect()
    {
        string name = GetComponentsInChildren<Text>()[1].text;
        if (name == "") return;
        PlayerPrefs.SetString("name", name);
        DGTProxyRemote.GetInstance().Login(PlayerPrefs.GetString("name"));
        gameObject.SetActive(false);
    }
}
