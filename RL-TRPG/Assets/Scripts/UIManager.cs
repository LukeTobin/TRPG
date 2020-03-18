using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /*
     * Basic UI manager - TODO
     */
    GameController gc;
    Team team;

    [Header("UI Elements")]
    public Button endTurnBtn;
    public Text turnText;

    [Header("Recruit Menu")]
    public GameObject recruitPanel;
    [Space]
    public Button recruitBtn1;
    public Button recruitBtn2;
    public Button recruitBtn3;
    [Space]
    public Button recruit1;
    public Button recruit2;
    public Button recruit3;
    [Space]
    public List<Unit> recruits = new List<Unit>();

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        team = FindObjectOfType<Team>();
        endTurnBtn.onClick.AddListener(gc.EndTurn);

        recruitPanel.SetActive(false);

        turnText.text = "Player Turn: " + gc.playerTurn;
    }

    public void UpdateTurn()
    {
        turnText.text = "Player Turn: " + gc.playerTurn;
    }

    public void OfferRecruit(List<Unit> offers) // pass units
    {
        recruitPanel.SetActive(true);
        
        recruit1.GetComponent<Image>().sprite = offers[0].profile;
        recruit2.GetComponent<Image>().sprite = offers[1].profile;
        recruit3.GetComponent<Image>().sprite = offers[2].profile;

        recruits.Add(offers[0]);
        recruits.Add(offers[1]);
        recruits.Add(offers[2]);

       // recruitBtn1.onClick.AddListener(delegate { AddUnit(recruits[0]); });
       // recruitBtn2.onClick.AddListener(delegate { AddUnit(recruits[1]); });
       // recruitBtn3.onClick.AddListener(delegate { AddUnit(recruits[2]); });
    }

    public void RemoveOffers()
    {
        recruitPanel.SetActive(false);

        recruits.Clear();
    }

    public void AddUnit(Unit unit)
    {
        team.AddNewUnit(unit);
        RemoveOffers();

        gc.EndGame(); // smooth transition here
    }
    
}
