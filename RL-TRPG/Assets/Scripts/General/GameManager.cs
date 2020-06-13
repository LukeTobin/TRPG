using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentStage = 0;
    public int gold = 50;

    [Header("Friendly Pool")]
    public List<Unit> friendlyList = new List<Unit>();

    [Header("Enemy Pool")]
    public List<Unit> enemyList = new List<Unit>();

    [Header("Artifact Pool")]
    public List<Artifact> artifactList = new List<Artifact>();

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Die()
    {

        Team team = FindObjectOfType<Team>();
        Destroy(team.gameObject);

        SceneManager.LoadScene("MainMenu");

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                PlayerPrefs.DeleteKey(i + "-" + j + ".visited");
            }
        }

        PlayerPrefs.SetInt("continued", 0);
        PlayerPrefs.SetInt("stage", 0);
        PlayerPrefs.Save();

        Destroy(gameObject);
    }

    public void NewPrefSet()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                PlayerPrefs.DeleteKey(i + "-" + j + ".visited");
            }
        }

        PlayerPrefs.SetInt("continued", 0);
        PlayerPrefs.SetInt("stage", 0);
        PlayerPrefs.Save();
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
}
