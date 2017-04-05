using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBehaviour : MonoBehaviour {

    public Sprite[] stoneSprites;

	// Use this for initialization
	void Start () {
        RandomTypeOfStone();
	}

    void RandomTypeOfStone()
    {
        int spriteIndex = Random.Range(0, stoneSprites.Length);
        GetComponent<SpriteRenderer>().sprite = stoneSprites[spriteIndex];
    }

    // Update is called once per frame
    void Update () {
		
	}
}
