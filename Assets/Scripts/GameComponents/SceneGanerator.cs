using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGanerator : MonoBehaviour {

    public GameObject landPrototype,rockPrototype,forestPrototype;
    public GameObject riverCurveLeftUp, riverCurveLeftDown, riverCurveRightUp, riverCurveRightDown, riverLeft, riverDown;

    GameObject[,] boardObject;
    Vector3[,] boardPosition;

    const int BOARD_SIZE = 12;

	// Use this for initialization
	void Start () {
        InitializeBoard();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitializeBoard()
    {
        boardObject = new GameObject[BOARD_SIZE, BOARD_SIZE];
        boardPosition = new Vector3[BOARD_SIZE, BOARD_SIZE];
        GeneratePosition();
        GenerateGeo();
    }

    void GeneratePosition()
    {
        const float ORIGIN_Y = 3.5f;
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            float offsetX = i * -0.65f;
            float offsetY = i * -0.325f + ORIGIN_Y;
            for (int j = 0; j < BOARD_SIZE; j++)
                boardPosition[i, j] = new Vector3(j * 0.65f + offsetX, j * -0.325f + offsetY, -1 * (j + i));
        }
    }

    void GenerateGeo()
    {
        GenerateRiver();
        GenerateGround();
    }

    void GenerateRiver()
    {
        bool hasRiver = true;//Mathf.Round(Random.Range(0f, 1f))==1;
        if (hasRiver)
        {
            int y = Random.Range(1, BOARD_SIZE-1);
            int x = 0;
            string currentType;
            int selection = Random.Range(0, 3);
            if (selection == 0)
            {
                boardObject[x, y] = Instantiate(riverLeft, boardPosition[x, y], Quaternion.identity);
                currentType = "l";
            }else if (selection == 1)
            {
                boardObject[x, y] = Instantiate(riverCurveLeftDown, boardPosition[x, y], Quaternion.identity);
                currentType = "ld";
            }else
            {
                boardObject[x, y] = Instantiate(riverCurveRightDown, boardPosition[x, y], Quaternion.identity);
                currentType = "rd";
            }
            while ( (x< BOARD_SIZE  && x>=0) && (y< BOARD_SIZE && y>=0) )
            {
                if (currentType == "ld")
                {
                    selection = Random.Range(0, 2);
                    y++;
                    if (y > BOARD_SIZE-1) break; 
                    if (selection == 0)
                    {
                        boardObject[x, y] = Instantiate(riverCurveRightUp, boardPosition[x, y], Quaternion.identity);
                        currentType = "ru";
                    }else
                    {
                        boardObject[x, y] = Instantiate(riverDown, boardPosition[x, y], Quaternion.identity);
                        currentType = "d";
                    }
                }
                else if (currentType == "rd")
                {
                    y--;
                    if (y < 0) break;
                    boardObject[x, y] = Instantiate(riverCurveLeftUp, boardPosition[x, y], Quaternion.identity);
                    currentType = "lu";
                }
                else if (currentType == "ru")
                {
                    x++;
                    if (x > BOARD_SIZE-1) break;
                    selection = Random.Range(0, 2);
                    if (selection == 0)
                    {
                        boardObject[x, y] = Instantiate(riverCurveLeftDown, boardPosition[x, y], Quaternion.identity);
                        currentType = "ld";
                    }
                    else
                    {
                        boardObject[x, y] = Instantiate(riverLeft, boardPosition[x, y], Quaternion.identity);
                        currentType = "l";
                    }
                }
                else if (currentType == "lu")
                {
                    x++;
                    if (x > BOARD_SIZE-1) break;
                    selection = Random.Range(0, 2);
                    if (selection == 0)
                    {
                        boardObject[x, y] = Instantiate(riverCurveLeftDown, boardPosition[x, y], Quaternion.identity);
                        currentType = "ld";
                    }
                    else
                    {
                        boardObject[x, y] = Instantiate(riverLeft, boardPosition[x, y], Quaternion.identity);
                        currentType = "l";
                    }
                }
                else if (currentType == "d")
                {
                    y++;
                    if (y > BOARD_SIZE-1) break;
                    selection = Random.Range(0, 2);
                    if (selection == 0)
                    {
                        boardObject[x, y] = Instantiate(riverCurveRightUp, boardPosition[x, y], Quaternion.identity);
                        currentType = "ru";
                    }
                    else
                    {
                        boardObject[x, y] = Instantiate(riverDown, boardPosition[x, y], Quaternion.identity);
                        currentType = "d";
                    }
                }
                else if (currentType == "l")
                {
                    selection = Random.Range(0, 3);
                    x++;
                    if (x > BOARD_SIZE-1) break;
                    if (selection == 0)
                    {
                        boardObject[x, y] = Instantiate(riverCurveLeftDown, boardPosition[x, y], Quaternion.identity);
                        currentType = "ld";
                    }
                    else if (selection==1)
                    {
                        boardObject[x, y] = Instantiate(riverLeft, boardPosition[x, y], Quaternion.identity);
                        currentType = "l";
                    }else
                    {
                        boardObject[x, y] = Instantiate(riverCurveRightDown, boardPosition[x, y], Quaternion.identity);
                        currentType = "rd";
                    }
                }
            }
        }
    }

    void GenerateMountain()
    {

    }

    void GenerateGround()
    {
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            for (int j = 0; j < BOARD_SIZE; j++)
            {
                if (boardObject[i, j] == null)
                    boardObject[i, j] = Instantiate(landPrototype, boardPosition[i, j], Quaternion.identity);
            }
        }
    }
}
