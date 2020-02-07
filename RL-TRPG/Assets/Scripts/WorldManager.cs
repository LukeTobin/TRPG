using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{

    [SerializeField] int x;
    [SerializeField] int y;

    [Header("Stored Prefabs")]
    public GameObject cell;

    private void Start()
    {
        //x = Random.RandomRange(6, 12);
       // y = x + Random.RandomRange(-2, 4);

        Grid world = new Grid(x, y, 2, cell, gameObject);
    }
}
