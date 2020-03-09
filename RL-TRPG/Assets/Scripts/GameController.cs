using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    /*
     * Controls flow and gameplay elements
     */

    [Header("Gameplay")]
    public float GameSpeed;
    public int cellSize;
    public int x;
    public int y;

    [Header("Access")]
    public TeamHandler team1;
    public TeamHandler team2;
    [Space]
    public GameObject tile;
    public Unit selectedUnit;
    public GameObject optionBox;
    public Grid map;
    GameObject StoredTiles;
    UIManager uim;
    
    [Header("General Information")]
    public int playerTurn = 1;

    [Header("Admin Tests")]
    public bool _forceEnd;

    bool uwu;

    private void Start()
    {
        uim = GameObject.FindGameObjectWithTag("BoardUI").GetComponent<UIManager>();
        StoredTiles = new GameObject("StoredTiles");
        map = new Grid(x, y, cellSize, tile, StoredTiles); // creates grid

        playerTurn = 1;

        optionBox.SetActive(false); // disable a test box for multiple commands [dont need to worry about it right now, its not fully finished]
    }

    private void Update()
    {
        // admin command, setting it true ends the turn and sets itself back to false.
        if (_forceEnd)
        {
            EndTurn();
            _forceEnd = false;
        }
    }

    // reset everything about the tile (color, stored coords, etc) - will probably be changed up more in the future
    // done for visual clarification
    public void ResetTiles()
    {
        // goes through each tile thats in the scene and resets each
        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            tile.Reset();
        }
    }

    // end turn
    public void EndTurn()
    {
        // make sure there is no selected unit stored & if there is, make it unselected
        if (selectedUnit != null)
        {
            selectedUnit.selected = false;
            selectedUnit = null;
        }

        ResetTiles();//reset all tiles (mostly for colors and visibiliy resets)


        // reset each unit in the scene
        foreach (Unit unit in FindObjectsOfType<Unit>()) 
        {
            unit.hasMoved = false;
            unit.hasAttacked = false;
            unit.sr.color = new Color(1, 1, 1, 255);
        }


        // change turns
        switch (playerTurn)
        {
            case 1:
                playerTurn = 2;
                team2.UpdateUnitsToMove();
                break;
            case 2:
                playerTurn = 1;
                team1.UpdateUnitsToMove();
                break;
            default:
                break;
        }

        // Update UI 
        uim.UpdateTurn();
        Debug.Log("Player turn: " + playerTurn);
    }

    // experimental option box, hasnt been implmented yet
    public void CreateOptionBox(Unit unit, Vector2 pos)
    {
        if (!uwu)
        {
            optionBox.SetActive(true);
            uwu = true;
        }
        else if(uwu)
        {
            optionBox.SetActive(false);
            uwu = false;
        }
    }
}
