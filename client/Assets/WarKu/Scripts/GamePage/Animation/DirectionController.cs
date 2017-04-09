using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionController : MonoBehaviour {

    public int direction = 0;
    public GameObject front, back;
    public bool isOwner = true;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (isOwner)
        {
            if (direction == 0)
            {
                BackFirst(true);
                Inverse(back, false);
                State(back, "Idle");
            }
            else if (direction == 1)
            {
                BackFirst(true);
                Inverse(back, true);
                State(back, "Walk");
            }
            else if (direction == 2 || direction == 3)
            {
                BackFirst(true);
                Inverse(back, false);
                State(back, "Walk");
            }
            else if (direction == 4)
            {
                BackFirst(false);
                Inverse(front, true);
                front.transform.localPosition = new Vector3(-3.5f, -3);
                State(front, "Walk");
            }
        }else
        {
            if (direction == 0)
            {
                BackFirst(false);
                Inverse(front, true);
                State(front, "Idle");
            }
            else if (direction == 1)
            {
                BackFirst(false);
                Inverse(front, true);
                State(front, "Walk");
            }
            else if (direction == 2)
            {
                BackFirst(false);
                front.transform.localPosition = new Vector3(3.5f, -3);
                Inverse(front, false);
                State(front, "Walk");
            }
            else if (direction == 3)
            {
                BackFirst(true);
                Inverse(back, false);
                State(back, "Walk");
            }
            else if (direction == 4)
            {
                BackFirst(false);
                Inverse(front, true);
                State(front, "Walk");
            }
            if (direction == 2)
            {
                front.transform.localPosition = new Vector3(3.5f, -3);
            }else
            {
                front.transform.localPosition = new Vector3(-3.5f, -3);
            }
        }
		
	}

    public void BackFirst(bool isBack)
    {
        if (isBack)
        {
            back.SetActive(true);
            front.SetActive(false);
        }else
        {
            back.SetActive(false);
            front.SetActive(true);
        }
    }

    public void Inverse(GameObject obj,bool left)
    {
        if (left)
        {
            if (obj.transform.localScale.x > -1)
                obj.transform.localScale = new Vector3(-1 * obj.transform.localScale.x, obj.transform.localScale.y, obj.transform.localScale.z);
        } else
        {
            if (obj.transform.localScale.x <= -1)
                obj.transform.localScale = new Vector3(-1 * obj.transform.localScale.x, obj.transform.localScale.y, obj.transform.localScale.z);
        }
    }

    public void State(GameObject obj,string state)
    {
        if (!obj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(state))
            obj.GetComponent<Animator>().SetTrigger(state);
    }

    public void Own(bool ownersip)
    {
        this.isOwner = ownersip;
        Debug.Log(ownersip);

        if (this.isOwner==false)
        {
            front.SetActive(true);
            back.SetActive(true);
            Debug.Log(":D");
            foreach (SpriteRenderer ren in GetComponentsInChildren<SpriteRenderer>()) ren.color = new Color(210 / 255f, 173 / 255f, 139 / 255f);
        }

    }
}
