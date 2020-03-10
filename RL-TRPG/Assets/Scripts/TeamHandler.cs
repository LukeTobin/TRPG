﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamHandler : MonoBehaviour
{

    /*
     * Team information handler for in combat
     */

    GameController gc;

    [Header("Team stats")]
    public int side; // [1 - 2]
    [SerializeField] int teamSize;
    public int unitsMovable;

    //GameObject[] unit = new GameObject[Team.units.Count];
    List<Unit> unit = new List<Unit>();

    void Start()
    {
        gc = FindObjectOfType<GameController>();

        switch (side)
        {
            case 1:
                // friendly team
                // make sure the unit is spawned properly
                float spawnX = gc.x / 2f;
                float spawnY = 2f;

                if (spawnX % 2 != 0)
                {
                    spawnX++;
                }

                if (spawnY % 2 != 0)
                {
                    spawnY++;
                }

                // spawn in leader - needs refactoring
                //Instantiate(Team.leader, new Vector2(spawnX, spawnY), Quaternion.identity, transform.parent = transform);

                // fill in all units on the board from your team list
                if (Team.units != null)
                {
                    for (int i = 0; i < Team.units.Count; i++)
                    {
                        float yRange = Random.Range(0, gc.y / 2); yRange *= gc.cellSize;
                        float xRange = Random.Range(0, gc.x / 2); xRange *= gc.cellSize;

                        //unit[i] = Instantiate(Team.units[i], new Vector3(xRange, yRange, 0), Quaternion.identity, transform.parent = transform);

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
    }

    public void CheckIfEnd()
    {
        CheckIfAllDead();

        // check if round needs to end
        if (unitsMovable == 0)
        {
            gc.EndTurn();
            UpdateUnitsToMove();
        } 
    }

    public void UpdateUnitsToMove()
    {
        // fill in a list of units which can be moved
        // can possibily be refactored, in case we want to spawn into additional friendly units within a round (summoner ability?)

        unitsMovable = 0;

        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        for (int i = 0; i < units.Length; i++)
        {
            if(units[i].GetComponent<Unit>().playerNumber == side)
            {
                unitsMovable++;
            }
        }

        teamSize = unitsMovable; // size of team is equal to units movable

        CheckIfEnd(); // incase we nolong have a team..
    }

    void CheckIfAllDead()
    {
        GameObject[] unitsToSearch = GameObject.FindGameObjectsWithTag("Unit");
        List<Unit> units = new List<Unit>();

        units.Clear();

        for (int i = 0; i < unitsToSearch.Length; i++)
        {
            if(unitsToSearch[i].GetComponent<Unit>().playerNumber == side)
            {
                units.Add(unitsToSearch[i].GetComponent<Unit>());
            }
        }

        if(units.Count <= 0)
        {
            // all dead.
            switch (side)
            {
                case 1:

                    break;
                case 2:
                    gc.Rewards();
                    break;
                default:
                    break;
            }
        }
    }
}
