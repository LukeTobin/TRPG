using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitySelection : MonoBehaviour
{
    [SerializeField] RunHandler handler = null;
    [Space]
    [SerializeField] TextMeshProUGUI abilityName = null;
    [SerializeField] TextMeshProUGUI abilityDesc = null;
    [Space]
    public Image img1;
    public Image img2;
    public Image img3;
    public Button bg1;
    public Button bg2;
    public Button bg3;
    public Unit unit;
    public int selectedAbility = 0;
    Button selectedButton;
    public Color selectedColor;
    public Color defaultColor;

    public void OpenMenu()
    {
        gameObject.SetActive(true);
        unit = handler.curHero;
        img1.sprite = unit.allAbilites[0].sprite;
        img2.sprite = unit.allAbilites[1].sprite;
        img3.sprite = unit.allAbilites[2].sprite;
        abilityName.text = unit.allAbilites[0].title;
        abilityDesc.text = unit.allAbilites[0].description;
        bg1.GetComponent<Image>().color = selectedColor;
        bg2.GetComponent<Image>().color = defaultColor;
        bg3.GetComponent<Image>().color = defaultColor;
        selectedAbility = 0;
        selectedButton = bg1;
    }

    public void ArtifactSelection(int which, Image front, Button btn)
    {
        unit = handler.curHero;
        selectedAbility = which;

        if(selectedButton != null)
        {
            selectedButton.GetComponent<Image>().color = defaultColor;
        }

        selectedButton = btn;
        btn.GetComponent<Image>().color = selectedColor;

        abilityName.text = unit.allAbilites[selectedAbility].title;
        abilityDesc.text = unit.allAbilites[selectedAbility].description;

        front.sprite = unit.allAbilites[selectedAbility].sprite;
    }

    public void SetActiveAbility()
    {
        unit.activeAbility = unit.allAbilites[selectedAbility];
        handler.DisplayNewUnit(unit);
        gameObject.SetActive(false);
    }
}
