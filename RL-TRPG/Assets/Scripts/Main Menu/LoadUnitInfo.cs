using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadUnitInfo : MonoBehaviour
{

    UnitSelection unitSelection;
    RunHandler handler;

    // store info
    public Image coverSprite = null;
    public Unit unit;

    private void Start()
    {
        unitSelection = FindObjectOfType<UnitSelection>();
        handler = FindObjectOfType<RunHandler>();

        if(unit == handler.curHero)
        {
            unitSelection.CreateDisplay(unit, this.GetComponent<Button>());
        }
    }

    public void ShowInfo()
    {
        unitSelection.CreateDisplay(unit, this.GetComponent<Button>());
    }
}
