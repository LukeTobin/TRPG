using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamHandler : MonoBehaviour
{

    /*
     * Team information handler for in combat
     */

    GameController gc;
    GameManager gm;
    Team team;

    public LayerMask tileLayer;

    [Header("Team stats")]
    public int friendlyUnitsMovable;
    public int fAttack;
    public int enemyUnitsMovable;
    public int eAttack;
    
    [Space]
    public List<Unit> friendlyUnits = new List<Unit>();
    public List<Unit> enemyUnits = new List<Unit>();
    [Space]
    public List<Unit> deadUnits = new List<Unit>();

    List<Unit> allUnits = new List<Unit>();
    [SerializeField] List<Tile> openList = new List<Tile>();

    public enum UpdateType
    {
        friendly,
        enemy,
        all
    }

    GameObject enemyContainer;
    GameObject friendlyContainer;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        team = FindObjectOfType<Team>(); // lets us access team object from map scene.

        enemyContainer = new GameObject("Enemy Team");
        friendlyContainer = new GameObject("Friendly Team");

        SpawnUnits(true);
        SpawnUnits(false);

        GetAllUnits();

        UpdateUnitsToMove(UpdateType.all); // update what units need to be able to move
        UpdateUnitsToAttack(UpdateType.all);
    }

    void SpawnUnits(bool friendly)
    {
        if (friendly)
        {
            // fill in all units on the board from your team list

            if (team.units != null)
            {
                GetAvailableTiles(0, 3);

                Unit teamLeader = Instantiate(team.leader, NewGetRandomTile(), Quaternion.identity);
                teamLeader.transform.parent = friendlyContainer.transform;
                friendlyUnits.Add(teamLeader);

                for (int i = 0; i < team.units.Count; i++)
                {
                    Unit unit = Instantiate(team.units[i], GetRandomTile(0, 3), Quaternion.identity);
                    unit.transform.parent = friendlyContainer.transform; ;
                    friendlyUnits.Add(unit);
                }
            }
        }
        else if(!friendly)
        {
            int enm = gm.currentStage * Random.Range(1, 3);
            GetAvailableTiles(gc.x - 3, gc.x);
            for (int i = 0; i < enm; i++)
            {
                Unit enemy = Instantiate(gm.enemyList[Random.Range(0, gm.enemyList.Count)], NewGetRandomTile(), Quaternion.identity);
                enemy.transform.parent = enemyContainer.transform;

                enemy.health += gm.currentStage + Random.Range(0, 4);
                enemy.attackDamage += gm.currentStage + Random.Range(0, 2);
                enemy.armor += gm.currentStage + Random.Range(0, 3);
                enemy.resist += gm.currentStage + +Random.Range(0, 3);

                enemyUnits.Add(enemy);
            }
        }
    }

    void GetAvailableTiles(int x1, int x2)
    {
        openList.Clear();
        x1 *= 2; x2 *= 2;
        int maxY = gc.y * 2;

        for (int x = x1; x < x2; x+=2)
        {
            for (int y = 0; y < maxY; y+=2)
            {
                Collider2D obstacle = Physics2D.OverlapCircle(new Vector2(x, y), 0.4f, tileLayer);
                if(obstacle != null)
                {
                    if (obstacle.gameObject.GetComponent<Tile>().isEmpty())
                    {
                        openList.Add(obstacle.gameObject.GetComponent<Tile>());
                    }
                }
            }
        }
    }

    Vector3 NewGetRandomTile()
    {
        Vector3 tile;

        Tile mTile = openList[Random.Range(0, openList.Count - 1)];
        tile = mTile.transform.position;
        openList.Remove(mTile);

        return tile;
    }

    Vector3 GetRandomTile(int x1, int x2)
    {
        Vector3 tile;
        int yRange = Random.Range(0, gc.y);
        int xRange = Random.Range(x1, x2);

        if(xRange % 2 != 0)
        {
            if ((xRange + 1) * 2 > gc.x)
            {
                xRange--;
            }
            else if((xRange - 1) * 2 < gc.x)
            {
                xRange++;
            }
            else
            {
                xRange++;
            }
        }

        if (yRange % 2 != 0)
        {
            if ((yRange + 1) * 2 > gc.y)
            {
                yRange--;
            }
            else if ((yRange - 1) * 2 < gc.y)
            {
                yRange++;
            }
            else
            {
                yRange++;
            }
        }

        yRange *= gc.cellSize;
        xRange *= gc.cellSize;

        tile = new Vector3(xRange, yRange, 0);

        if (!gc.map.TileFree(tile) )
        {
            GetRandomTile(x1, x2);
        }

        return tile;
    }

    void GetAllUnits()
    {
        allUnits.Clear();

        for (int i = 0; i < friendlyUnits.Count; i++)
        {
            allUnits.Add(friendlyUnits[i]);
        }

        for (int j = 0; j < enemyUnits.Count; j++)
        {
            allUnits.Add(enemyUnits[j]);
        }
    }

    /// <summary>
    /// Updates all units which need to be moved.
    /// </summary>
    /// <param name="units"></param>
    public void UpdateUnitsToMove(UpdateType units)
    {
        friendlyUnitsMovable = 0;
        enemyUnitsMovable = 0;

        if(units == UpdateType.all)
        {
            GetAllUnits();
            foreach(Unit unit in allUnits)
            {
                if(!unit.hasMoved && !unit.hasAttacked)
                {
                    if(unit.playerNumber == 1)
                    {
                        friendlyUnitsMovable++;
                    }
                    else
                    {
                        enemyUnitsMovable++;
                    }
                }
            }
        }
        else if (units == UpdateType.friendly)
        {
            foreach(Unit unit in friendlyUnits)
            {
                if (!unit.hasMoved && !unit.hasAttacked)
                {
                    friendlyUnitsMovable++;
                }
            }
        }
        else if (units == UpdateType.enemy)
        {
            foreach (Unit unit in enemyUnits)
            {
                if (!unit.hasMoved && !unit.hasAttacked)
                {
                    enemyUnitsMovable++;
                }
            }
        }
    }

    public void UpdateUnitsToAttack(UpdateType team)
    {
        fAttack = 0;
        eAttack = 0;

        if (team == UpdateType.all)
        {
            GetAllUnits();
            foreach (Unit unit in allUnits)
            {
                if (!unit.hasAttacked)
                {
                    if (unit.playerNumber == 1)
                    {
                        fAttack++;
                    }
                    else
                    {
                        eAttack++;
                    }
                }
            }
        }
        else if (team == UpdateType.friendly)
        {
            foreach (Unit unit in friendlyUnits)
            {
                if (!unit.hasAttacked)
                {
                    fAttack++;
                }
            }
        }
        else if (team == UpdateType.enemy)
        {
            foreach (Unit unit in enemyUnits)
            {
                if (!unit.hasAttacked)
                {
                    eAttack++;
                }
            }
        }
    }

    public bool CheckIfTurnEnd(int team)
    {
        if (team == 1)
        {
            if (friendlyUnitsMovable <= 0 && fAttack <= 0)
                return true;
            //else if (friendlyUnitsMovable <= 0 && gc.autoEnd)
            //    return NoUnitsInRange(UpdateType.friendly);
            else
                return false;
        }
        else
        {
            if (enemyUnitsMovable <= 0 && eAttack <= 0)
                return true;
            else if (enemyUnitsMovable <= 0)
                return NoUnitsInRange(UpdateType.enemy);
            else
                return false;
        }
    }

    public bool CheckIfAllDead(UpdateType team)
    {
        if (team == UpdateType.friendly)
        {
            if (friendlyUnits.Count <= 0)
                return true;
            else
                return false;
        }
        else if(team == UpdateType.enemy)
        {
            if (enemyUnits.Count <= 0)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    bool NoUnitsInRange(UpdateType team)
    {
        if(team == UpdateType.friendly)
        {
            foreach(Unit unit in friendlyUnits)
            {
                if(!unit.hasAttacked && unit.hasMoved)
                {
                    foreach (Tile tile in FindObjectsOfType<Tile>())
                    {
                        if (Mathf.Abs(unit.transform.position.x - tile.transform.position.x) + Mathf.Abs(unit.transform.position.y - tile.transform.position.y) <= unit.range)
                        {
                            if (tile.unitOccupying(unit.transform.position, unit.range, 1)) // check if tile is clear
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        else if (team == UpdateType.enemy)
        {
            foreach (Unit unit in enemyUnits)
            {
                if (!unit.hasAttacked && unit.hasMoved)
                {
                    foreach (Tile tile in FindObjectsOfType<Tile>())
                    {
                        if (Mathf.Abs(unit.transform.position.x - tile.transform.position.x) + Mathf.Abs(unit.transform.position.y - tile.transform.position.y) <= unit.range)
                        {
                            if (tile.unitOccupying(unit.transform.position, unit.range, 2)) // check if tile is clear
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }

        Debug.Log("no unit in range");
        return false;
    }
}
