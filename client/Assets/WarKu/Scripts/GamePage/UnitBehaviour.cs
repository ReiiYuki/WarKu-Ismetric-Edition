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
    public Vector3 offsetVector;
    Vector3 right = new Vector3(2f, 1f);
    Vector3 down = new Vector3(2f, -1f);
    List<int[]> path;
    #endregion

    #region Ordinary
    // Use this for initialization
    void Start () {
        offsetVector = new Vector3(0, GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2);
        path = new List<int[]>();
        transform.position += offsetVector;
    }
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    #endregion

    #region Setter
    public void SetDirection(int direction)
    {
        this.direction = direction;
    }

    public void SetPosition(int x,int y)
    {
        this.x = x;
        this.y = y;
    }
    #endregion

    #region Move
    void Move()
    {
        if (direction == (int)Direction.RIGHT)
            transform.Translate(right * Time.deltaTime * speed);
        else if (direction == (int)Direction.LEFT)
            transform.Translate(right * Time.deltaTime * speed * -1 + GetDifferentZ(GameObject.FindObjectOfType<BoardController>().GetPositionOfTile(x + 1, y)));
        else if (direction == (int)Direction.DOWN )
            transform.Translate(down * Time.deltaTime * speed + GetDifferentZ(GameObject.FindObjectOfType<BoardController>().GetPositionOfTile(x , y+1)));
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
            CheckAndUpdatePosition(transform.position.x > GameObject.FindObjectOfType<BoardController>().GetPosition(x, y + 1).x);
        else if (direction == (int)Direction.UP)
            CheckAndUpdatePosition(transform.position.x < GameObject.FindObjectOfType<BoardController>().GetPosition(x, y - 1).x);
        else
            transform.position = GameObject.FindObjectOfType<BoardController>().GetPosition(x, y) + offsetVector;
    }

    void CheckAndUpdatePosition( bool condition)
    {
        if (condition)
        {
            DGTProxyRemote.GetInstance().RequestUpdateUnit(x, y);
        }
    }

    public void Stop()
    {
        path.Clear();
    }

    public void SetTarget(int toX,int toY)
    {
        UpdatePath(toX, toY);
    }

    public void UpdatePath(int toX, int toY)
    {
        path = new List<int[]>();
        Dictionary<string, int[]> history = new Dictionary<string, int[]>();
        List<int[]> queue = new List<int[]>();
        List<string> marker = new List<string>();
        marker.Add(x + " " + y);
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
                if (!marker.Contains((x + 1) + " " + y) && GameObject.FindObjectOfType<BoardController>().CanMoveInto(x + 1, y))
                {
                    marker.Add((x + 1) + " " + y);
                    queue.Add(new int[] { x + 1, y });
                    history[(x + 1) + " " + y] = current;
                }
                if (!marker.Contains((x - 1) + " " + y) && GameObject.FindObjectOfType<BoardController>().CanMoveInto(x - 1, y))
                {
                    marker.Add((x - 1) + " " + y);
                    queue.Add(new int[] { x - 1, y });
                    history[(x - 1) + " " + y] = current;
                }
                if (!marker.Contains(x + " " + (y + 1)) && GameObject.FindObjectOfType<BoardController>().CanMoveInto(x, y + 1))
                {
                    marker.Add(x + " " + (y + 1));
                    queue.Add(new int[] { x, y + 1 });
                    history[x + " " + (y + 1)] = current;
                }
                if (!marker.Contains(x + " " + (y - 1)) && GameObject.FindObjectOfType<BoardController>().CanMoveInto(x, y - 1))
                {
                    marker.Add(x + " " + (y - 1));
                    queue.Add(new int[] { x, y - 1 });
                    history[x + " " + (y - 1)] = current;
                }
            }
        }
        int[] now = new int[] { toX, toY };
        path.Add(now);
        while (history.ContainsKey(now[0] + " " + now[1]))
        {
            now = history[now[0] + " " + now[1]];
            path.Add(now);
        }
        path.Reverse();
        path.RemoveAt(0);
        UpdateDirection();
    }

    public void UpdateDirection()
    {
        if (path.Count > 0)
        {
            int targetX = path[0][0];
            int targetY = path[0][1];
            path.RemoveAt(0);
            if (x == targetX && y < targetY) DGTProxyRemote.GetInstance().RequestChangeDirection(x, y, 4);
            else if (x == targetX && y > targetY) DGTProxyRemote.GetInstance().RequestChangeDirection(x, y, 3);
            else if (y == targetY && x > targetX) DGTProxyRemote.GetInstance().RequestChangeDirection(x, y, 1);
            else if (y == targetY && x < targetX) DGTProxyRemote.GetInstance().RequestChangeDirection(x, y, 2);
        }
    }

    Vector3 GetDifferentZ(Vector3 nextTilePosition)
    {
        return new Vector3(0f, 0f, nextTilePosition.z - transform.position.z);
    }
    #endregion
}
