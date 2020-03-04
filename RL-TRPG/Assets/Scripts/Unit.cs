using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    /*
     * Unit Script
     * For:
     * - Controlling
     * - Attacking
     * - Stats
     * - And more!
     */

    public bool selected;
    GameController gc;
    TeamHandler team;
    Healthbar hb;
    Pathfinding path;
    public SpriteRenderer sr;

    // Core Identity of unit
    [Header("Identity")]
    public string title;
    public int moveSpeed;
    public int range;

    // Base Stats
    [Header("Base Stats")]
    public int maxHealth;
    public int attackDamage; // both physical & magical
    public int armor;

    // Ability Info
    [Header("Ability Info")]
    public bool AllyInteractable;

    //UI
    [Header("UI")]

    // Extra
    [Header("Public checks")]
    public bool hasMoved;
    public bool hasAttacked;

    [Header("Public Stats")]
    public int health;

    [Header("1 = Player      2 = Enemy")]
    public int playerNumber;
    public bool isLeader;

    public List<Unit> enemiesInRange = new List<Unit>(); // what enemies are in range of unit

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        sr = GetComponent<SpriteRenderer>();
        team = gameObject.GetComponentInParent<TeamHandler>();
        hb = GetComponentInChildren<Healthbar>();
        path = GetComponent<Pathfinding>();

       // hb.gameObject.SetActive(false);
        
        health = maxHealth; // current health = maxHealth, when starting. Needs to be refactored.

        // range & tiles they can move is multiplyed by the size of a cell so they can move properly
        range *= gc.cellSize;
        moveSpeed *= gc.cellSize;
    }

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
                gc.selectedUnit.Attack(unit);
                Debug.Log("Attacking!");
            }
        }
    }

    // create options menu - TODO
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("showing options");
            gc.CreateOptionBox(this, transform.position);
        }
    }

    // attack an enemy unit
    void Attack(Unit enemy)
    {
        // close options menu
        gc.optionBox.SetActive(false);

        // damage the enemy will take
        int enemyDamage = attackDamage - enemy.armor;
        Debug.Log(name + " attacked " + enemy.name);

        // if the enemy damage actually does something
        if (enemyDamage >= 1)
        {
            // damage enemy & update ui for it
            enemy.health -= enemyDamage;
            enemy.hb.SetSize(enemy.maxHealth, enemy.health);
        }

        // if enemy unit dies, kill it & update tiles
        if (enemy.health <= 0)
        {
            Destroy(enemy.gameObject);
            GetWalkableTiles();
        }

        // check if this unit died
        if (health <= 0)
        {
            gc.ResetTiles();
            Destroy(this.gameObject);
            team.UpdateUnitsToMove();
        }

        // setting units moved & attacked to true
        hasMoved = true;
        hasAttacked = true;

        // updating that we have moved
        team.unitsMovable--;
        sr.color = new Color(1, 1, 1, 150);
        team.CheckIfEnd();
        gc.ResetTiles();
    }

    public void TakeDamage()
    {

    }

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
                    Debug.Log("Enemies in range: " + enemiesInRange.Count);
                }
            }
        }
    }

    // move unit to the selected tile position
    public void Move(Vector2 tilePos)
    {
        gc.optionBox.SetActive(false);
        List<Vector2> movePath = path.FindPath(transform.position, tilePos);
        if(movePath != null)
            TestMove(movePath);
        //StartCoroutine(StartMove(tilePos));
        gc.ResetTiles();
    }

    void TestMove(List<Vector2> dir)
    {
        Debug.Log("Dir count: " + dir.Count);
        for (int i = 0; i < dir.Count; i++)
        {
            transform.position = dir[i];
        }

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
            sr.color = new Color(1, 1, 1, 150);
            team.CheckIfEnd();
        }
    }

    // move unit
    IEnumerator StartMove(Vector2 tilePos)
    {

        // needs to be refactored to use pathfinding algo like A*

        // if unit is not on x or y of tile position, move to that tile.
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
            sr.color = new Color(1, 1, 1, 150);
            team.CheckIfEnd();
        }
    }
}
