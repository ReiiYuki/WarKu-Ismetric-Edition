using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour {

    float speed = 1;
    string direction;
    int offset;
    public int x,y,targetX,targetY;

    Vector3 right = new Vector3(2f, 1f);
    Vector3 down = new Vector3(2f, -1f);
    public Vector3 offsetVector;

    List<int[]> path;
    bool canWalk;

    BoardEnvironmentController boardCon;

    // Use this for initialization
    void Start () {
        ConnectToBoardController();
        offsetVector = new Vector3(0, GetComponent<SpriteRenderer>().sprite.bounds.size.y/2 );
        transform.position += offsetVector;
        path = new List<int[]>();
        targetX = -999;
        targetY = -999;
        canWalk = false;
        direction = tag == "PlayerUnit" ? "u" : "d";
        if (tag == "EnemyUnit")
        {
            int randomTile = Random.Range(0, boardCon.BOARD_SIZE);
            Debug.Log(randomTile);
            SetTarget(randomTile, boardCon.BOARD_SIZE - 1);
        }
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
            transform.Translate(right * Time.deltaTime * speed);
        else if (direction == "l" && !boardCon.IsUpperBound(x) && boardCon.CanMoveInto(x + 1, y))
            transform.Translate(right * Time.deltaTime * speed * -1 + GetDifferentZ(boardCon.GetPositionOfTile(x + 1, y)));
        else if (direction == "d" && !boardCon.IsUpperBound(y) && boardCon.CanMoveInto(x, y + 1))
            transform.Translate(down * Time.deltaTime * speed + GetDifferentZ(boardCon.GetPositionOfTile(x, y + 1)));
        else if (direction == "u" && !boardCon.IsLowerBound(y) && boardCon.CanMoveInto(x, y - 1))
            transform.Translate(down * Time.deltaTime * speed * -1);
        else
            Stop();
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
                if (canWalk && path.Count > 0)
                {
                    targetX = path[0][0];
                    targetY = path[0][1];
                    path.RemoveAt(0);
                    if (path.Count == 0)
                    {
                        canWalk = false;
                    }
                }
            }
        }
    }

    public void SetTarget(int toX,int toY)
    {
        targetX = x;
        targetY = y;
        UpdatePath(toX, toY);
    }

    Vector3 GetDifferentZ(Vector3 nextTilePosition)
    {
        return new Vector3(0f, 0f, nextTilePosition.z - transform.position.z);
    }

    public void UpdatePath(int toX,int toY)
    {
        ConnectToBoardController();
        path = new List<int[]>();
        Dictionary<string, int[]> history = new Dictionary<string, int[]>();
        List<int[]> queue = new List<int[]>();
        List<string> marker = new List<string>();
        marker.Add(x+" "+y);
        queue.Add(new int[] { x, y });
        while (queue.Count > 0)
        {
            int[] current = queue[0];
            queue.RemoveAt(0);
            int x = current[0];
            int y = current[1];
            if (x == toX && y == toY)
                break;
            else
            {
                if (!marker.Contains((x+1)+" "+y)&& boardCon.CanMoveInto(x + 1, y))
                {
                    marker.Add(( x + 1)+" " +y );
                    queue.Add(new int[] { x + 1, y });
                    history[(x+1)+" "+y] = current;
                }
                if (!marker.Contains(( x - 1)+" "+ y )&&  boardCon.CanMoveInto(x - 1, y))
                {
                    marker.Add(( x - 1)+" "+ y );
                    queue.Add(new int[] { x - 1, y });
                    history[(x-1)+" "+y] = current;
                }
                if (!marker.Contains( x+" "+( y + 1 )) && boardCon.CanMoveInto(x , y+1))
                {
                    marker.Add( x+" "+( y + 1 ));
                    queue.Add(new int[] { x, y + 1 });
                    history[x+" "+(y+1)] =  current;
                }
                if (!marker.Contains( x +" "+( y - 1 ))  && boardCon.CanMoveInto(x , y-1))
                {
                    marker.Add(x+" "+( y - 1 ));
                    queue.Add(new int[] { x, y - 1 });
                    history[x+" "+(y-1)] = current;
                }
            }
        }
        int[] now = new int[] { toX, toY };
        path.Add(now);
        while (history.ContainsKey(now[0]+" "+now[1]))
        {
            now = history[now[0]+" "+now[1]];
            path.Add(now);
        }
        path.Reverse();
        canWalk = true;
    }

}
