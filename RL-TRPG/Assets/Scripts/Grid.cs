using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid 
{

    /*
     * Creates a basic tile grid when filled in
     */

    // variables
    int width;
    int height;
    int cellSize; // size of one box

    int[,] gridArray; // 2D array 

    // constructor for creating a grid on the map
    public Grid(int width, int height, int cellSize, GameObject tile, GameObject newParent) // tile is the object we spawn, newParent is what will be the parent of each object
    {
        // get / set - setting the recived variables to this class
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        // 2D array size set to the passed width & height
        gridArray = new int[width, height];

        // loops through the width & height (x & y)
        for (int x = 0; x < gridArray.GetLength(0); x++) // use .GetLength when you want to reference a certain row of the 2d array [0, 1, 2, ..] based on how many dimensions your array has
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                // will go through each x & y point [0, 0], [0, 1] ... etc
                GameObject _tile = GameObject.Instantiate(tile, GetWorldPosition(x, y), Quaternion.identity); // instantiate the tile game object at a world position based on where the loop is
                _tile.transform.parent = newParent.transform; // make it a child of the parent object
                _tile.GetComponent<Tile>().x = x * cellSize;
                _tile.GetComponent<Tile>().y = y * cellSize;
            }
        }
    }

    Vector3 GetWorldPosition(int x, int y) // return a Vector3 of the world position of new tile
    {
        return new Vector3((x * cellSize), (y * cellSize), 1); // multiply the x & y values by the cellSize
    }

    public Tile GetTile(int x, int y)
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tiles");
        Debug.Log("To Search: " + tiles.Length);
        for (int i = 0; i < tiles.Length; i++)
        {
            if(tiles[i].GetComponent<Tile>().x == x && tiles[i].GetComponent<Tile>().y == y) 
            {
                Debug.Log(x + "," + y + " == " + tiles[i].GetComponent<Tile>().x + "," + tiles[i].GetComponent<Tile>().y + "<- Match!");
                return tiles[i].GetComponent<Tile>();
            }
            else
            {
                Debug.Log(x + "," + y + " != " + tiles[i].GetComponent<Tile>().x + "," + tiles[i].GetComponent<Tile>().y);
            }
        }
        return null;
    }
}
