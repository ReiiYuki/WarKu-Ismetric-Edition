using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGanerator : MonoBehaviour {

    public GameObject landPrototype;

    int[,] board;
    int numForest;
    int numRock;

	// Use this for initialization
	void Start () {
        InitializeBoard();
        GenerateScene();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitializeBoard()
    {
        board = new int[8,8];
        numForest = 16;
        numRock = 8;
    }

    void GenerateScene()
    {
        //int assignNum;
        float origin_y = 2f;
        for (int i = 0; i < 8; i++)
        {
            float constant_x = i * -0.65f;
            float constant_y = i * -0.325f+origin_y;
            for (int j = 0; j < 8; j++)
            {
                Instantiate(landPrototype, new Vector3(j * 0.65f+ constant_x, j * -0.325f+ constant_y, -1 * (j + i)), Quaternion.identity);
            }
        }
       /* for(int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; )
            {
                assignNum = (int)Random.Range(0, 3);
                Debug.Log(assignNum);
                if (assignNum == 1)
                {
                    if (numForest <= 0) continue;
                    numForest--;
                }else if (assignNum == 2)
                {
                    if (numRock <= 0) continue;
                    numRock--;
                }
                board[i, j] = assignNum;
                Instantiate(landPrototype, new Vector3(j * -0.65f, j * -0.325f),Quaternion.identity);
                j++;
            }
        }*/
   /*     for (int i = 0; i < 8; i++)
        {
            string s = "";
            for (int j = 0; j < 8; j++)
            {
                s += board[i, j] + " ";
            }
            Debug.Log(s);
        }*/
    }
}
