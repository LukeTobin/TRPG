using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatsLoader : MonoBehaviour
{
    [SerializeField] GameObject player1StatBox, player2StatBox, player3StatBox, player4StatBox;
    [SerializeField] public GameObject[] units;
    GameObject playerTeam;

    // Start is called before the first frame update
    void Start()
    {
        /*units = GameObject.FindGameObjectsWithTag("Unit");

        for (int i = 0; i < units.Length; i++)
        {
            int playerNumber = 0;

            if (units[i].transform.parent.name == "Team")
            {
                playerNumber++;
                Debug.Log(units[i].name + "... Player " + playerNumber);
            }
            else
            {
                Debug.Log(units[i].name + "... enemy");
            }
        }*/


        playerTeam = GameObject.FindGameObjectWithTag("PlayerTeam");


        for (int i = 0; i < units.Length; i++)
        {
            Debug.Log("Player " + (i + 1) + " " + name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
