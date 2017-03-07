using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGanerator : MonoBehaviour {

    public GameObject landPrototype,rockPrototype,forestPrototype;
    public GameObject riverCurveLeftUp, riverCurveLeftDown, riverCurveRightUp, riverCurveRightDown, riverLeft, riverDown;
    public GameObject mountPeak, mountSlopeUp, mountSlopeLeft, mountSlopeRight, mountSlopeDown, mountRidgeLeftUp, mountRidgeRightUp, mountRidgeRightDown, mountRidgeLeftDown;

    public GameObject[,] boardFloorObject,boardUnitObject;
    public Vector3[,] boardPosition;

    const int BOARD_SIZE = 12;

	// Use this for initialization
	void Start () {
        InitializeBoard();
        GenerateGeo();
        GetComponent<BoardManager>().SetPosition(boardFloorObject, boardUnitObject, boardPosition);
    }

    void InitializeBoard()
    {
        boardFloorObject = new GameObject[BOARD_SIZE, BOARD_SIZE];
        boardPosition = new Vector3[BOARD_SIZE, BOARD_SIZE];
        boardUnitObject = new GameObject[BOARD_SIZE, BOARD_SIZE];
        GeneratePosition();
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
        GenerateMountain();
        GenerateRock();
        GenerateForest();
        GenerateGround();
    }

    void GenerateRiver()
    {
        bool hasRiver = Mathf.Round(Random.Range(0f, 1f))==1;
        if (hasRiver)
        {
            int y = Random.Range(1, BOARD_SIZE-1);
            int x = 0;
            string currentType;
            int selection = Random.Range(0, 3);
            if (selection == 0)
            {
                boardFloorObject[x, y] = Instantiate(riverLeft, boardPosition[x, y], Quaternion.identity);
                currentType = "l";
            }else if (selection == 1)
            {
                boardFloorObject[x, y] = Instantiate(riverCurveLeftDown, boardPosition[x, y], Quaternion.identity);
                currentType = "ld";
            }else
            {
                boardFloorObject[x, y] = Instantiate(riverCurveRightDown, boardPosition[x, y], Quaternion.identity);
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
                        boardFloorObject[x, y] = Instantiate(riverCurveRightUp, boardPosition[x, y], Quaternion.identity);
                        currentType = "ru";
                    }else
                    {
                        boardFloorObject[x, y] = Instantiate(riverDown, boardPosition[x, y], Quaternion.identity);
                        currentType = "d";
                    }
                }
                else if (currentType == "rd")
                {
                    y--;
                    if (y < 0) break;
                    boardFloorObject[x, y] = Instantiate(riverCurveLeftUp, boardPosition[x, y], Quaternion.identity);
                    currentType = "lu";
                }
                else if (currentType == "ru")
                {
                    x++;
                    if (x > BOARD_SIZE-1) break;
                    selection = Random.Range(0, 2);
                    if (selection == 0)
                    {
                        boardFloorObject[x, y] = Instantiate(riverCurveLeftDown, boardPosition[x, y], Quaternion.identity);
                        currentType = "ld";
                    }
                    else
                    {
                        boardFloorObject[x, y] = Instantiate(riverLeft, boardPosition[x, y], Quaternion.identity);
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
                        boardFloorObject[x, y] = Instantiate(riverCurveLeftDown, boardPosition[x, y], Quaternion.identity);
                        currentType = "ld";
                    }
                    else
                    {
                        boardFloorObject[x, y] = Instantiate(riverLeft, boardPosition[x, y], Quaternion.identity);
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
                        boardFloorObject[x, y] = Instantiate(riverCurveRightUp, boardPosition[x, y], Quaternion.identity);
                        currentType = "ru";
                    }
                    else
                    {
                        boardFloorObject[x, y] = Instantiate(riverDown, boardPosition[x, y], Quaternion.identity);
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
                        boardFloorObject[x, y] = Instantiate(riverCurveLeftDown, boardPosition[x, y], Quaternion.identity);
                        currentType = "ld";
                    }
                    else if (selection==1)
                    {
                        boardFloorObject[x, y] = Instantiate(riverLeft, boardPosition[x, y], Quaternion.identity);
                        currentType = "l";
                    }else
                    {
                        boardFloorObject[x, y] = Instantiate(riverCurveRightDown, boardPosition[x, y], Quaternion.identity);
                        currentType = "rd";
                    }
                }
            }
        }
    }

    void GenerateMountain()
    {
        int numberOfPeak = Random.Range(0, 10);
        int[][] peakPosition = new int[numberOfPeak][]; 
        for (int i = 0;i<numberOfPeak;i++)
        {
            peakPosition[i] = new int[2];
            peakPosition[i][0] = Random.Range(0, BOARD_SIZE);
            peakPosition[i][1] = Random.Range(0, BOARD_SIZE);
        }
        Vector3 mountainOffset = new Vector3(0f, 0.1f);
        int x, y,topPos,downPos,leftPos,rightPos;
        foreach (int[] mountPos in peakPosition)
        {
            x = mountPos[0];
            y = mountPos[1];
            if (boardFloorObject[x, y] == null|| boardFloorObject[x, y].tag=="Slope" || boardFloorObject[x, y].tag=="Ridge")
            {
                if (boardFloorObject[x, y] != null)
                    Destroy(boardFloorObject[x, y]);
                boardFloorObject[x, y] = Instantiate(mountPeak, boardPosition[x, y] + mountainOffset, Quaternion.identity);
                topPos = y - 1;
                downPos = y + 1;
                leftPos = x + 1;
                rightPos = x - 1;
                if (topPos >= 0)
                {
                    if (boardFloorObject[x, topPos] == null)
                        boardFloorObject[x, topPos] = Instantiate(mountSlopeUp, boardPosition[x, topPos], Quaternion.identity);
                    if (rightPos >= 0)
                    {
                        if (boardFloorObject[rightPos, topPos] == null)
                            boardFloorObject[rightPos, topPos] = Instantiate(mountRidgeLeftUp, boardPosition[rightPos, topPos], Quaternion.identity);
                    }
                    if (leftPos < BOARD_SIZE)
                    {
                        if (boardFloorObject[leftPos, topPos] == null)
                            boardFloorObject[leftPos, topPos] = Instantiate(mountRidgeLeftDown, boardPosition[leftPos, topPos] , Quaternion.identity);
                    }
                }
                if (downPos < BOARD_SIZE)
                {
                    if (boardFloorObject[x, downPos] == null)
                        boardFloorObject[x, downPos] = Instantiate(mountSlopeDown, boardPosition[x, downPos] + mountainOffset, Quaternion.identity);
                    if (rightPos >= 0)
                    {
                        if (boardFloorObject[rightPos, downPos] == null)
                            boardFloorObject[rightPos, downPos] = Instantiate(mountRidgeRightUp, boardPosition[rightPos, downPos], Quaternion.identity);
                    }
                    if (leftPos < BOARD_SIZE)
                    {
                        if (boardFloorObject[leftPos, downPos] == null)
                            boardFloorObject[leftPos, downPos] = Instantiate(mountRidgeRightDown, boardPosition[leftPos, downPos] + mountainOffset, Quaternion.identity);
                    }
                }
                if (rightPos >= 0)
                {
                    if (boardFloorObject[rightPos, y] == null)
                        boardFloorObject[rightPos, y] = Instantiate(mountSlopeRight, boardPosition[rightPos, y] , Quaternion.identity);
                }
                if (leftPos < BOARD_SIZE)
                {
                    if (boardFloorObject[leftPos, y] == null)
                        boardFloorObject[leftPos, y] = Instantiate(mountSlopeLeft, boardPosition[leftPos, y] + mountainOffset, Quaternion.identity);
                }
            }
        }
    }

    void GenerateRock()
    {
        int numberOfStone = Random.Range(0, 10);
        int x, y;
        for (int i = 0; i < numberOfStone; i++)
        {
            x = Random.Range(0, BOARD_SIZE);
            y = Random.Range(0, BOARD_SIZE);
            if (boardFloorObject[x, y] == null)
                boardFloorObject[x, y] = Instantiate(rockPrototype, boardPosition[x, y], Quaternion.identity);
        }
    }

    void GenerateForest()
    {
        int numberOfForest = Random.Range(0, 15);
        int x, y;
        for (int i = 0; i < numberOfForest; i++)
        {
            x = Random.Range(0, BOARD_SIZE);
            y = Random.Range(0, BOARD_SIZE);
            if (boardFloorObject[x, y] == null)
                boardFloorObject[x, y] = Instantiate(forestPrototype, boardPosition[x, y]+new Vector3(0,0.13f), Quaternion.identity);
        }
    }

    void GenerateGround()
    {
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            for (int j = 0; j < BOARD_SIZE; j++)
            {
                if (boardFloorObject[i, j] == null)
                    boardFloorObject[i, j] = Instantiate(landPrototype, boardPosition[i, j], Quaternion.identity);
            }
        }
    }

}
