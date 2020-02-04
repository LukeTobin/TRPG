using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject tile;
    GameObject StoredTiles;

    private void Start()
    {
        StoredTiles = new GameObject("StoredTiles");
        Grid map = new Grid(6, 6, 2, tile, StoredTiles);
    }
}
