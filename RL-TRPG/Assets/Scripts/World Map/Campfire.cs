using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Campfire : MonoBehaviour
{
    GameManager gm;

    public GameObject campfireGUI;
    [Space]
    public Button healing;
    public Button boost;
    public Button explore;

    [Header("Found Box")]
    public GameObject box;
    public Image image;

    Team team;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        team = FindObjectOfType<Team>();

        healing.onClick.AddListener(HealParty);
        boost.onClick.AddListener(BoostParty);
        explore.onClick.AddListener(Explore);

        campfireGUI.SetActive(false);
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

    void Explore()
    {
        int x = Random.Range(1, 11);
        if (x <= 2)
        {
            box.GetComponent<FoundBox>().Fill(false, false);
            box.SetActive(true);
            healing.gameObject.SetActive(false);
            boost.gameObject.SetActive(false);
            explore.gameObject.SetActive(false);
        }
        else if(x > 2 && x <= 6)
        {
            box.GetComponent<FoundBox>().Fill(true, false, (int)Random.Range(30, 100));
            box.SetActive(true);
            healing.gameObject.SetActive(false);
            boost.gameObject.SetActive(false);
            explore.gameObject.SetActive(false);
        }
        else if(x > 6 && x <= 10)
        {
            Artifact temp = gm.ReturnRandomArtifact();
            box.GetComponent<FoundBox>().Fill(false, true, 0, temp);
            box.SetActive(true);
            healing.gameObject.SetActive(false);
            boost.gameObject.SetActive(false);
            explore.gameObject.SetActive(false);
        }
    }
}
