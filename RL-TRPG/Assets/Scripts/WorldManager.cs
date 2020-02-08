using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [Header("Stored Prefabs")]
    public GameObject cell;
    public GameObject[] nodes;
    public GameObject GridMap;

    [Header("World Settings")]
    public int cellSize;

    private void Start()
    {
        int length = Random.Range(6, 12);
        CreateMap(length);
        Grid world = new Grid(length, length, cellSize, cell, GridMap);
    }

    void CreateMap(int size)
    {
        int[,] gridArray = new int[size, size];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                
                GameObject node = GameObject.Instantiate(nodes[Random.Range(0, nodes.Length)], GetWorldPosition(x, y), Quaternion.identity);
                node.transform.parent = transform;
            }
        }
    }

    Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3((x * cellSize), (y * cellSize), 1);
    }
}
