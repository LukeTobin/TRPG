using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject controller = null;
    [Space]
    [SerializeField] Button continueBtn = null;
    [SerializeField] TextMeshProUGUI title = null;
    [SerializeField] TextMeshProUGUI scoreT = null;
    [SerializeField] TextMeshProUGUI highScore = null;
    [SerializeField] TextMeshProUGUI playerLevel = null;
    public Color win;
    public Color lose;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void InformationHandler(bool playerLose, int score, int xp, int xpLeft, bool levelUp = false)
    {
        gameObject.SetActive(true);

        if (playerLose)
        {
            title.text = $"<color=#f25f5c>YOU LOSE</color>";
            continueBtn.GetComponent<Image>().color = lose;
        }
        else
        {
            title.text = $"<color=#70c1b3>YOU LOSE</color>";
            continueBtn.GetComponent<Image>().color = win;
        }

        if (score > PlayerPrefs.GetInt("highscore"))
        {
            scoreT.text = "New Best!";
            highScore.text = score.ToString();
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.Save();
        }
        else
        {
            scoreT.text = score.ToString();
            highScore.text = PlayerPrefs.GetInt("highscore").ToString();
        }

        if (levelUp)
        {
            playerLevel.text = "<color=#fee440>LEVEL UP!</color>";
            continueBtn.onClick.AddListener(delegate { ReturnToMain(true, playerLose); });
        }
        else
        {
            playerLevel.text = $"<color=#a0c4ff>{xp}</color> <color=#f7fff7>/</color> <color=#ffc6ff>{xpLeft}</color> <";
            continueBtn.onClick.AddListener(delegate { ReturnToMain(false, playerLose); });
        }
    }

    void ReturnToMain(bool lvlUp, bool playerLose)
    {
        GameManager gm = FindObjectOfType<GameManager>();

        if (lvlUp)
        {
            controller.GetComponent<CallGM>().LevelUp(playerLose);
        }
        else
        {
            gm.ReturnToMainMenu(playerLose);
        }
    }
}
