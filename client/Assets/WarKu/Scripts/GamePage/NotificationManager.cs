using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour {

    public GameObject notification;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (notification.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            notification.SetActive(false);
        }
	}

    public void NotifySpawnUnit()
    {
        notification.SetActive(true);
        notification.GetComponentInChildren<Text>().text = "Unit Spawn !";
        notification.GetComponentInChildren<Image>().color = new Color(6f / 255, 163f / 255, 0f);
    }

    public void NotifyKillEnemy()
    {
        notification.SetActive(true);
        notification.GetComponentInChildren<Text>().text = "Enemy DIE !";
        notification.GetComponentInChildren<Image>().color = new Color(108f / 255, 3f / 255, 174f/255);
    }

    public void NotifyWin()
    {
        notification.SetActive(true);
        notification.GetComponentInChildren<Text>().text = "VICTORY !";
        notification.GetComponentInChildren<Image>().color = new Color(3f / 255, 96f / 255, 174f/255);
    }

    public void NotifyDraw()
    {
        notification.SetActive(true);
        notification.GetComponentInChildren<Text>().text = "RETREAT !";
        notification.GetComponentInChildren<Image>().color = new Color(231f / 255, 177f / 255, 0f);
    }

    public void NotifyLose()
    {
        notification.SetActive(true);
        notification.GetComponentInChildren<Text>().text = "WE LOST THE LAND !";
        notification.GetComponentInChildren<Image>().color = new Color(212f / 255, 0, 0);
    }
}
