using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBehaviour : MonoBehaviour {

    public Sprite[] forestSprites;
    public float offsetY;

    int healthPoint;
	// Use this for initialization
	void Start () {
        RandomTypeOfForest();
	}
	
    void RandomTypeOfForest()
    {
        int spriteIndex = Random.Range(0,forestSprites.Length);
        offsetY = GetComponent<SpriteRenderer>().sprite.bounds.size.y/10f;
        GetComponent<SpriteRenderer>().sprite = forestSprites[spriteIndex];
        offsetY -= GetComponent<SpriteRenderer>().sprite.bounds.size.y / 10f;
        transform.position -= new Vector3(0, offsetY);
        healthPoint = spriteIndex < 8 ? 2 : 3;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
