using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamHandler : MonoBehaviour
{

    /*
     * Team information handler for in combat
     */

    GameController gc;
    Team team;

    [Header("Team stats")]
    public int side; // [1 - 2]
    [SerializeField] int teamSize;
    public int unitsMovable;

    bool youDied;

    [Space]
    public List<Unit> children = new List<Unit>();

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        team = FindObjectOfType<Team>(); // lets us access team object from map scene.

        switch (side)
        {
            case 1:
                // friendly team
                // make sure the unit is spawned properly
                float spawnX = 2f;
                float spawnY = gc.y / 2f;

                if (spawnX % 2 != 0)
                {
                    spawnX++;
                }

                if (spawnY % 2 != 0)
                {
                    spawnY++;
                }

                // spawn in leader - needs refactoring
                Instantiate(team.leader, new Vector2(spawnX, spawnY), Quaternion.identity, transform.parent = transform);

                // fill in all units on the board from your team list

                if (team.units != null)
                {
                    for (int i = 0; i < team.units.Count; i++)
                    {
                        float yRange = Random.Range(0, gc.y / 2); yRange *= gc.cellSize;
                        float xRange = Random.Range(0, gc.x / 2); xRange *= gc.cellSize;

                        Unit unit = Instantiate(team.units[i], new Vector3(xRange, yRange, 0), Quaternion.identity, transform.parent = transform);
                        unit.transform.parent = transform;
                    }
                }
                break;
            case 2:
                // enemy spawning stuff
                break;
            default:
                break;
        }

        UpdateUnitsToMove(); // update what units need to be able to move
        CountStart();
    }

    void CountStart()
    {
        switch (side)
        {
            case 1:
                gc.friendly = teamSize;
                break;
            case 2:
                gc.enemy= teamSize;
                break;
            default:
                break;
        }
    }

    public void CheckIfEnd()
    {
        if (CheckIfAllDead())
        {
            gc.Rewards();
        }
        else if (CheckIfAllDead() && youDied)
        {
            gc.YouDied();
            youDied = false;
        }
        else
        {
            // check if round needs to end
            if (unitsMovable == 0)
            {
                gc.EndTurn();
                UpdateUnitsToMove();
            }
        }

        
    }

    /*
     * // fill in a list of units which can be moved
        // can possibily be refactored, in case we want to spawn into additional friendly units within a round (summoner ability?)

        unitsMovable = 0;
        units.Clear();

        GameObject[] unitsToSearch = GameObject.FindGameObjectsWithTag("Unit");
        for (int i = 0; i < unitsToSearch.Length; i++)
        {
            if(unitsToSearch[i].GetComponent<Unit>().playerNumber == side)
            {
                unitsMovable++;
                units.Add(unitsToSearch[i].GetComponent<Unit>());
            }
        }

        teamSize = unitsMovable; // size of team is equal to units movable

        if(teamSize == 0)
        {
            Debug.LogError("Team Null");
        }

        //CheckIfEnd(); // incase we no longer have a team..
     */

    public void UpdateUnitsToMove()
    {
        // experimental
        Unit[] child = gameObject.GetComponentsInChildren<Unit>();

        children.Clear();

        foreach(Unit unit in child)
        {
            children.Add(unit);
        }

        unitsMovable = children.Count;
        teamSize = children.Count;
    }



    public bool CheckIfAllDead()
    {
        if(children.Count <= 0)
        {
            // all dead.
            switch (side)
            {
                case 1: // win
                    gc.ended = true;
                    return true;
                case 2: // lose
                    youDied = true;
                    gc.ended = true;
                    return true;
                default:
                    return false;
            }
        }
        else
        {
            return false;
        }
    }
}
