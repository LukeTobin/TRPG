using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /*
     * Basic UI manager - TODO
     */
    GameController gc;
    Team team;

    [Header("UI Elements")]
    public Button endTurnBtn;
    public Button infoButton;
    public Button useAbility;
    public Text turnText;

    [Header("Information Panel")]
    public GameObject infoPanel;
    [Space]
    public Image charHolder;
    public Text charTitle;
    public Text t_ad;
    public Text t_md;
    public Text t_ar;
    public Text t_mr;
    [Space]
    public Transform bar;
    public Image[] mana;
    public Sprite manaFull;
    public Sprite manaEmpty;
    [Space]
    public Text abilityTitle;
    public Text abilityDescription;
    public Text abilityCost;

    [Header("Recruit Menu")]
    public GameObject recruitPanel;
    [Space]
    public Button recruitBtn1;
    public Button recruitBtn2;
    public Button recruitBtn3;
    [Space]
    public Button recruit1;
    public Button recruit2;
    public Button recruit3;
    [Space]
    public Button skip;
    [Space]
    public List<Unit> recruits = new List<Unit>();

    bool infoVisible;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        team = FindObjectOfType<Team>();
        endTurnBtn.onClick.AddListener(gc.EndTurn);
        infoButton.onClick.AddListener(delegate { ShowInfo(gc.selected, gc.selectedUnit); });

        recruitPanel.SetActive(false);
        infoPanel.SetActive(false);

        skip.onClick.AddListener(SkipOffer);

        turnText.text = "Player Turn: " + gc.playerTurn;
    }

    public void UpdateTurn()
    {
        turnText.text = "Player Turn: " + gc.playerTurn;
    }

    public void OfferRecruit(List<Unit> offers) // pass units
    {
        recruitPanel.SetActive(true);
        
        recruit1.GetComponent<Image>().sprite = offers[0].profile;
        recruit2.GetComponent<Image>().sprite = offers[1].profile;
        recruit3.GetComponent<Image>().sprite = offers[2].profile;

        recruits.Add(offers[0]);
        recruits.Add(offers[1]);
        recruits.Add(offers[2]);

       // recruitBtn1.onClick.AddListener(delegate { AddUnit(recruits[0]); });
       // recruitBtn2.onClick.AddListener(delegate { AddUnit(recruits[1]); });
       // recruitBtn3.onClick.AddListener(delegate { AddUnit(recruits[2]); });
    }

    public void RemoveOffers()
    {
        recruitPanel.SetActive(false);

        recruits.Clear();
    }

    public void AddUnit(Unit unit)
    {
        team.AddNewUnit(unit);
        RemoveOffers();

        gc.EndGame(); // smooth transition here
    }

    void SkipOffer()
    {
        RemoveOffers();
        gc.EndGame();
    }
    
    void ShowInfo(bool selected, Unit unit = null)
    {
        if (selected && !infoVisible)
        {
            infoPanel.SetActive(true);

            // identity
            charHolder.sprite = unit.profile;
            charTitle.text = unit.title;
            t_ad.text = unit.attackDamage.ToString();
            t_md.text = unit.magicDamage.ToString();
            t_ar.text = unit.armor.ToString();
            t_mr.text = unit.resist.ToString();

            // health / mana
            float OldRange = 0f - unit.maxHealth; // get your old range
            float NewRange = 0f - 1f; // new range 0f - 1f (0-100%)
            float NewValue = (((unit.health - 0f) * NewRange) / OldRange) + 0f; // creates a new value to reprsent our current health in terms of (0%-100%)
            bar.localScale = new Vector3(NewValue, 1f);

            switch (unit.mana)
            {
                case 1:
                    mana[0].sprite = manaFull;
                    mana[1].sprite = manaEmpty;
                    mana[2].sprite = manaEmpty;
                    mana[3].sprite = manaEmpty;
                    mana[4].sprite = manaEmpty;
                    break;
                case 2:
                    mana[0].sprite = manaFull;
                    mana[1].sprite = manaFull;
                    mana[2].sprite = manaEmpty;
                    mana[3].sprite = manaEmpty;
                    mana[4].sprite = manaEmpty;
                    break;
                case 3:
                    mana[0].sprite = manaFull;
                    mana[1].sprite = manaFull;
                    mana[2].sprite = manaFull;
                    mana[3].sprite = manaEmpty;
                    mana[4].sprite = manaEmpty;
                    break;
                case 4:
                    mana[0].sprite = manaFull;
                    mana[1].sprite = manaFull;
                    mana[2].sprite = manaFull;
                    mana[3].sprite = manaFull;
                    mana[4].sprite = manaEmpty;
                    break;
                case 5:
                    mana[0].sprite = manaFull;
                    mana[1].sprite = manaFull;
                    mana[2].sprite = manaFull;
                    mana[3].sprite = manaFull;
                    mana[4].sprite = manaFull;
                    break;
                default:
                    mana[0].sprite = manaEmpty;
                    mana[1].sprite = manaEmpty;
                    mana[2].sprite = manaEmpty;
                    mana[3].sprite = manaEmpty;
                    mana[4].sprite = manaEmpty;
                    break;
            }

            // ability
            abilityTitle.text = unit.activeAbility.GetComponent<Ability>().title;
            abilityDescription.text = unit.activeAbility.GetComponent<Ability>().description;
            abilityCost.text = "Cost: " + unit.activeAbility.GetComponent<Ability>().cost;

            infoVisible = true;
        }
        else if(infoVisible)
        {
            infoPanel.SetActive(false);
            infoVisible = false;
        }
    }
}
