using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUpdator : MonoBehaviour {

	public void SetTime(int time)
    {
        GetComponent<Text>().text = string.Format("{0:00}", time);
    }
}
