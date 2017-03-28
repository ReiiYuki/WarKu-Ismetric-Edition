using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    // Normal Tile
    public static int PLAIN = 0;
    // River Tile
    public static int RIVER_UP_LEFT = 1;
    public static int RIVER_UP_RIGHT = 2;
    public static int RIVER_DOWN_LEFT = 3;
    public static int RIVER_DOWN_RIGHT = 4;
    public static int RIVER_DOWN = 5;
    public static int RIVER_LEFT = 6;
    // Mountain Tile
    public static int MOUNTAIN_RIDGE_UP_RIGHT = 7;
    public static int MOUNTAIN_SLOPE_RIGHT = 8;
    public static int MOUNTAIN_RIDGE_DOWN_RIGHT = 9;
    public static int MOUNTAIN_SLOPE_UP = 10;
    public static int MOUNTAIN_PEAK = 11;
    public static int MOUNTAIN_SLOPE_DOWN = 12;
    public static int MOUNTAIN_RIDGE_UP_LEFT = 13;
    public static int MOUNTAIN_SLOPE_LEFT = 14;
    public static int MOUNTAIN_RIDGE_DOWN_LEFT = 15;
    // Forest Tile
    public static int FORSET = 16;
    // Stone Tile
    public static int STONE = 17;

    public int type;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
