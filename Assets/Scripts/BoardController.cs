using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public int rows = 10;
    public int columns = 10;

    public GameObject floorTile;
    public GameObject wallTile;
    public GameObject softTile;

    private Transform boardHolder;
    private Transform floorHolder;
    private Transform outerWallHolder;
    private Transform innerWallHolder;
    private Transform softBlockHolder;

    private void Start()
    {
        boardHolder = new GameObject("Board").transform;

        Setup();
    }

    public void Setup()
    {
        SetupFloor();
        SetupOuterWalls();
        SetupInnerWalls();
    }

    /*
     * Fill the outer walls prefabs
     */
    void SetupOuterWalls()
    {
        outerWallHolder = new GameObject("OuterWall").transform;
        outerWallHolder.SetParent(boardHolder);

        for (int x = -1; x <= columns + 1; x++)
        {
            for (int y = -1; y <= rows + 1; y++)
            {
                if (x == -1 || x == rows + 1 || y == -1 || y == columns + 1)
                {                    
                    Instantiate(wallTile, new Vector3(x, 0.5f, y), Quaternion.identity)
                        .transform
                        .SetParent(outerWallHolder);
                }
            }
        }
    }

    /*
     * Fill the inner walls prefabs
     */
    void SetupInnerWalls()
    {
        innerWallHolder = new GameObject("InnerWall").transform;
        innerWallHolder.SetParent(boardHolder);

        softBlockHolder = new GameObject("SoftBlock").transform;
        softBlockHolder.SetParent(boardHolder);

        for (int x = 0; x <= columns; x++)
        {
            for (int y = 0; y <= rows; y++)
            {
                if (x % 2 != 0 && y % 2 != 0)
                {
                    Instantiate(wallTile, new Vector3(x, 0.5f, y), Quaternion.identity)
                        .transform
                        .SetParent(innerWallHolder);
                }
                else
                {
                    if (x <= 1 && y <= 1) {
                        continue;
                    }
                    Instantiate(softTile, new Vector3(x, 0.5f, y), Quaternion.identity)
                    .transform
                    .SetParent(softBlockHolder);
                }
            }
        }
    }
       
    /*
     * Fill the floor prefabs
     */
    void SetupFloor()
    {
        floorHolder = new GameObject("InnerWall").transform;
        floorHolder.SetParent(boardHolder);

        for (int x = 0; x <= columns; x++)
        {
            for (int y = 0; y <= rows; y++)
            {
               Instantiate(floorTile, new Vector3(x, 0f, y), Quaternion.identity)
                    .transform
                    .SetParent(floorHolder);
            }           
        }
    }
}
