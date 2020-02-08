using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamHandler : MonoBehaviour
{
    GameController gc;

    [Header("Team stats")]
    public int side; // [1 - 2]
    [SerializeField] int teamSize;
    public int unitsMovable;

    GameObject[] unit = new GameObject[Team.units.Count];

    void Start()
    {
        gc = FindObjectOfType<GameController>();

        if(side == 1)
        {
            float spawnX = gc.x / 2f;
            float spawnY = 2f;

            if(spawnX % 2 != 0)
            {
                spawnX++;
            }

            if (spawnY % 2 != 0)
            {
                spawnY++;
            }

            Instantiate(Team.leader, new Vector2(spawnX, spawnY), Quaternion.identity, transform.parent = transform);

            if (Team.units != null)
            {
                for (int i = 0; i < Team.units.Count; i++)
                {
                    float yRange = Random.Range(0, gc.y / 2); yRange *= gc.cellSize;
                    float xRange = Random.Range(0, gc.x / 2); xRange *= gc.cellSize;

                    unit[i] = Instantiate(Team.units[i], new Vector3(xRange, yRange, 0), Quaternion.identity, transform.parent = transform);
                }
            }
        }

        UpdateUnitsToMove();
    }

    public void CheckIfEnd()
    {
        if(unitsMovable == 0)
        {
            gc.EndTurn();
            UpdateUnitsToMove();
        }
    }

    public void UpdateUnitsToMove()
    {
        unitsMovable = 0;

        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        for (int i = 0; i < units.Length; i++)
        {
            if(units[i].GetComponent<Unit>().playerNumber == side)
            {
                unitsMovable++;
            }
        }

        teamSize = unitsMovable;

        CheckIfEnd(); // incase we nolong have a team..
    }
}
