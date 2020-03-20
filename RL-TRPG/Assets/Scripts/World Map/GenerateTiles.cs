using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTiles : MonoBehaviour
{
    int width;
    int height;
    int cellSize; // size of one box

    int[,] gridArray; // 2D array 

    public GameObject bgTile;
    public Sprite[] tiles;

    public void SpawnTiles(int width, int height, int cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new int[width, height];
        GameObject BackgroundTiles = new GameObject("Background Tiles");

        for (int x = 0; x < gridArray.GetLength(0); x++) // use .GetLength when you want to reference a certain row of the 2d array [0, 1, 2, ..] based on how many dimensions your array has
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                GameObject tile = GameObject.Instantiate(bgTile, GetWorldPosition(x, y), Quaternion.identity);
                tile.transform.parent = BackgroundTiles.transform; // make it a child of the parent object

                bgTile.GetComponent<SpriteRenderer>().sprite = tiles[Random.Range(0, tiles.Length)];
            }
        }
    }

    Vector3 GetWorldPosition(int x, int y) // return a Vector3 of the world position of new tile
    {
        return new Vector3((x * cellSize), (y * cellSize), 1); // multiply the x & y values by the cellSize
    }
}
