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
    public GameObject TeamInformationHandler;
    public Team allTeam;
    [Space]
    public GameObject tile;
    public Unit selectedUnit;
    public GameObject optionBox;
    [Space]
    public Grid map;
    public bool selected;
    public bool unitMoving;
    GenerateTiles gt;
    UIManager uim;

    [Header("General Information")]
    public int playerTurn = 1;
    public int round = 1;
    public int score = 1000;
    [Space]
    public int friendly;
    public int enemy;
    [Space]
    public bool ended = false;
    public int enemyAccess = 0;
    [Space]
    public bool autoEnd = false;

    [Header("Admin Tests")]
    public bool _forceEnd;
    public bool logAttacks;

    bool uwu;
    public TeamHandler teams;
    EnemyManager em;
    TraitBonuses traits;
    GameManager gm;

    private void Start()
    {
        uim = GameObject.FindGameObjectWithTag("BoardUI").GetComponent<UIManager>();
        allTeam = GameObject.FindGameObjectWithTag("Team").GetComponent<Team>();
        gm = FindObjectOfType<GameManager>();

        gt = GetComponent<GenerateTiles>();

        teams = TeamInformationHandler.GetComponent<TeamHandler>();
        em = TeamInformationHandler.GetComponent<EnemyManager>();
        traits = TeamInformationHandler.GetComponent<TraitBonuses>();

        GameObject StoredTiles = new GameObject("StoredTiles");
        map = new Grid(x, y, cellSize, tile, StoredTiles); // creates grid

        gt.SpawnTiles(x, y, cellSize);

        playerTurn = 1;
        allTeam.LoadArtifacts();
    }

    private void Update()
    {
        #region Admin Tests & Keybinds
        // admin command, setting it true ends the turn and sets itself back to false.
        if (_forceEnd)
        {
            EndTurn();
            _forceEnd = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            EndTurn();
        }

        #endregion
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

    /// <summary>
    /// Ends Turn:
    /// - Update all units that need to be moved
    /// - Check if either side all died
    /// - Reset all tiles and units
    /// - Swap Player turns
    /// </summary>
    public void EndTurn()
    {
        teams.UpdateUnitsToMove(TeamHandler.UpdateType.all);
        teams.UpdateUnitsToAttack(TeamHandler.UpdateType.all);

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
            if (unit.mana < 5 && playerTurn == 2)
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

            unit.UpdateUCanvas();
        }


        // change turns
        switch (playerTurn)
        {
            case 1:
                playerTurn = 2;
                break;
            case 2:
                playerTurn = 1;
                score -= Random.Range(30, 50 + ( 2* gm.currentStage));
                break;
            default:
                break;
        }

        round++;

        // Update UI 
        uim.UpdateTurn();

        if (playerTurn == 2)
            GetEnemyMoves();
    }

    /// <summary>
    /// Give out rewards
    /// </summary>
    public void Rewards()
    {
        // save each units data before ending
        foreach (Unit unit in teams.friendlyUnits)
        {
            unit.SaveData();
        }

        int val = (15 * gm.currentStage) - round;
        if (val < 1)
            val = 1;
        uim.RewardScreen(val, 1, true);
    }

    /// <summary>
    /// Function for cleanly killing a unit
    /// </summary>
    /// <param name="unit">What unit needs to die</param>
    /// <param name="side">What side the unit was apart of</param>
    public void KillUnit(Unit unit, int side)
    {
        switch (side)
        {
            case 1:
                //friendly--;
                teams.friendlyUnits.Remove(unit);
                allTeam.units.Remove(unit);
                PlayerPrefs.SetInt(unit.title, 0);
                PlayerPrefs.Save();
                if(!unit.hasAttacked && !unit.hasMoved)
                {
                    teams.friendlyUnitsMovable--;
                }
                score -= 100;
                Destroy(unit.gameObject);
                break;
            case 2:
                //enemy--;
                teams.enemyUnits.Remove(unit);
                if(!unit.hasAttacked && !unit.hasMoved)
                {
                    teams.enemyUnitsMovable--;
                }
                score += (300 + (gm.currentStage * 20));
                Destroy(unit.gameObject);
                break;
            default:
                break;
        }

        teams.UpdateUnitsToMove(TeamHandler.UpdateType.all);
    }

    public void YouDied()
    {
        gm.score += score;
        PlayerPrefs.SetInt("storedScore", gm.score);
        PlayerPrefs.Save();
        gm.Die();
    }

    public void EndGame()
    {
        gm.currentStage++;
      
        gm.currentNode.Visited();
        gm.currentNode = null;

        gm.score += score;
        PlayerPrefs.SetInt("storedScore", gm.score);
        PlayerPrefs.SetInt("stage", gm.currentStage);
        PlayerPrefs.Save();

        if (allTeam.artifacts.Count > 0)
            allTeam.LoadArtifacts(false); // unload all artifacts
        SceneManager.LoadScene("Map");
    }

    /// <summary>
    /// Generates 2 lists (enemies, targets) and passes them to the enemymanager
    /// </summary>
    public void GetEnemyMoves()
    {
        if (teams.enemyUnitsMovable > 0)
        {
            // get list
            List<Unit> enemies = new List<Unit>();
            enemies.Clear();
            for (int i = 0; i < teams.enemyUnits.Count; i++)
            {
                if (!teams.enemyUnits[i].hasMoved)
                    enemies.Add(teams.enemyUnits[i]);
            }

            List<Unit> targets = new List<Unit>();
            targets.Clear();
            for (int i = 0; i < teams.friendlyUnits.Count; i++)
            {
                targets.Add(teams.friendlyUnits[i]);
            }

            if (enemies.Count > 0)
                em.ControlEnemies(enemies[0], targets);
        }
        else
        {
            CheckEnds(true);
        }
    }

    public void CheckEnds(bool EnemyController = false)
    {
        teams.UpdateUnitsToMove(TeamHandler.UpdateType.all);
        teams.UpdateUnitsToMove(TeamHandler.UpdateType.all);

        if (teams.CheckIfAllDead(TeamHandler.UpdateType.enemy))
        {
            Rewards();
        }
        else if (teams.CheckIfAllDead(TeamHandler.UpdateType.friendly))
        {
            YouDied();
        }
        else if (teams.CheckIfTurnEnd(playerTurn))
        {
            EndTurn();
        }
        else if(EnemyController)
        {
            GetEnemyMoves();
        }
    }
}
