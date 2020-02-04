using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    GameController gc;

    [Header("UI Elements")]
    public Button endTurnBtn;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        endTurnBtn.onClick.AddListener(gc.EndTurn);
    }

    
}
