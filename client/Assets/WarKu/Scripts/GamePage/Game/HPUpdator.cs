using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUpdator : MonoBehaviour {

    public GameObject P1Gauge, P2Gauge;

    public void UpdateHP(float p1Hp,float p2Hp)
    {
        Debug.Log(p1Hp + " hp " + p2Hp);
        float p1Ratio = p1Hp;
        float p2Ratio = p2Hp;
        Debug.Log(P1Gauge.GetComponent<RectTransform>().localScale);
        P1Gauge.GetComponent<RectTransform>().localScale = new Vector3(p1Ratio, 1, 1);
        Debug.Log(P1Gauge.GetComponent<RectTransform>().localScale);
        P2Gauge.GetComponent<RectTransform>().localScale = new Vector3(p2Ratio, 1, 1);
    }
}
