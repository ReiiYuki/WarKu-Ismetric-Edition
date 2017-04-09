using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour {

    public GameObject notification;

    public AudioClip epicEndline,windSound,drawSound,loseSound,spawnSound;

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
        notification.GetComponent<AudioSource>().clip = spawnSound;
        notification.GetComponent<AudioSource>().Play();
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
        notification.GetComponent<AudioSource>().clip = windSound;
        notification.GetComponent<AudioSource>().Play();
    }

    public void NotifyDraw()
    {
        notification.SetActive(true);
        notification.GetComponentInChildren<Text>().text = "RETREAT !";
        notification.GetComponentInChildren<Image>().color = new Color(231f / 255, 177f / 255, 0f);
        notification.GetComponent<AudioSource>().clip = drawSound;
        notification.GetComponent<AudioSource>().Play();
    }

    public void NotifyLose()
    {
        notification.SetActive(true);
        notification.GetComponentInChildren<Text>().text = "WE LOST THE LAND !";
        notification.GetComponentInChildren<Image>().color = new Color(212f / 255, 0, 0);
        notification.GetComponent<AudioSource>().clip = loseSound;
        notification.GetComponent<AudioSource>().Play();
    }

    public void NotifyNewBuilding()
    {
        notification.SetActive(true);
        notification.GetComponentInChildren<Text>().text = "NEW BUILDING !";
        notification.GetComponentInChildren<Image>().color = new Color(94f / 255, 228f/255, 1f);
    }

    public void NotifyEndLine()
    {
        notification.SetActive(true);
        notification.GetComponentInChildren<Text>().text = "WE REACH ENEMY DEADLINE !";
        notification.GetComponentInChildren<Image>().color = new Color(132f / 255, 0, 231f/255);
        notification.GetComponent<AudioSource>().clip = epicEndline;
        notification.GetComponent<AudioSource>().Play();
    }

    public void NotifyEnemyEndLine()
    {
        notification.SetActive(true);
        notification.GetComponentInChildren<Text>().text = "ENEMY REACH OUR DEADLINE !";
        notification.GetComponentInChildren<Image>().color = new Color(1f, 0, 0);
        notification.GetComponent<AudioSource>().clip = epicEndline;
        notification.GetComponent<AudioSource>().Play();
    }
}
