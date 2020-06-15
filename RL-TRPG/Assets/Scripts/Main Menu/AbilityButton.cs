using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    public AbilitySelection selection;
    public int i = 0;
    public Image placeholder;

    public void SelectAbility()
    {
        selection.ArtifactSelection(i, placeholder, this.GetComponent<Button>());
    }
    
}
