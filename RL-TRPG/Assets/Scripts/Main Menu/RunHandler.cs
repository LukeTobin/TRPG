using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RunHandler : MonoBehaviour
{

    public Button run;
    public Button highscore;
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

    void Start()
    {
        run.onClick.AddListener(NormalRun);
        highscore.onClick.AddListener(Highscore);

        left.onClick.AddListener(LeftHero);
        right.onClick.AddListener(RightHero);

        curHero = heroes[count];
    }

    void NormalRun()
    {
        team.leader = curHero;
        team.leader.health = team.leader.maxHealth + 20;

        //PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Map");
    }

    void Highscore()
    {
        // filled when we have a scoring system. Use PlayerPreff
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
