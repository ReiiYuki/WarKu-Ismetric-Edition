using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    public GameObject enemyPrefab;

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

    void Spawn()
    {
      //  int tile = Random.Range(0, GameObject.FindGameObjectWithTag("Board").GetComponent<BoardEnvironmentController>().BOARD_SIZE);
     //   GameObject.FindGameObjectWithTag("Board").GetComponent<BoardEnvironmentController>().SpawnUnit(tile, 0,enemyPrefab, "EnemyUnit");
    }

    IEnumerator SpawnByRandomTime()
    {
        readySpawn = false;
        yield return new WaitForSeconds(time);
        readySpawn = true;
        Spawn();
    }
}
