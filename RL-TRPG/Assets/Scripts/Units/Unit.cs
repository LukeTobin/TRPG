using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{

    /*
     * Unit script for all units and enemies in game - if needs be the script can be refactored into various other scripts containing these main features.
     * 
     * - Variables: All stats, accessable scripts & components
     * 
     * - Start / Load: For Start(), Awake() and general loading
     * 
     * - Selecting: Right or left clicking on a unit and managing what can be done when they're selected
     * 
     * - Attacking: Manages unit attacking and calculations
     * 
     * - Movement & Tiles: Gets tiles moveable too, enemies in range and handles path movement found by A*
     * 
     * - Miscellaneous: For extra functions related to unit script
     * 
     */

    #region Variables
    GameController gc;
    TeamHandler team;
    Healthbar hb;
    Pathfinding path;
    StatsLoader sl;

    public enum PreferredDamage
    {
        physical,
        magic,
        mixed
    }

    // Core Identity of unit
    [Header("Identity")]
    public string title;
    public int moveSpeed;
    public int range;
    public PreferredDamage damageType;


    // Base Stats
    // can be refactored from max to base at a later time, doesnt matter right now
    [Header("Base Stats")]
    public int maxHealth;
    public int maxAttackDamage;
    public int maxMagicDamage;
    public int maxArmor;
    public int maxResist;
    public int maxSpeed;

    // Ability Info
    [Header("Ability Info")]
    public bool AllyInteractable;

    //UI
    [Header("UI")]
    public Sprite profile;
    public SpriteRenderer sr;

    // Extra
    [Header("Public checks")]
    public bool selected;
    public bool hasMoved;
    public bool hasAttacked;

    // Current stats
    [Header("Public/Current Stats")]
    public int health;
    public int mana;
    public int attackDamage;
    public int magicDamage;
    public int armor;
    public int resist;
    public int speed;
    [Space]
    public int armorPen;
    public int mrPen;
    public int manaGrowth;

    [Header("One Turn Stats - Leave 0")]
    public int tempAD;
    public int tempMD;
    public int tempAR;
    public int tempMR;
    public int tempSP;

    [Header("Player Stats")]
    public int playerNumber = 1;
    public bool isLeader;

    [Space]
    public List<Unit> enemiesInRange = new List<Unit>(); // what enemies are in range of unit

    #endregion

    #region Start / Load
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        sr = GetComponent<SpriteRenderer>();
        sl = FindObjectOfType<StatsLoader>();

        team = gameObject.GetComponentInParent<TeamHandler>();
        hb = GetComponentInChildren<Healthbar>();
        path = GetComponent<Pathfinding>();

        //hb.gameObject.SetActive(false);

        // range & tiles they can move is multiplyed by the size of a cell so they can move properly
        range *= gc.cellSize;
        moveSpeed *= gc.cellSize;

        mana = 0;
        tempAD = 0;
        tempMD = 0;
        tempAR = 0;
        tempMR = 0;
        tempSP = 0;
    }

    #endregion

    #region Selecting
    // when unit is selected
    void OnMouseDown()
    {
        if (selected)
        {
            // remove this unit being selected
            gc.selectedUnit = null;
            selected = false;
            gc.ResetTiles();
        }
        else
        {
            if (playerNumber == gc.playerTurn)
            {
                // if the unit is yours, it can be selected
                if (gc.selectedUnit != null)
                {
                    gc.selectedUnit.selected = true;
                }

                // set the unit to selected and find all information about it
                selected = true;
                gc.selectedUnit = this;
                gc.ResetTiles();
                GetEnemies();
                GetWalkableTiles();
                team.UpdateUnitsToMove();
            }
        }


        // if the unit is already selected & you select another enemy unit. Attack that unit
        // create a 2d collider to check where your mouse is
        Collider2D col = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.15f);
        Unit unit = col.GetComponent<Unit>();

        if (gc.selectedUnit != null)
        {
            // check if the unit you selected is in range & you havent yet attacked
            if (gc.selectedUnit.enemiesInRange.Contains(unit) && !gc.selectedUnit.hasAttacked) 
            {
                // attack selected enemy unit
               // gc.optionBox.SetActive(false);
                gc.selectedUnit.Attack(unit);
                fade(true);
            }
        }
    }

    // create options menu - TODO
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            gc.CreateOptionBox(this, transform.position);
        }
    }
    #endregion

    #region Attacking
    void Attack(Unit enemy)
    {
        int enemyDamage;

        // get damage to enemy
        switch (damageType)
        {
            case PreferredDamage.magic:
                enemyDamage = magicDamage - enemy.resist;
                break;
            case PreferredDamage.physical:
                enemyDamage = attackDamage - enemy.armor;
                break;
            case PreferredDamage.mixed:
                enemyDamage = ((attackDamage / 2) + (magicDamage / 2)) - ((enemy.armor / 2) + (enemy.resist / 2));
                break;
            default:
                enemyDamage = 0;
                Debug.LogError("Could not find damage type");
                break;
        }

        // if the enemy damage actually does something
        if (enemyDamage >= 1)
        {
            // damage enemy & update ui for it
            enemy.health -= enemyDamage;
            sl.UpdateStatBox();
            //enemy.hb.SetSize(enemy.maxHealth, enemy.health); // update healthbar
        }

        // if enemy unit dies, kill it & update tiles
        if (enemy.health <= 0)
        {
            gc.KillUnit(enemy, enemy.playerNumber);
            GetWalkableTiles();
        }

        // check if this unit died
        if (health <= 0)
        {
            gc.ResetTiles();
            gc.KillUnit(this, playerNumber);
            team.UpdateUnitsToMove();
        }

        // setting units moved & attacked to true
        hasMoved = true;
        hasAttacked = true;

        // updating that we have moved
        team.unitsMovable--;
        team.CheckIfEnd();
        gc.ResetTiles();

        gc.CheckEnd();
    }
    #endregion

    #region Movement & Tiles
    void GetWalkableTiles()
    {
        if (hasMoved && hasAttacked)
        {
            return; // do nothing if unit has moved & attacked
        }
        else if (hasMoved && !hasAttacked)
        {
            // check if any tile around the unit is in range of their attack
            foreach (Tile tile in FindObjectsOfType<Tile>())
            {
                if (Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= range)
                {
                    if (tile.IsClear(transform.position, range, AllyInteractable)) // check if tile is clear
                    {
                        tile.ShowRange(); // if so, show range color
                    }
                }
            }
        }
        else
        {
            // check if any tile around the unit is in range of their attack & movement
            foreach (Tile tile in FindObjectsOfType<Tile>())
            {
                if (Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= moveSpeed + range)
                {
                    if (tile.IsClear(transform.position, range + moveSpeed, AllyInteractable)) // check if tile is clear
                    {
                        tile.ShowRange(); // if so, show range color
                    }
                }

                if (Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= moveSpeed)
                {
                    if (tile.IsClear(transform.position, moveSpeed, AllyInteractable)) // check if tile is clear
                    {
                        tile.Highlight(); // highlight tile if in range
                    }
                }
            }
        }
    }

    void GetEnemies() // get all enemies in range of selected unit
    {
        enemiesInRange.Clear(); // remove all enemies in range within the list

        foreach (Unit enemy in FindObjectsOfType<Unit>()) // go through each enemy unit
        {
            // check if enemy unit is in range to be attacked by unit
            if (Mathf.Abs(transform.position.x - enemy.transform.position.x) + Mathf.Abs(transform.position.y - enemy.transform.position.y) <= range)
            {
                if (enemy.playerNumber != gc.playerTurn)
                {
                    // if the unit is, add it to the list
                    enemiesInRange.Add(enemy);
                }
            }
        }
    }

    // move unit to the selected tile position
    public void Move(Vector2 tilePos)
    {
        //gc.optionBox.SetActive(false);
        List<Vector2> movePath = path.FindPath(transform.position, tilePos);
        if(movePath != null)
            MakeMove(movePath);
        //StartCoroutine(StartMove(tilePos));
        gc.ResetTiles();
    }

    void MakeMove(List<Vector2> dir)
    {

        StartCoroutine(StartMove(dir[dir.Count-1]));
        
        // state that the unit has moved & unselect it
        gc.selectedUnit.hasMoved = true;
        hasMoved = true;
        gc.selectedUnit = null;
        selected = false;

        // update enemies in range
        GetEnemies();

        // if they moved & attacked, remove them from movable units & 
        if (hasMoved && hasAttacked)
        {
            team.unitsMovable--;
            team.CheckIfEnd();
        }
    }

    // move unit
    IEnumerator StartMove(Vector2 tilePos)
    {
        while (transform.position.x != tilePos.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(tilePos.x, transform.position.y), gc.GameSpeed * Time.deltaTime);
            yield return null;
        }

        while (transform.position.y != tilePos.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, tilePos.y), gc.GameSpeed * Time.deltaTime);
            yield return null;
        }
    }
    #endregion

    #region Miscellaneous
    public void fade(bool used)
    {
        if (used)
        {
            sr.color = new Color(.6f, .6f, .6f, 1f);
        }
        else
        {
            sr.color = new Color(1, 1, 1, 1);
        }
    }

    #endregion
}
