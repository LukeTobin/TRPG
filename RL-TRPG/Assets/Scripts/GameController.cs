using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Gameplay")]
    public float GameSpeed;
    public int cellSize;

    [Header("Stored Info")]
    public TeamHandler team1;
    public TeamHandler team2;
    public GameObject tile;
    public Unit selectedUnit;
    public GameObject optionBox;

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
        Grid map = new Grid(6, 6, cellSize, tile, StoredTiles);

        playerTurn = 1;
        team1.UpdateUnitsToMove();

        optionBox.SetActive(false);
    }

    private void Update()
    {
        if (_forceEnd)
        {
            EndTurn();
            _forceEnd = false;
        }
    }

    public void ResetTiles()
    {
        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            tile.Reset();
        }
    }

    public void EndTurn()
    {
        if (selectedUnit != null)
        {
            selectedUnit.selected = false;
            selectedUnit = null;
        }

        ResetTiles();

        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            unit.hasMoved = false;
            unit.hasAttacked = false;
            unit.sr.color = new Color(1, 1, 1, 255);
        }

        if (playerTurn == 1)
        {
            playerTurn = 2;
            team2.UpdateUnitsToMove();
        }
        else if (playerTurn == 2)
        {
            playerTurn = 1;
            team1.UpdateUnitsToMove();
        }

        uim.UpdateTurn();
        Debug.Log("Player turn: " + playerTurn);
    }

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
