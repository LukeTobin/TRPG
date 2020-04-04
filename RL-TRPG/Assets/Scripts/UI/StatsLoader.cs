using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsLoader : MonoBehaviour
{
    [SerializeField] GameObject UI_Element;
    GameObject[] unitArray;
    Sprite[] teamSprites;
    List<GameObject> playerUnitsUI = new List<GameObject>();

    GameObject[] statBoxArray;

    [SerializeField] public GameObject playerStatBox1;
    [SerializeField] public GameObject playerStatBox2;
    [SerializeField] public GameObject playerStatBox3;
    [SerializeField] public GameObject playerStatBox4;


    int playerCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] statBoxArray = { playerStatBox1, playerStatBox2, playerStatBox3, playerStatBox4 };

        //This code fills the UI elements at the left side of the screen for player stats
        unitArray = GameObject.FindGameObjectsWithTag("Unit");
        for (int i = 0; i < unitArray.Length; i++)
        {
            //If the units in the unit array are from the player team, then fill the info boxes to the left
            if (unitArray[i].transform.parent.name == "Team")
            {
                playerUnitsUI.Add(unitArray[i]);
                playerCount++;
                statBoxArray[playerCount - 1].transform.Find("Character").GetComponent<Image>().sprite =
                    unitArray[i].GetComponent<SpriteRenderer>().sprite;
                statBoxArray[playerCount - 1].transform.Find("HealthText").GetComponent<Text>().text =
                    unitArray[i].GetComponent<Unit>().health.ToString() + "/" +
                    unitArray[i].GetComponent<Unit>().maxHealth.ToString();
            }
        }

        //TODO: Refactor code to use for loop
        if (playerStatBox1.transform.Find("Character").GetComponent<Image>().sprite == null)
            playerStatBox1.SetActive(false);
        if (playerStatBox2.transform.Find("Character").GetComponent<Image>().sprite == null)
            playerStatBox2.SetActive(false);
        if (playerStatBox3.transform.Find("Character").GetComponent<Image>().sprite == null)
            playerStatBox3.SetActive(false);
        if (playerStatBox4.transform.Find("Character").GetComponent<Image>().sprite == null)
            playerStatBox4.SetActive(false);
    }

    //TODO: Refactor code to use a proper for loop
    public void UpdateStatBox()
    {
        if (playerStatBox1.transform.Find("Character").GetComponent<Image>().sprite != null)
        {
            playerStatBox1.transform.Find("HealthText").GetComponent<Text>().text =
                    playerUnitsUI[0].GetComponent<Unit>().health.ToString() + "/" +
                    playerUnitsUI[0].GetComponent<Unit>().maxHealth.ToString();

            if(playerUnitsUI[0].GetComponent<Unit>().health <= 0)
                playerStatBox1.transform.Find("HealthText").GetComponent<Text>().text = "DEAD";
        }
           
        if(playerStatBox2.transform.Find("Character").GetComponent<Image>().sprite != null)
        {
            playerStatBox2.transform.Find("HealthText").GetComponent<Text>().text =
                    playerUnitsUI[1].GetComponent<Unit>().health.ToString() + "/" +
                    playerUnitsUI[1].GetComponent<Unit>().maxHealth.ToString();

            if (playerUnitsUI[1].GetComponent<Unit>().health <= 0)
                playerStatBox2.transform.Find("HealthText").GetComponent<Text>().text = "DEAD";
        }

        if (playerStatBox3.transform.Find("Character").GetComponent<Image>().sprite != null)
        {
            playerStatBox3.transform.Find("HealthText").GetComponent<Text>().text =
                    playerUnitsUI[2].GetComponent<Unit>().health.ToString() + "/" +
                    playerUnitsUI[2].GetComponent<Unit>().maxHealth.ToString();

            if (playerUnitsUI[2].GetComponent<Unit>().health <= 0)
                playerStatBox3.transform.Find("HealthText").GetComponent<Text>().text = "DEAD";
        }

        if (playerStatBox4.transform.Find("Character").GetComponent<Image>().sprite != null)
        {
            playerStatBox4.transform.Find("HealthText").GetComponent<Text>().text =
                    playerUnitsUI[3].GetComponent<Unit>().health.ToString() + "/" +
                    playerUnitsUI[3].GetComponent<Unit>().maxHealth.ToString();

            if (playerUnitsUI[3].GetComponent<Unit>().health <= 0)
                playerStatBox4.transform.Find("HealthText").GetComponent<Text>().text = "DEAD";
        }

    }
}
