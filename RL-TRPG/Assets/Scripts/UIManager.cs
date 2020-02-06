using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    GameController gc;

    [Header("UI Elements")]
    public Button endTurnBtn;
    public Text turnText;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        endTurnBtn.onClick.AddListener(gc.EndTurn);

        turnText.text = "Player Turn: " + gc.playerTurn;
    }

    public void UpdateTurn()
    {
        turnText.text = "Player Turn: " + gc.playerTurn;
    }
    
}
