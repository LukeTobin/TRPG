using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid 
{

    /*
     * Creates a basic tile grid when filled in
     */

    int width;
    int height;

    float cellSize;
    int[,] gridArray;

    public Grid(int width, int height, float cellSize, GameObject tile, GameObject newParent)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new int[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                GameObject _tile = GameObject.Instantiate(tile, GetWorldPosition(x, y), Quaternion.identity);
                _tile.transform.parent = newParent.transform;
            }
        }
    }

    Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }
}
