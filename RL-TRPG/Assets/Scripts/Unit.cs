using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public bool selected;
    GameController gc;
    TeamHandler team;
    public SpriteRenderer sr;

    // Core Identity of unit
    [Header("Identity")]
    public string title;
    public int moveSpeed;
    public int range;

    // Base Stats
    [Header("Base Stats")]
    public int health;
    public int attackDamage; // both physical & magical
    public int armor;

    // Ability Info
    [Header("Ability Info")]
    public bool AllyInteractable;

    // Extra
    [Header("Public checks")]
    public bool hasMoved;
    public bool hasAttacked;

    [Header("1 = Player      2 = Enemy")]
    public int playerNumber;

    public List<Unit> enemiesInRange = new List<Unit>();

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        sr = GetComponent<SpriteRenderer>();
        team = gameObject.GetComponentInParent<TeamHandler>();

        range *= gc.cellSize;
        moveSpeed *= gc.cellSize;
    }

    void OnMouseDown()
    {
        if (selected)
        {
            gc.selectedUnit = null;
            selected = false;
            gc.ResetTiles();
        }
        else
        {
            if (playerNumber == gc.playerTurn)
            {
                if (gc.selectedUnit != null)
                {
                    gc.selectedUnit.selected = true;
                }

                selected = true;
                gc.selectedUnit = this;
                gc.ResetTiles();
                GetEnemies();
                GetWalkableTiles();
            }
        }

        Collider2D col = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.15f);
        Unit unit = col.GetComponent<Unit>();

        if (gc.selectedUnit != null)
        {
            if (gc.selectedUnit.enemiesInRange.Contains(unit) && !gc.selectedUnit.hasAttacked)
            {
                gc.selectedUnit.Attack(unit);
                Debug.Log("Attacking!");
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("showing options");
            gc.CreateOptionBox(this, transform.position);
        }
    }

    void Attack(Unit enemy)
    {
        gc.optionBox.SetActive(false);

        int enemyDamage = attackDamage - enemy.armor;
        Debug.Log(name + " attacked " + enemy.name);

        if (enemyDamage >= 1)
        {
            enemy.health -= enemyDamage;
        }

        if (enemy.health <= 0)
        {
            Destroy(enemy.gameObject);
            GetWalkableTiles();
        }

        if (health <= 0)
        {
            gc.ResetTiles();
            Destroy(this.gameObject);
            team.UpdateUnitsToMove();
        }

        hasMoved = true;
        hasAttacked = true;

        team.unitsMovable--;
        sr.color = new Color(1, 1, 1, 150);
        team.CheckIfEnd();
        gc.ResetTiles();
    }

    void GetWalkableTiles()
    {
        if (hasMoved && hasAttacked)
        {
            return;
        }
        else if (hasMoved && !hasAttacked)
        {
            foreach (Tile tile in FindObjectsOfType<Tile>())
            {
                if (Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= range)
                {
                    if (tile.IsClear(transform.position, range, AllyInteractable))
                    {
                        tile.ShowRange();
                    }
                }
            }
        }
        else
        {
            foreach (Tile tile in FindObjectsOfType<Tile>())
            {
                if (Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= moveSpeed + range)
                {
                    if (tile.IsClear(transform.position, range + moveSpeed, AllyInteractable))
                    {
                        tile.ShowRange();
                    }
                }

                if (Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= moveSpeed)
                {
                    if (tile.IsClear(transform.position, moveSpeed, AllyInteractable))
                    {
                        tile.Highlight();
                    }
                }
            }
        }
    }

    void GetEnemies()
    {
        enemiesInRange.Clear();

        foreach (Unit enemy in FindObjectsOfType<Unit>())
        {
            if (Mathf.Abs(transform.position.x - enemy.transform.position.x) + Mathf.Abs(transform.position.y - enemy.transform.position.y) <= range)
            {
                if (enemy.playerNumber != gc.playerTurn)
                {
                    enemiesInRange.Add(enemy);
                    Debug.Log("Enemies in range: " + enemiesInRange.Count);
                }
            }
        }
    }

    public void Move(Vector2 tilePos)
    {
        gc.ResetTiles();
        gc.optionBox.SetActive(false);
        StartCoroutine(StartMove(tilePos));
    }

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

        gc.selectedUnit.hasMoved = true;
        hasMoved = true;
        gc.selectedUnit = null;
        selected = false;

        GetEnemies();

        if (hasMoved && hasAttacked)
        {
            team.unitsMovable--;
            sr.color = new Color(1, 1, 1, 150);
            team.CheckIfEnd();
        }
    }
}
