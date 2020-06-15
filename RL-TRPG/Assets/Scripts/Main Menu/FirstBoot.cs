using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FirstBoot : MonoBehaviour
{
    [SerializeField] GameObject welcomeMessage = null;
    [SerializeField] GameObject welcomeMessage2 = null;
    [Space]
    [SerializeField] GameObject continueBtn = null;
    [SerializeField] GameObject newRun = null;
    [SerializeField] GameObject highscore = null;
    [SerializeField] GameObject settings = null;

    [Header("Exclusives")]
    public bool affiliateArtifact = false;

    void Awake()
    {
        if (PlayerPrefs.GetInt("firstBoot") != 1)
        {
            welcomeMessage.SetActive(true);
            welcomeMessage2.SetActive(false);
            GiveNewUnits();

            PlayerPrefs.SetInt("playerLevel", 1);
            PlayerPrefs.Save();

            continueBtn.SetActive(false);
            newRun.SetActive(false);
            highscore.SetActive(false);
            settings.SetActive(false);
        }
        else
        {
            welcomeMessage.SetActive(false);
            welcomeMessage2.SetActive(false);
        }
    }

    void GiveNewUnits()
    {
        // starting units
        PlayerPrefs.SetInt("Irene.unlocked", 1);
        PlayerPrefs.SetInt("Mara.unlocked", 1);
        PlayerPrefs.SetInt("Sirros.unlocked", 1);
        PlayerPrefs.SetInt("Fauna.unlocked", 1);

        // starting artifacts
        PlayerPrefs.SetInt("Ruined Map.unlocked", 1);
        PlayerPrefs.SetInt("Chaos Orb.unlocked", 1);
        PlayerPrefs.SetInt("Clarity Orb.unlocked", 1);
        PlayerPrefs.SetInt("Flourish Coin.unlocked", 1);
        PlayerPrefs.SetInt("Heavy Weight.unlocked", 1);
        PlayerPrefs.SetInt("Heroes Gloves.unlocked", 1);
        PlayerPrefs.SetInt("Heroes Mask.unlocked", 1);
        PlayerPrefs.SetInt("Heroes Spear.unlocked", 1);
        PlayerPrefs.SetInt("Heroes Stoneplate.unlocked", 1);
        PlayerPrefs.SetInt("Lucky Clover.unlocked", 1);
        PlayerPrefs.SetInt("Rage Orb.unlocked", 1);
        PlayerPrefs.SetInt("Skar Totem.unlocked", 1);
        PlayerPrefs.SetInt("Trash.unlocked", 1);
        PlayerPrefs.SetInt("Treasure Chest.unlocked", 1);
        PlayerPrefs.SetInt("Vampiric Sceptre", 1);

        PlayerPrefs.Save();
    }

    public void ActiveAll()
    {
        newRun.SetActive(true);
        highscore.SetActive(true);
        settings.SetActive(true);
    }
}
