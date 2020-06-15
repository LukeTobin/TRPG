using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallGM : MonoBehaviour
{
    GameManager gm;
    [SerializeField] GameOver gg;
    [SerializeField] LevelUpBox _levelUpScreen;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        GainXP(gm.score, gm.state);
    }

    public void GainXP(int amount, bool lost)
    {
        int currentLevel = PlayerPrefs.GetInt("playerLevel");
        int xpall = PlayerPrefs.GetInt("xp") + amount;

        if(currentLevel < 3)
        {
            if(xpall >= 8000)
            {
                PlayerPrefs.SetInt("playerLevel", currentLevel++);

                int val = Mathf.Abs(xpall - 8000);
                PlayerPrefs.SetInt("xp", val);
                PlayerPrefs.Save();


                if (PlayerPrefs.GetInt("playerLevel") >= 3)
                    gg.InformationHandler(lost, amount, val, 32000, true);
                else
                    gg.InformationHandler(lost, amount, val, 8000, true);
            }
            else
            {
                PlayerPrefs.SetInt("xp", xpall);

                if (PlayerPrefs.GetInt("playerLevel") >= 3)
                    gg.InformationHandler(lost, amount, xpall, 32000, false);
                else
                    gg.InformationHandler(lost, amount, xpall, 8000, false);
            }
        }
        else if(currentLevel >= 3 && currentLevel < 7)
        {
            if (xpall >= 32000)
            {
                PlayerPrefs.SetInt("playerLevel", currentLevel++);

                int val = Mathf.Abs(xpall - 32000);
                PlayerPrefs.SetInt("xp", val);
                PlayerPrefs.Save();

                if (PlayerPrefs.GetInt("playerLevel") >= 7)
                    gg.InformationHandler(lost, amount, val, 40000, true);
                else
                    gg.InformationHandler(lost, amount, val, 32000, true);
            }
            else
            {
                PlayerPrefs.SetInt("xp", xpall);

                if (PlayerPrefs.GetInt("playerLevel") >= 7)
                    gg.InformationHandler(lost, amount, xpall, 40000, false);
                else
                    gg.InformationHandler(lost, amount, xpall, 32000, false);
            }
        }
        else if (currentLevel >= 7)
        {
            if (xpall >= 40000)
            {
                PlayerPrefs.SetInt("playerLevel", currentLevel++);

                int val = Mathf.Abs(xpall - 40000);
                PlayerPrefs.SetInt("xp", val);
                PlayerPrefs.Save();

                gg.InformationHandler(lost, amount, val, 40000, true);

            }
            else
            {
                PlayerPrefs.SetInt("xp", xpall);

                gg.InformationHandler(lost, amount, xpall, 40000, false);
            }
        }
        
    }

    public void LevelUp(bool playerLose)
    {
        _levelUpScreen.gameObject.SetActive(true);

        List<Artifact> lockedArtifacts = new List<Artifact>();
        List<Unit> lockedUnits = new List<Unit>();

        for (int i = 0; i < gm.friendlyList.Count; i++)
        {
            if (PlayerPrefs.GetInt(gm.friendlyList[i].title + ".unlocked") == 0)
            {
                lockedUnits.Add(gm.friendlyList[i]);
            }
        }

        for (int j = 0; j < gm.artifactList.Count; j++)
        {
            if (PlayerPrefs.GetInt(gm.artifactList[j].artifactName + ".unlocked") == 0)
            {
                lockedArtifacts.Add(gm.artifactList[j]);
            }
        }

        if (lockedArtifacts.Count + lockedUnits.Count > 0)
        {
            bool a = false; bool b = false;
            if (lockedArtifacts.Count > 0)
                a = true;

            if (lockedUnits.Count > 0)
                b = true;

            if (a && b)
            {
                int x = Random.Range(0, 2);
                if (x == 0)
                {
                    Artifact temp = lockedArtifacts[Random.Range(0, lockedArtifacts.Count)];

                    _levelUpScreen.GetComponent<LevelUpBox>().truFalse = playerLose;
                    _levelUpScreen.GetComponent<LevelUpBox>().levelText.text = $"<color=#ffadad>{PlayerPrefs.GetInt("playerLevel") - 1}</color> <color=#fffffc>-></color> <color=#caffbf>{PlayerPrefs.GetInt("playerLevel")}</color>";
                    _levelUpScreen.GetComponent<LevelUpBox>().unlock.sprite = temp.artifactVisual;

                    PlayerPrefs.SetInt(temp.artifactName + ".unlocked", 1);
                    PlayerPrefs.Save();

                    //gm.ReturnToMainMenu(playerLose);
                }
                else
                {
                    Unit temp = lockedUnits[Random.Range(0, lockedUnits.Count)];

                    _levelUpScreen.GetComponent<LevelUpBox>().truFalse = playerLose;
                    _levelUpScreen.GetComponent<LevelUpBox>().levelText.text = $"<color=#ffadad>{PlayerPrefs.GetInt("playerLevel") - 1}</color> <color=#fffffc>-></color> <color=#caffbf>{PlayerPrefs.GetInt("playerLevel")}</color>";
                    _levelUpScreen.GetComponent<LevelUpBox>().unlock.sprite = temp.profile;

                    PlayerPrefs.SetInt(temp.title + ".unlocked", 1);
                    PlayerPrefs.Save();

                    //gm.ReturnToMainMenu(playerLose);
                }
            }
            else if (a && !b)
            {
                Artifact temp = lockedArtifacts[Random.Range(0, lockedArtifacts.Count)];

                _levelUpScreen.GetComponent<LevelUpBox>().truFalse = playerLose;
                _levelUpScreen.GetComponent<LevelUpBox>().levelText.text = $"<color=#ffadad>{PlayerPrefs.GetInt("playerLevel") - 1}</color> <color=#fffffc>-></color> <color=#caffbf>{PlayerPrefs.GetInt("playerLevel")}</color>";
                _levelUpScreen.GetComponent<LevelUpBox>().unlock.sprite = temp.artifactVisual;

                PlayerPrefs.SetInt(temp.artifactName + ".unlocked", 1);
                PlayerPrefs.Save();

                //gm.ReturnToMainMenu(playerLose);
            }
            else if (!a && b)
            {
                Unit temp = lockedUnits[Random.Range(0, lockedUnits.Count)];

                _levelUpScreen.GetComponent<LevelUpBox>().truFalse = playerLose;
                _levelUpScreen.GetComponent<LevelUpBox>().levelText.text = $"<color=#ffadad>{PlayerPrefs.GetInt("playerLevel") - 1}</color> <color=#fffffc>-></color> <color=#caffbf>{PlayerPrefs.GetInt("playerLevel")}</color>";
                _levelUpScreen.GetComponent<LevelUpBox>().unlock.sprite = temp.profile;

                PlayerPrefs.SetInt(temp.title + ".unlocked", 1);
                PlayerPrefs.Save();

                //gm.ReturnToMainMenu(playerLose);
            }
            else
            {
                gm.ReturnToMainMenu(playerLose);
            }
        }
        else
        {
            print("none left to unlock");
            gm.ReturnToMainMenu(playerLose);
        }
    }
}
