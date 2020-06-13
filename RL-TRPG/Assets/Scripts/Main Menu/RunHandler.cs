﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RunHandler : MonoBehaviour
{

    public Button run;
    public Button highscore;
    public Button continueButton;
    [Space]
    public Button left;
    public Button right;
    [Space]
    public Image heroSprite;
    [Space]
    public List<Unit> heroes;
    [Space]
    public Team team;
    public Unit curHero;
    int count = 0;

    GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        run.onClick.AddListener(NormalRun);
        highscore.onClick.AddListener(Highscore);
        continueButton.onClick.AddListener(ContinueRun);

        left.onClick.AddListener(LeftHero);
        right.onClick.AddListener(RightHero);

        curHero = heroes[count];
    }

    void NormalRun()
    {
        team.leader = curHero;
        team.leader.health = team.leader.maxHealth + 20;

        gm.NewPrefSet();

        //PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Map");
    }

    void Highscore()
    {
        // filled when we have a scoring system. Use PlayerPreff
    }

    void ContinueRun()
    {
        /*
         * save team in playerprefs
         */
        if(PlayerPrefs.GetInt("continued") == 1)
        {
            SceneManager.LoadScene("Map");
        }
    }

    void LeftHero()
    {
        count--;
        if(count < 0)
        {
            count = heroes.Count-1;
        }

        curHero = heroes[count];
        heroSprite.sprite = heroes[count].profile;
    }

    void RightHero()
    {
        count++;
        if (count >= heroes.Count)
        {
            count = 0;
        }

        curHero = heroes[count];
        heroSprite.sprite = heroes[count].profile;
    }
}
