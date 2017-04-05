using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBehaviour : MonoBehaviour {

    public Sprite[] forestSprites;
    public float offsetY;

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
        if (offsetY > 0.0011)
            offsetY += 0.038f;
        transform.position -= new Vector3(0, offsetY);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
