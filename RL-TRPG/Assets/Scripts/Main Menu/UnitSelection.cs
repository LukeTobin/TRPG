using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitSelection : MonoBehaviour
{
    [SerializeField] RunHandler handler;
    [SerializeField] Button creatableButton;
    [SerializeField] GameObject storage;
    [Space]
    [SerializeField] Unit displayUnit = null;
    [SerializeField] Button storedButton = null;
    [Space]
    public Color defaultColor;
    public Color selectedColor;

    void Start()
    {
        //handler = FindObjectOfType<RunHandler>();
    }

    public void CreateDisplay(Unit unit, Button btn)
    {
        displayUnit = unit;

        if (storedButton != null)
            storedButton.GetComponent<Image>().color = defaultColor;

        storedButton = btn;
        btn.GetComponent<Image>().color = selectedColor;
    }

    public void CreateMenu()
    {
        //handler.GetUnitsUnlocked();

        for (int i = 0; i < handler.heroes.Count; i++)
        {
            Button btn = Instantiate(creatableButton);
            btn.transform.SetParent(storage.transform, false);
            btn.GetComponent<LoadUnitInfo>().unit = handler.heroes[i];
            btn.GetComponent<LoadUnitInfo>().coverSprite.sprite = handler.heroes[i].profile;
        }
    }
}
