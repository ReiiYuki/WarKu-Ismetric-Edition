using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    float time;
    bool readySpawn;

	// Use this for initialization
	void Start () {
        time = 5;
        readySpawn = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (time > 0.5)
            time -= Time.deltaTime/100;
        if (readySpawn)
            StartCoroutine(SpawnByRandomTime());
	}

    void RandomTileSpawn()
    {
        int tile = Random.Range(0, GameObject.FindGameObjectWithTag("Board").GetComponent<BoardEnvironmentController>().BOARD_SIZE);
    }

    IEnumerator SpawnByRandomTime()
    {
        readySpawn = false;
        yield return new WaitForSeconds(time);
        readySpawn = true;
        Debug.Log("Spawn");
    }
}
