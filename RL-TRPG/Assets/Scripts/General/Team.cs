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
    [Space]
    public List<Artifact> artifacts = new List<Artifact>();

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

    public void AddNewUnit(Unit unit)
    {
        unit.health = unit.maxHealth;
        unit.attackDamage = unit.maxAttackDamage;
        unit.magicDamage = unit.maxMagicDamage;
        unit.armor = unit.maxArmor;
        unit.resist = unit.maxResist;
        unit.speed = unit.maxSpeed;

        units.Add(unit);
    }

    public void AddNewArtifact(Artifact artifact)
    {
        artifacts.Add(artifact);
    }

    public void LoadArtifacts(bool unload = false)
    {
        if (!unload)
        {
            if (artifacts != null)
            {
                for (int i = 0; i < artifacts.Count; i++)
                {
                    Instantiate(artifacts[i], transform);
                }
            }
        }
        else
        {
            Artifact[] searchArtifacts = GetComponentsInChildren<Artifact>();
            if(searchArtifacts != null)
            {
                foreach (Artifact artifact in searchArtifacts)
                {
                    Destroy(artifact.gameObject);
                }
            }
        }
    }
}
