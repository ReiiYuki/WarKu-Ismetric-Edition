using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehaviour : MonoBehaviour {

    public Sprite[] clouds;

    bool isFound = false;

	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sprite = clouds[Random.Range(0, clouds.Length)];
	}

    void Update()
    {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("End"))
            gameObject.SetActive(false);
        if (!isFound&&transform.parent.GetComponentInChildren<UnitBehaviour>())
        {
            isFound = true;
            Found();
        }
    }

	public void Found()
    {
        gameObject.SetActive(false);
        Debug.Log("Found");
        GetComponent<Animator>().SetTrigger("FadeOut");
    }
}
