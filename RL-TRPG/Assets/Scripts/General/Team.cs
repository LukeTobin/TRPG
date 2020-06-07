using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    /*
     * Team information holder 
     */


    public Unit leader; // leader unit
    public List<Unit> units = new List<Unit>(); // list of units

    [Header("Admin Testing")]
    public bool checkTeamHealth;

    private void Awake()
    {
        DontDestroyOnLoad(this); // allow the gameobject to go through scenes
    }

    private void Start()
    {
        // setting leader unit
        leader.isLeader = true;
        leader.health = leader.maxHealth;
    }

    private void Update()
    {
        if (checkTeamHealth)
        {
            for (int i = 0; i < units.Count; i++)
            {
                Debug.Log(units[i].health);
            }

            checkTeamHealth = false;
        }
    }

    public void TestCall()
    {
        Debug.Log("hello world");
    }

    public void AddNewUnit(Unit unit)
    {
        unit.health = unit.maxHealth;
        units.Add(unit);
    }
}
