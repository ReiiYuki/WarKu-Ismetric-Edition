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

    BoardEnvironmentController boardCon;

    // Use this for initialization
    void Start () {
        ConnectToBoardController();
    }
	
    void ConnectToBoardController()
    {
        boardCon = GameObject.FindGameObjectWithTag("Board").GetComponent<BoardEnvironmentController>();
    }

    /*   void Move() 
       {
           if (y < targetY)
           {
               int nextY = y + 1;
               if (transform.position.x < boardManager.GetPosition(x, nextY).x)
               {
                   direction = "d";
               }else
               {
                   y = nextY;
                   transform.position = boardManager.GetPosition(x, nextY);
               }
           }else if (y > targetY)
           {
               int nextY = y - 1;
               if (transform.position.x > boardManager.GetPosition(x, nextY).x)
               {
                   direction = "u";
               }
               else
               {
                   y = nextY;
                   transform.position = boardManager.GetPosition(x, nextY);
               }
           }
           else
           {
               if (x < targetX)
               {
                   int nextX = x + 1;
                   if (transform.position.x > boardManager.GetPosition(nextX, y).x)
                   {
                       direction = "l";
                   }
                   else
                   {
                       x = nextX;
                       transform.position = boardManager.GetPosition(nextX, y);
                   }
               }
               else if (x > targetX)
               {
                   int nextX = x - 1;
                   if (transform.position.x < boardManager.GetPosition(nextX, y).x)
                   {
                       direction = "r";
                   }
                   else
                   {
                       x = nextX;
                       transform.position = boardManager.GetPosition(nextX, y);
                   }
               }
               else
               {
                   direction = "s";
                   transform.position = boardManager.GetPosition(targetX, targetY);
               }
           }
       }
       */
    // Update is called once per frame
    void Update () {
        Move();
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
            CheckAndUpdatePosition( x - 1,y, transform.position.x < boardCon.GetPosition(x-1, y).x );
        else if (direction == "l")
            CheckAndUpdatePosition(x + 1, y, transform.position.x > boardCon.GetPosition(x+1, y).x );
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
            transform.Translate(right * Time.deltaTime * speed);
        else if (direction == "l" && !boardCon.IsUpperBound(x) && boardCon.CanMoveInto(x + 1, y))
            transform.Translate(right * Time.deltaTime * speed * -1);
        else if (direction == "d" && !boardCon.IsUpperBound(y) && boardCon.CanMoveInto(x, y + 1))
            transform.Translate(down * Time.deltaTime * speed);
        else if (direction == "u" && !boardCon.IsLowerBound(y) && boardCon.CanMoveInto(x, y - 1))
            transform.Translate(down * Time.deltaTime * speed * -1);
        else
            direction = "s";
    }

    public void Stop()
    {
        direction = "s";
        transform.position = boardCon.GetPositionOfTile(x, y);
    }
}
