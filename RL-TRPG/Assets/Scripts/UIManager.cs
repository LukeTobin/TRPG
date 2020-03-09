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

    [Header("UI Elements")]
    public Button endTurnBtn;
    public Text turnText;

    [Header("Recruit Menu")]
    public GameObject recruitPanel;
    public Button recruitSlot1;
    public Button recruitSlot2;
    public Button recruitSlot3;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        endTurnBtn.onClick.AddListener(gc.EndTurn);

        recruitPanel.SetActive(false);

        turnText.text = "Player Turn: " + gc.playerTurn;
    }

    public void UpdateTurn()
    {
        turnText.text = "Player Turn: " + gc.playerTurn;
    }

    public void OfferRecruit() // pass units
    {
        recruitPanel.SetActive(true);
    }

    public void RemoveOffers()
    {
        recruitPanel.SetActive(false);
    }
    
}
