using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmAbility : MonoBehaviour
{
    public AbilitySelection selection;

    public void Clicked()
    {
        selection.SetActiveAbility();
    }

    public void OpenMenu()
    {
        selection.OpenMenu();
    }
}
