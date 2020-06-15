using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Highscores : MonoBehaviour
{
    public GameManager gm;
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI wins;
    public TextMeshProUGUI mostWins;

    public void ShowHighscores()
    {
        gameObject.SetActive(true);
        bestScore.text = "" + PlayerPrefs.GetInt("highscore");
        wins.text = PlayerPrefs.GetInt("wins").ToString();

        int high = 0;
        Unit stored = new Unit();
        for (int i = 0; i < gm.friendlyList.Count; i++)
        {
            int val = PlayerPrefs.GetInt(gm.friendlyList[i] + ".wins");
            if (val > 0)
            {
                if(val > high)
                {
                    high = val;
                    stored = gm.friendlyList[i];
                }
            }
        }

        if (stored != null)
            mostWins.text = stored.title;
        else
            mostWins.text = "no wins";
    }
}
