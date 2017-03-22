using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour {

    float speed = 1;
    string direction = "u";
    int offset;
    public int x,y,targetX,targetY;

    Vector3 right = new Vector3(2f, 1f);
    Vector3 down = new Vector3(2f, -1f);
    public Vector3 offsetVector;

    BoardEnvironmentController boardCon;

    // Use this for initialization
    void Start () {
        ConnectToBoardController();
        offsetVector = new Vector3(0, GetComponent<SpriteRenderer>().sprite.bounds.size.y/2 );
        transform.position += offsetVector;
        targetX = -999;
        targetY = -999;
    }
	
    void ConnectToBoardController()
    {
        boardCon = GameObject.FindGameObjectWithTag("Board").GetComponent<BoardEnvironmentController>();
    }

    // Update is called once per frame
    void Update () {
        Move();
        MoveToTarget();
        UpdatePosition();
	}
    
    public void SetPosition(int x,int y)
    {
        this.x = x;
        this.y = y;
    }

    void UpdatePosition()
    {
        if (direction == "r")
            CheckAndUpdatePosition( x - 1,y, transform.position.x > boardCon.GetPosition(x-1, y).x );
        else if (direction == "l")
            CheckAndUpdatePosition(x + 1, y, transform.position.x < boardCon.GetPosition(x+1, y).x );
        else if (direction == "d")
            CheckAndUpdatePosition(x, y + 1, transform.position.x > boardCon.GetPosition(x, y+1).x );
        else if (direction == "u")
            CheckAndUpdatePosition(x, y - 1, transform.position.x < boardCon.GetPosition(x, y-1).x );
    }

    void CheckAndUpdatePosition(int changeX, int changeY,bool condition)
    {
        if (condition)
        {
            boardCon.MoveSprite(x, y, changeX, changeY);
            x = changeX;
            y = changeY;
        }
    }

    void Move()
    {
        if (direction == "r" && !boardCon.IsLowerBound(x) && boardCon.CanMoveInto(x - 1, y))
            transform.Translate(right * Time.deltaTime * speed );
        else if (direction == "l" && !boardCon.IsUpperBound(x) && boardCon.CanMoveInto(x + 1, y))
            transform.Translate(right * Time.deltaTime * speed * -1 + GetDifferentZ(boardCon.GetPositionOfTile(x + 1, y)));
        else if (direction == "d" && !boardCon.IsUpperBound(y) && boardCon.CanMoveInto(x, y + 1))
            transform.Translate(down * Time.deltaTime * speed + GetDifferentZ(boardCon.GetPositionOfTile(x , y+1)));
        else if (direction == "u" && !boardCon.IsLowerBound(y) && boardCon.CanMoveInto(x, y - 1))
            transform.Translate(down * Time.deltaTime * speed * -1 );
        else
            direction = "s";
    }

    public void Stop()
    {
        direction = "s";
        transform.position = boardCon.GetPositionOfTile(x, y)+offsetVector;  
    }

    public void SetDirection(string direction)
    {
        this.direction = direction;
    }

    void MoveToTarget()
    {
        if (targetX > -1 && targetY > -1)
        {
            if (y > targetY)
            {
                SetDirection("u");
            }else if (y < targetY)
            {
                SetDirection("d");
            }
            else if (x > targetX)
            {
                SetDirection("r");
            }else if (x < targetX)
            {
                SetDirection("l");
            }else 
            {
                Stop();
                targetX = -999;
                targetY = -999;
            }
        }
    }

    public void SetTarget(int x,int y)
    {
        targetX = x;
        targetY = y;
    }

    Vector3 GetDifferentZ(Vector3 nextTilePosition)
    {
        return new Vector3(0f, 0f, nextTilePosition.z - transform.position.z);
    }
}
