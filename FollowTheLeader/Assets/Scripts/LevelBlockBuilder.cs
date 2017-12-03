using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBlockBuilder : MonoBehaviour {

    private static readonly int MAX_FLOOR_WIDTH = 18;
    private static readonly int MAX_FLOOR_HEIGT = 21;
    private static readonly int MAX_BIG_WALL_WIDTH = 18;
    private static readonly int MAX_BIG_WALL_HEIGHT = 2;
    private static readonly int MAX_SIDE_WALL_HEIGHT = 23;
    private static readonly int MAX_SIDE_WALL_WIDTH = 1;


    public GameObject[] prefabs;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [ContextMenu("GenerateFloor")]
    public void GenerateFloor() {

        GameObject floorParent = new GameObject("Floor");

        for (int i = 0; i > -MAX_FLOOR_HEIGT; i--) {

            for(int j = 0; j < MAX_FLOOR_WIDTH; j++) {

                GameObject tile;

                if(i == 0) {
                    //Top floor
                    tile = Instantiate<GameObject>(prefabs[20]);
                } else if(i < 0 && i > -MAX_FLOOR_HEIGT + 1) {
                    //Floor
                    tile = Instantiate<GameObject>(prefabs[3]);
                } else {
                    //Bottom wall
                    tile = Instantiate<GameObject>(prefabs[2]);
                }

                floorParent.transform.SetParent(this.transform);
                tile.transform.position = new Vector3(j, i, 0);
                tile.transform.SetParent(floorParent.transform);
            }
        }
    }

    [ContextMenu("GenerateBackWall")]
    public void GenerateBackWall() {

        GameObject wallParent = new GameObject("BackWall");

        for (int i = 0; i > -MAX_BIG_WALL_HEIGHT; i--) {

            for (int j = 0; j < MAX_BIG_WALL_WIDTH; j++) {

                GameObject tile;

                if (i == 0) {
                    //Top wall
                    tile = Instantiate<GameObject>(prefabs[21]);
                } else {
                    //Border wall
                    tile = Instantiate<GameObject>(prefabs[1]);
                }

                wallParent.transform.SetParent(this.transform);
                tile.transform.position = new Vector3(j, i, 0);
                tile.transform.SetParent(wallParent.transform);
            }
        }
    }

    [ContextMenu("GenerateLeftWall")]
    public void GenerateLeftWall() {

        GameObject wallParent = new GameObject("LeftWall");

        for (int i = 0; i > -MAX_SIDE_WALL_HEIGHT; i--) {

            for (int j = 0; j < MAX_SIDE_WALL_WIDTH; j++) {

                GameObject tile;

                if (i == 0) {
                    //Left wall corner
                    tile = Instantiate<GameObject>(prefabs[9]);
                } else if (i == -1) {
                    //Left wall back wall border
                    tile = Instantiate<GameObject>(prefabs[8]);
                } else if(i == -2) {
                    //Left wall top floor
                    tile = Instantiate<GameObject>(prefabs[11]);
                } else if (i <= -3 && i > -MAX_SIDE_WALL_HEIGHT + 3) {
                    //Left wall floor
                    tile = Instantiate<GameObject>(prefabs[10]);
                } else if (i == -MAX_SIDE_WALL_HEIGHT + 3) {
                    //Left top door
                    tile = Instantiate<GameObject>(prefabs[13]);
                } else if (i == -MAX_SIDE_WALL_HEIGHT + 2) {
                    //Left bottom door
                    tile = Instantiate<GameObject>(prefabs[15]);
                } else {
                    //Bottom wall
                    tile = Instantiate<GameObject>(prefabs[2]);
                }

                wallParent.transform.SetParent(this.transform);
                tile.transform.position = new Vector3(j, i, 0);
                tile.transform.SetParent(wallParent.transform);
            }
        }
    }

    [ContextMenu("GenerateRightWall")]
    public void GenerateRightWall() {

        GameObject wallParent = new GameObject("RightWall");

        for (int i = 0; i > -MAX_SIDE_WALL_HEIGHT; i--) {

            for (int j = 0; j < MAX_SIDE_WALL_WIDTH; j++) {

                GameObject tile;

                if (i == 0) {
                    //Right wall corner
                    tile = Instantiate<GameObject>(prefabs[18]);
                } else if (i == -1) {
                    //Right wall back wall border
                    tile = Instantiate<GameObject>(prefabs[17]);
                } else if (i == -2) {
                    //Right wall top floor
                    tile = Instantiate<GameObject>(prefabs[22]);
                } else if (i <= -3 && i > -MAX_SIDE_WALL_HEIGHT + 3) {
                    //Right wall floor
                    tile = Instantiate<GameObject>(prefabs[19]);
                } else if (i == -MAX_SIDE_WALL_HEIGHT + 3) {
                    //Right top door
                    tile = Instantiate<GameObject>(prefabs[6]);
                } else if (i == -MAX_SIDE_WALL_HEIGHT + 2) {
                    //Right bottom door
                    tile = Instantiate<GameObject>(prefabs[4]);
                } else {
                    //Bottom wall
                    tile = Instantiate<GameObject>(prefabs[2]);
                }

                wallParent.transform.SetParent(this.transform);
                tile.transform.position = new Vector3(j, i, 0);
                tile.transform.SetParent(wallParent.transform);
            }
        }
    }
}
