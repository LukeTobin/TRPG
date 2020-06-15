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
        //
    }

    public void ActiveAll()
    {
        newRun.SetActive(true);
        highscore.SetActive(true);
        settings.SetActive(true);
    }
}
