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

    void Start()
    {
        gc = FindObjectOfType<GameController>();

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
