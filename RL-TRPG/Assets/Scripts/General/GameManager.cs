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

    public List<Unit> CreateRecruitList()
    {
        // create a unique recruit list
        List<Unit> recruitList = new List<Unit>();

        Team team = FindObjectOfType<Team>();
        List<Unit> closedList = team.units;

        for (int i = 0; recruitList.Count < 4; i++)
        {
            // need to check for dup's
            Unit tempUnit = friendlyList[Random.Range(0, friendlyList.Count)];
            if (!closedList.Contains(tempUnit) && !recruitList.Contains(tempUnit))
            {
                recruitList.Add(tempUnit);
            }
        }

        return recruitList;

    }
}
