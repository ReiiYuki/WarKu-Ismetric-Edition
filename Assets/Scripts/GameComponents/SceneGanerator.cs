using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGanerator : MonoBehaviour {

    public GameObject landPrototype,rockPrototype,forestPrototype;

    GameObject[,] boardObject;
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
        boardObject = new GameObject[8,8];
        numForest = Random.Range(1,16);
        numRock = Random.Range(1,8);
    }

    void GenerateScene()
    {
        //int assignNum;
        float origin_y = 2f;
        int assignNum;
        for (int i = 0; i < 8; i++)
        {
            float constant_x = i * -0.65f;
            float constant_y = i * -0.325f+origin_y;
            for (int j = 0; j < 8; )
            {
                assignNum = (int)Random.Range(0, 3);
                if (assignNum == 1)
                {
                    if (numForest <= 0) continue;
                    numForest--;
                    Vector3 position = new Vector3(j * 0.65f + constant_x, j * -0.325f + constant_y+0.14f, -1 * (j + i));
                    boardObject[i,j] = Instantiate(forestPrototype, position , Quaternion.identity);
                }
                else if (assignNum == 2)
                {
                    if (numRock <= 0) continue;
                    numRock--;
                    Vector3 position = new Vector3(j * 0.65f + constant_x, j * -0.325f + constant_y, -1 * (j + i));
                    boardObject[i, j] = Instantiate(rockPrototype, position , Quaternion.identity);
                }else
                {
                    Vector3 position = new Vector3(j * 0.65f + constant_x, j * -0.325f + constant_y, -1 * (j + i));
                    boardObject[i, j] = Instantiate(landPrototype, position, Quaternion.identity);
                }
                j++;
            }
        }
    }
}
