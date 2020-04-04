using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [Space]
    public Grid map;
    public bool selected;
    GenerateTiles gt;
    UIManager uim;
    
    [Header("General Information")]
    public int playerTurn = 1;
    [Space]
    public int friendly;
    public int enemy;
    [Space]
    public bool ended = false;

    [Header("Unit Stoarge")]
    public List<Unit> units = new List<Unit>();

    [Header("Admin Tests")]
    public bool _forceEnd;
    public bool _clearList;

    bool uwu;
    

    private void Start()
    {
        uim = GameObject.FindGameObjectWithTag("BoardUI").GetComponent<UIManager>();
        gt = GetComponent<GenerateTiles>();

        GameObject StoredTiles = new GameObject("StoredTiles");
        map = new Grid(x, y, cellSize, tile, StoredTiles); // creates grid

        gt.SpawnTiles(x, y, cellSize);

        playerTurn = 1;

        //optionBox.SetActive(false); // disable a test box for multiple commands [dont need to worry about it right now, its not fully finished]
    }

    private void Update()
    {
        // admin command, setting it true ends the turn and sets itself back to false.
        if (_forceEnd)
        {
            EndTurn();
            _forceEnd = false;
        }

        if (_clearList)
        {
            team1.children.Clear();
            team2.children.Clear();
            _clearList = false;
        }

        if (selectedUnit != null)
            selected = true;
        else
            selected = false;
    }

    private void FixedUpdate()
    {
        if (friendly <= 0 && !ended)
        {
            team1.UpdateUnitsToMove();
            team2.UpdateUnitsToMove();

            team1.CheckIfEnd();
            team2.CheckIfEnd();

            enemy = 100;
            friendly = 100;
        }

        if (enemy <= 0 && !ended)
        {
            team1.UpdateUnitsToMove();
            team2.UpdateUnitsToMove();

            team1.CheckIfEnd();
            team2.CheckIfEnd();

            enemy = 100;
            friendly = 100;
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
        team1.UpdateUnitsToMove();
        team2.UpdateUnitsToMove();

        if (!team1.CheckIfAllDead() || !team2.CheckIfAllDead())
        {
            // make sure there is no selected unit stored & if there is, make it unselected
            if (selectedUnit != null)
            {
                selectedUnit.selected = false;
                selectedUnit = null;
            }

            ResetTiles();//reset all tiles (mostly for colors and visibiliy resets)


            // reset each unit in the scene & add mana
            foreach (Unit unit in FindObjectsOfType<Unit>())
            {
                unit.hasMoved = false;
                unit.hasAttacked = false;
                unit.sr.color = new Color(1, 1, 1, 255);
                if(unit.mana < 5)
                {
                    unit.mana++;
                }
                unit.tempAD = 0;
                unit.tempMD = 0;
                unit.tempAR = 0;
                unit.tempMR = 0;
                unit.tempSP = 0;

                unit.fade(false);
                unit.marked = false;
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
        }
        
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

    public void Rewards()
    {
        // save each units data before ending
        foreach(Unit unit in team1.children)
        {
            unit.SaveData();
        }

        // create a unique recruit list
        List<Unit> recruitList = new List<Unit>();

        for (int i = 0; i < 3; i++)
        {
            // need to check for dup's
            recruitList.Add(units[Random.Range(0, units.Count)]);
        }

        // make sure the game knows its continuing
        PlayerPrefs.SetInt("continued", 1);
        PlayerPrefs.Save();

        uim.OfferRecruit(recruitList);
    }

    public void KillUnit(Unit unit, int side)
    {

        switch (side)
        {
            case 1:
                friendly--;
                team1.children.Clear();
                Destroy(unit.gameObject);
                break;
            case 2:
                enemy--;
                team2.children.Clear();
                Destroy(unit.gameObject);
                break;
            default:
                Debug.Log("contains - none");
                break;
        }

        team1.UpdateUnitsToMove();
        team2.UpdateUnitsToMove();
    }

    public void CheckEnd()
    {
        //team1.CheckIfAllDead();
        //team2.CheckIfAllDead();
    }

    public void YouDied()
    {
        EndGame();
    }

    public void EndGame()
    {
        SceneManager.LoadScene("Map");
    }
}
