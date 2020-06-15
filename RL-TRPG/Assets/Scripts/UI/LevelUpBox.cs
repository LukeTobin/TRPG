using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpBox : MonoBehaviour
{
    public TextMeshProUGUI levelText = null;
    [SerializeField] Button exitBtn = null;
    public Image unlock = null;
    public bool truFalse = false;

    // Start is called before the first frame update
    void Awake()
    {
        exitBtn.onClick.AddListener(delegate { LeaveRoom(truFalse); });
        levelText.text = "";

        gameObject.SetActive(false);
    }

    void LeaveRoom(bool tF)
    {
        GameManager gm = FindObjectOfType<GameManager>();
        gm.ReturnToMainMenu(tF);
    }
}
