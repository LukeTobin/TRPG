using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Campfire : MonoBehaviour
{
    public GameObject campfireGUI;
    [Space]
    public Button healing;
    public Button boost;

    Team team;

    // Start is called before the first frame update
    void Start()
    {
        campfireGUI.SetActive(false);

        team = FindObjectOfType<Team>();

        healing.onClick.AddListener(HealParty);
        boost.onClick.AddListener(BoostParty);
    }

    public void SetCampfireActive()
    {
        campfireGUI.SetActive(true);
    }

    void HealParty()
    {
        foreach(Unit unit in team.units)
        {
            if(unit.health != unit.maxHealth)
            {
                int val = Mathf.CeilToInt((unit.maxHealth / 100) * 40);

                if (unit.health + val >= unit.maxHealth)
                    unit.health = unit.maxHealth;
                else
                    unit.health += val;
            }
        }

        campfireGUI.SetActive(false);
    }

    void BoostParty()
    {
        foreach (Unit unit in team.units)
        {
            unit.moral++;
        }

        campfireGUI.SetActive(false);
    }
}
