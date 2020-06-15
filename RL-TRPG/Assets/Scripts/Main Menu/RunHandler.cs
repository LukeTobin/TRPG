using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RunHandler : MonoBehaviour
{

    public Button run;
    public Button highscore;
    public Button continueButton;
    public Button settingsButton;
    [Space]
    public GameObject fog;
    public GameObject settings;
    [Space]
    public GameObject UnitSelectionScreen;
    [Space]
    public List<Unit> heroes;
    public Team team;

    [Header("Unit")]
    public Unit curHero;
    public Image heroSprite;
    public TextMeshProUGUI unitName;
    public Button origin;
    public Button _class;
    public Button ability;

    GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        run.onClick.AddListener(NormalRun);
        highscore.onClick.AddListener(Highscore);
        continueButton.onClick.AddListener(ContinueRun);
        settingsButton.onClick.AddListener(ShowSettings);

        UnitSelectionScreen.SetActive(false);
        fog.SetActive(false);
        settings.SetActive(false);

        if (PlayerPrefs.GetInt("active") != 1)
            continueButton.gameObject.SetActive(false);

        GetUnitsUnlocked();

        curHero = heroes[0];
        heroSprite.sprite = curHero.profile;
        unitName.text = curHero.title;
        origin.GetComponent<Image>().sprite = gm.GetOriginImage(curHero.GetComponent<Traits>()._origin);
        _class.GetComponent<Image>().sprite = gm.GetClassImage(curHero.GetComponent<Traits>()._class);
        //ability.GetComponent<Image>().sprite = curHero.activeAbility.sprite;
    }

    #region Main Buttons
    void NormalRun()
    {
        gm.NewPrefSet();

        team.leader = curHero;
        team.leader.health = team.leader.maxHealth + 20;
        team.leader.attackDamage = team.leader.maxAttackDamage;
        team.leader.magicDamage = team.leader.maxMagicDamage;
        team.leader.armor = team.leader.maxArmor;
        team.leader.resist = team.leader.maxResist;
        team.leader.speed = team.leader.maxSpeed;

        int plays = PlayerPrefs.GetInt(team.leader.title + ".runs") + 1;
        PlayerPrefs.SetInt(team.leader.title + ".runs", plays);
        PlayerPrefs.SetInt(team.leader.title + ".health", team.leader.health);
        PlayerPrefs.SetInt(team.leader.title + ".leader", 1);
        PlayerPrefs.SetInt("active", 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Map");
    }

    void Highscore()
    {
        // filled when we have a scoring system. Use PlayerPreff
    }

    void ContinueRun()
    {
        gm.LoadSavedTeam();

        if(PlayerPrefs.GetInt("active") == 1)
        {
            SceneManager.LoadScene("Map");
        }
    }

    void ShowSettings()
    {
        fog.SetActive(true);
        settings.SetActive(true);
    }

    #endregion


    public void GetUnitsUnlocked()
    {
        // heroes.Clear();

        for (int i = 0; i < gm.friendlyList.Count; i++)
        {
            if(PlayerPrefs.GetInt(gm.friendlyList[i].title + ".unlocked") == 1)
            {
                heroes.Add(gm.friendlyList[i]);
            }
        }
    }
}
