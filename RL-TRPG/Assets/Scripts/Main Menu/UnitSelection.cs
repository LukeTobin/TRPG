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
    [Header("Info To Be Filled")]
    [SerializeField] TextMeshProUGUI titleT = null;
    [SerializeField] TextMeshProUGUI damageTypeT = null;
    [SerializeField] TextMeshProUGUI _originT = null;
    [SerializeField] TextMeshProUGUI _classT = null;
    [SerializeField] TextMeshProUGUI adT = null;
    [SerializeField] TextMeshProUGUI mdT = null;
    [SerializeField] TextMeshProUGUI arT = null;
    [SerializeField] TextMeshProUGUI mrT = null;
    [SerializeField] TextMeshProUGUI hpT = null;

    [SerializeField] Image damageTypeI = null;
    [SerializeField] Image _originI = null;
    [SerializeField] Image _classI = null;

    GameManager gm;
    List<Button> buttonList = new List<Button>();

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        displayUnit = handler.curHero;
        CreateDisplay(handler.curHero, null);
    }

    public void CreateDisplay(Unit unit, Button btn)
    {
        if (displayUnit == unit)
            Confirm();
        else
        {
            displayUnit = unit;

            if (storedButton != null)
                storedButton.GetComponent<Image>().color = defaultColor;

            if(btn != null)
            {
                storedButton = btn;
                btn.GetComponent<Image>().color = selectedColor;
            }
               
            titleT.text = unit.title;
            damageTypeT.text = unit.damageType.ToString();
            _originT.text = unit.GetComponent<Traits>()._origin.ToString();
            _classT.text = unit.GetComponent<Traits>()._class.ToString();
            adT.text = "AD: " + unit.maxAttackDamage.ToString();
            mdT.text = "MD: " + unit.maxMagicDamage.ToString();
            arT.text = "AR: " + unit.maxArmor.ToString();
            mrT.text = "MR: " + unit.maxResist.ToString();
            hpT.text = "HP: " + unit.maxHealth.ToString();

            damageTypeI.sprite = gm.GetDamageTypeSprite(unit.damageType);
            _originI.sprite = gm.GetOriginImage(unit.GetComponent<Traits>()._origin);
            _classI.sprite = gm.GetClassImage(unit.GetComponent<Traits>()._class);
        }    
    }

    public void CreateMenu(bool createNew = false)
    {
        if(buttonList.Count <= 0)
        {
            handler.GetUnitsUnlocked();

            for (int i = 0; i < handler.heroes.Count; i++)
            {
                Button btn = Instantiate(creatableButton);
                btn.transform.SetParent(storage.transform, false);
                btn.GetComponent<LoadUnitInfo>().unit = handler.heroes[i];
                btn.GetComponent<LoadUnitInfo>().coverSprite.sprite = handler.heroes[i].profile;

                buttonList.Add(btn);
            }
        }
        else if (createNew)
        {
            buttonList.Clear();

            handler.GetUnitsUnlocked();

            for (int i = 0; i < handler.heroes.Count; i++)
            {
                Button btn = Instantiate(creatableButton);
                btn.transform.SetParent(storage.transform, false);
                btn.GetComponent<LoadUnitInfo>().unit = handler.heroes[i];
                btn.GetComponent<LoadUnitInfo>().coverSprite.sprite = handler.heroes[i].profile;

                buttonList.Add(btn);
            }
        }
    }

    public void Confirm()
    {
        if (displayUnit != null)
            handler.DisplayNewUnit(displayUnit);

        gameObject.SetActive(false);
    }
}
