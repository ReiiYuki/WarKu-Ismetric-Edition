﻿using System.Collections;
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

    public void UpdatePosition()
    {
        if (direction == "r")
        {
            if (transform.position.x > boardCon.GetPosition(x - 1, y).x)
            {
                x -= 1;
            }
        } else if (direction == "l")
        {
            if (transform.position.x < boardCon.GetPosition(x + 1, y).x)
            {
                x += 1;
            }
        } else if (direction == "d")
        {
            if (transform.position.x > boardCon.GetPosition(x, y+1).x)
            {
                y += 1;
            }
        } else if (direction == "u")
        {
            if (transform.position.x < boardCon.GetPosition(x, y - 1).x)
            {
                y -= 1;
            }
        }
    }

    void Move()
    {
        if (direction == "r" && !boardCon.IsLowerBound(x))
            transform.Translate(right * Time.deltaTime * speed);
        else if (direction == "l" && !boardCon.IsUpperBound(x))
            transform.Translate(right * Time.deltaTime * speed * -1);
        else if (direction == "d" && !boardCon.IsUpperBound(y))
            transform.Translate(down * Time.deltaTime * speed);
        else if (direction == "u" && !boardCon.IsLowerBound(y))
            transform.Translate(down * Time.deltaTime * speed * -1);
        else
            direction = "s";
    }
}
