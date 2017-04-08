using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour {

    public Texture2D cursor;
    public GameObject tooltip,explosion;
    public bool canMove;
    public int x, y;

    void Start()
    {
       // tooltip = Instantiate(tooltip, transform.position+new Vector3(0f,1.5f),Quaternion.identity);
        explosion = Instantiate(explosion, transform.position + new Vector3(0f, 0.35f), Quaternion.identity);
        explosion.transform.SetParent(transform);
        explosion.SetActive(false);
/*        tooltip.transform.SetParent(transform);
        tooltip.SetActive(false);*/
    }

    void Update()
    {
        if (GetComponentInChildren<Animator>())
            if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Out"))
                explosion.SetActive(false);
    }

    public void SetPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    void OnMouseDown()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        if (GameObject.FindObjectOfType<Selector>().IsCreation())
        {
            DGTProxyRemote.GetInstance().RequestSpawnUnit(x, y, GameObject.FindObjectOfType<Selector>().GetUnitCreationType());
            GameObject.FindObjectOfType<Selector>().ResetState();
        }
        else if (GameObject.FindObjectOfType<Selector>().IsListen())
        {
            GameObject.FindObjectOfType<Selector>().GetWillMoveUnit().GetComponent<UnitBehaviour>().SetTarget(x, y);
            GameObject.FindObjectOfType<Selector>().ResetState();
        }
        else if (GameObject.FindObjectOfType<Selector>().IsBuild())
        {
            TileBehaviour currentTile = GameObject.FindObjectOfType<Selector>().GetCurrentTile().GetComponent<TileBehaviour>();
            if (x== currentTile.x+1|| x == currentTile.x - 1 || y == currentTile.y + 1 || y == currentTile.y - 1)
            {
                DGTProxyRemote.GetInstance().BuildRequest(currentTile.x, currentTile.y, x, y);
            } 
            GameObject.FindObjectOfType<Selector>().ResetState();
        }
        else
        {
            if (GetComponentInChildren<UnitBehaviour>() && GetComponentInChildren<UnitBehaviour>().isOwner)
            {
                GetComponentInChildren<UnitBehaviour>().Stop();
                if (GetComponentInChildren<UnitBehaviour>().direction == 0)
                {
                    GameObject.FindObjectOfType<Selector>().SetCurrentTile(gameObject);
                    GameObject.FindObjectOfType<ToolTipManager>().ShowToolTip();
                }
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

    public void Explosion()
    {
        explosion.SetActive(true);
    }

    void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = new Color(96/255f, 203/255f, 255/255f,255/255f);
    }

    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f,1f);
    }

}
