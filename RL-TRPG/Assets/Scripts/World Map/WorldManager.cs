using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    /*
     * Build world map - currently only create a grid
     * Procedrual generation needs to be added here
     */

    [Header("Stored Prefabs")]
    [Space]
    public GameObject[] nodes;
    [Space]
    public GameObject GridMap;

    [Header("World Settings")]
    public int cellSize;
    public int x;
    public int y;

    [Header("Progress Settings")]
    public int stage = 0;


    // private vars
    List<Node> nodeList = new List<Node>();

    private void Start()
    {
        int length = Random.Range(6, 12);
        CreateMap(x, y);
    }

    void CreateMap(int _x, int _y)
    {
        int[,] gridArray = new int[_x, _y];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                if(x == 0)
                {
                    GameObject node = GameObject.Instantiate(nodes[0], GetWorldPosition(x, y+1), Quaternion.identity);
                    node.transform.parent = transform;
                    nodeList.Add(node.GetComponent<Node>());
                }
                else
                {
                    GameObject node = GameObject.Instantiate(nodes[Random.Range(0, nodes.Length)], GetWorldPosition(x, y+1), Quaternion.identity);
                    node.transform.parent = transform;
                    nodeList.Add(node.GetComponent<Node>());
                }       
            }
        }
    }

    Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3((x * cellSize), (y * cellSize), 1);
    }
}
