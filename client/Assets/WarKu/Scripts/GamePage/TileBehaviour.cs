using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour {

    public bool canMove;
    int x, y;

    public void SetPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    void OnMouseDown()
    {
        if (GameObject.FindObjectOfType<Selector>().IsCreation())
        {
            DGTProxyRemote.GetInstance().RequestSpawnUnit(x, y, GameObject.FindObjectOfType<Selector>().GetUnitCreationType());
            GameObject.FindObjectOfType<Selector>().ResetState();
        }
        else if (GameObject.FindObjectOfType<Selector>().IsListen())
        {
            GameObject.FindObjectOfType<Selector>().GetWillMoveUnit().GetComponent<UnitBehaviour>().SetTarget(x, y);
            GameObject.FindObjectOfType<Selector>().ResetState();
            Debug.Log("Move");
        }
        else
        {
            if (GetComponentInChildren<UnitBehaviour>())
            {
                GameObject.FindObjectOfType<Selector>().ReadyToMove(transform.GetChild(0).gameObject);
                Debug.Log("Select");
            }
        }
    }

    public void OnSpawnUnit(int status)
    {
        if (status == -1)
            Debug.Log("Nooooooooooooooooo");
        else
            Debug.Log("Spawn");
    }
}
