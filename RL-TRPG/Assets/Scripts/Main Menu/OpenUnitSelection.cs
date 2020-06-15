using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUnitSelection : MonoBehaviour
{
    public GameObject reference;
    public void OpenScreen()
    {
        reference.SetActive(true);
        reference.GetComponent<UnitSelection>().CreateMenu();
    }

    public void ConfirmSelection()
    {
        reference.GetComponent<UnitSelection>().Confirm();
    }
}
