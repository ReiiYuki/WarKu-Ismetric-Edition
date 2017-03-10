using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUIAnimation : MonoBehaviour {

    float size = 0;
    float time = 0;

	// Use this for initialization
	void Start () {
        GetComponentInChildren<MeshRenderer>().sortingLayerName = "UpperUI";
    }
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.y<1)
        {
            transform.localScale += new Vector3(0f, Time.deltaTime*10 );
            StartCoroutine(Hide());
        }
	}

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
