using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadUnitInfo : MonoBehaviour
{

    UnitSelection unitSelection;

    // store info
    public Image coverSprite = null;
    public Unit unit;

    private void Start()
    {
        unitSelection = FindObjectOfType<UnitSelection>();
    }

    public void ShowInfo()
    {
        unitSelection.CreateDisplay(unit, this.GetComponent<Button>());
    }
}
