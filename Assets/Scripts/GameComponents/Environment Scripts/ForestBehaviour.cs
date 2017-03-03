using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBehaviour : MonoBehaviour {

    public Sprite[] forestSprites;

    int healthPoint;
	// Use this for initialization
	void Start () {
        RandomTypeOfForest();
	}
	
    void RandomTypeOfForest()
    {
        int spriteIndex = Random.Range(0,forestSprites.Length);
        GetComponent<SpriteRenderer>().sprite = forestSprites[spriteIndex];
        if (spriteIndex < 8)
            transform.position = transform.position - new Vector3(0, 0.025f);
        healthPoint = spriteIndex < 8 ? 2 : 3;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
