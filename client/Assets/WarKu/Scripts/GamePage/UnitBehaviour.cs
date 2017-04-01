using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour {

    #region attribute
    enum Direction
    {
        STOP = 0,
        RIGHT = 1,
        LEFT = 2,
        UP = 3,
        DOWN = 4
    }
    int x, y,targetX,targetY,direction;
    public int speed = 1;
    Vector3 offsetVector;
    Vector3 right = new Vector3(2f, 1f);
    Vector3 down = new Vector3(2f, -1f);
    #endregion

    // Use this for initialization
    void Start () {
        offsetVector = new Vector3(0, GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2);
        transform.position += offsetVector;
    }
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    public void SetDirection(int direction)
    {
        this.direction = direction;
        Debug.Log(direction);
    }

    public void SetPosition(int x,int y)
    {
        this.x = x;
        this.y = y;
    }

    void Move()
    {
        if (direction == (int)Direction.RIGHT)
            transform.Translate(right * Time.deltaTime * speed);
        else if (direction == (int)Direction.LEFT)
            transform.Translate(right * Time.deltaTime * speed * -1 );
        else if (direction == (int)Direction.DOWN )
            transform.Translate(down * Time.deltaTime * speed );
        else if (direction == (int)Direction.UP )
            transform.Translate(down * Time.deltaTime * speed * -1);
        MoveToNextTile();
    }

    void MoveToNextTile()
    {
        if (direction == (int)Direction.RIGHT)
            CheckAndUpdatePosition(transform.position.x > GameObject.FindObjectOfType<BoardController>().GetPosition(x - 1, y).x);
        else if (direction == (int)Direction.LEFT)
            CheckAndUpdatePosition(transform.position.x < GameObject.FindObjectOfType<BoardController>().GetPosition(x + 1, y).x);
        else if (direction == (int)Direction.DOWN)
            CheckAndUpdatePosition( transform.position.x > GameObject.FindObjectOfType<BoardController>().GetPosition(x, y + 1).x);
        else if (direction == (int)Direction.UP)
            CheckAndUpdatePosition(transform.position.x < GameObject.FindObjectOfType<BoardController>().GetPosition(x, y - 1).x);
    }

    void CheckAndUpdatePosition( bool condition)
    {
        if (condition)
        {
            DGTProxyRemote.GetInstance().RequestUpdateUnit(x, y);
        }
    }

}
