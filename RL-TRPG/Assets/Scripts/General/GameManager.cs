using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Current Run Information")]
    public int currentStage = 1;
    public int gold = 50;
    public int score = 0;
    public Node currentNode;
    public bool state = false;

    [Header("Trait Images")]
    public Sprite human;
    public Sprite tiefling;
    public Sprite nymph;
    public Sprite satyr;
    [Space]
    public Sprite caster;
    public Sprite enchanter;
    public Sprite brawler;
    public Sprite ranger;
    public Sprite dueler;
    [Space]
    public Sprite unknown;

    [Header("Friendly Pool")]
    public List<Unit> friendlyList = new List<Unit>();

    [Header("Enemy Pool")]
    public List<Unit> enemyList = new List<Unit>();

    [Header("Artifact Pool")]
    public List<Artifact> artifactList = new List<Artifact>();

    [Header("Admin Tools")]
    public bool ResetPrefs;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (ResetPrefs)
        {
            PlayerPrefs.DeleteAll();
            print("PlayerPrefs Reest!");
            ResetPrefs = false;
        }
    }
    public void Die()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                PlayerPrefs.DeleteKey(i + "-" + j + ".visited");
            }
        }

        PlayerPrefs.SetInt("active", 0);
        PlayerPrefs.SetInt("continued", 0);
        PlayerPrefs.SetInt("stage", 0);
        PlayerPrefs.SetInt("storedScore", 0);
        PlayerPrefs.Save();

        state = false;

        // go to rewardscene;
        SceneManager.LoadScene("RewardScene");

        //GainXP(score, true);
    }

    public void YouWin()
    {
        Team team = FindObjectOfType<Team>();
        int wins = PlayerPrefs.GetInt(team.leader.title + ".wins") + 1;
        PlayerPrefs.SetInt(team.leader.title + ".wins", wins);
        PlayerPrefs.Save();
        
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                PlayerPrefs.DeleteKey(i + "-" + j + ".visited");
            }
        }

        PlayerPrefs.SetInt("active", 0);
        PlayerPrefs.SetInt("continued", 0);
        PlayerPrefs.SetInt("stage", 0);
        PlayerPrefs.SetInt("storedScore", 0);
        PlayerPrefs.Save();

        state = true;

        SceneManager.LoadScene("RewardScene");

        //GainXP(score, false);
    }

    public void AbandonRun()
    {
        Die();
    }

    public void NewPrefSet()
    {
        Team team = FindObjectOfType<Team>();

        for (int i = 0; i < friendlyList.Count; i++)
        {
            PlayerPrefs.SetInt(friendlyList[i].title + ".leader", 0);
            PlayerPrefs.SetInt(friendlyList[i].title + ".health", friendlyList[i].maxHealth);
            PlayerPrefs.SetInt(friendlyList[i].title, 0);
        }

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                PlayerPrefs.DeleteKey(i + "-" + j + ".visited");
            }
        }

        for (int i = 0; i < artifactList.Count; i++)
        {
            PlayerPrefs.SetInt(artifactList[i].artifactName, 0);
            PlayerPrefs.Save();
        }

        currentStage = 1;
        PlayerPrefs.SetInt("active", 0);
        PlayerPrefs.SetInt("continued", 0);
        PlayerPrefs.SetInt("stage", 1);
        PlayerPrefs.SetInt("storedScore", 0);
        PlayerPrefs.Save();
    }

    public void LoadSavedTeam()
    {
        Team team = FindObjectOfType<Team>();

        for (int i = 0; i < friendlyList.Count; i++)
        {
            if(PlayerPrefs.GetInt(friendlyList[i].title + ".leader") == 1)
            {
                team.leader = friendlyList[i];
                team.leader.health = PlayerPrefs.GetInt(friendlyList[i].title + ".health");
            }
            else if(PlayerPrefs.GetInt(friendlyList[i].title) == 1)
            {
                team.LoadUnit(friendlyList[i]);
            }
        }

        for (int i = 0; i < artifactList.Count; i++)
        {
            if(PlayerPrefs.GetInt(artifactList[i].artifactName) == 1)
            {
                team.artifacts.Add(artifactList[i]);
            }
        }

        currentStage = PlayerPrefs.GetInt("stage");
    }

    public List<Unit> CreateRecruitList()
    {
        // create a unique recruit list
        List<Unit> recruitList = new List<Unit>();

        Team team = FindObjectOfType<Team>();
        List<Unit> closedList = team.units;
        List<Unit> possibleList = new List<Unit>();

        foreach(Unit unit in friendlyList)
        {
            if (!closedList.Contains(unit))
            {
                possibleList.Add(unit);
            }
        }

        for (int i = 0; recruitList.Count < 4; i++)
        {
            // need to check for dup's
            Unit tempUnit = possibleList[Random.Range(0, possibleList.Count)];
            if (!recruitList.Contains(tempUnit))
            {
                recruitList.Add(tempUnit);
            }
        }

        return recruitList;

    }

    public List<Artifact> CreateArtifactList()
    {
        List<Artifact> optionList = new List<Artifact>();

        Team team = FindObjectOfType<Team>();
        List<Artifact> closedList = team.artifacts;
        List<Artifact> openList = new List<Artifact>();

        foreach(Artifact artifact in artifactList)
        {
            if(closedList.Count != 0)
            {
                if (!closedList.Contains(artifact))
                {
                    openList.Add(artifact);
                }
            }
            else
            {
                openList.Add(artifact);
            }  
        }

        for (int i = 0; optionList.Count < 4; i++)
        {
            Artifact temp = openList[Random.Range(0, openList.Count)];
            if (!optionList.Contains(temp))
            {
                optionList.Add(temp);
            }
        }

        return optionList;
    }

    public void ReturnToMainMenu(bool playerLose)
    {
        Team team = FindObjectOfType<Team>();
        Destroy(team.gameObject);

        PlayerPrefs.SetInt("active", 0);
        PlayerPrefs.SetInt("continued", 0);
        PlayerPrefs.SetInt("stage", 0);
        PlayerPrefs.Save();

        SceneManager.LoadScene("MainMenu");

        Destroy(gameObject);
    }

    public Sprite GetClassImage(Traits.Class c)
    {
        switch (c)
        {
            case Traits.Class.caster:
                return caster;
            case Traits.Class.enchanter:
                return enchanter;
            case Traits.Class.dueler:
                return dueler;
            case Traits.Class.brawler:
                return brawler;
            case Traits.Class.ranger:
                return brawler;
            default:
                return unknown;
        }
    }

    public Sprite GetOriginImage(Traits.Origin o)
    {
        switch (o)
        {
            case Traits.Origin.human:
                return human;
            case Traits.Origin.tiefling:
                return tiefling;
            case Traits.Origin.nymph:
                return nymph;
            case Traits.Origin.satyr:
                return satyr;
            default:
                return unknown;
        }
    }
}
