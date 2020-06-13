using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    /*
     * Basic UI manager - TODO
     */
    GameController gc;
    GameManager gm;
    Team team;

    [Header("Basic UI Elements")]
    public Button endTurnBtn;
    public Button infoButton;
    public Button useAbility;
    public TextMeshProUGUI turnText;
    public TextMeshProUGUI roundText;

    [Header("Information Panel")]
    public GameObject infoPanel;
    public Image charHolder;
    public Text charTitle;
    public Text t_ad;
    public Text t_md;
    public Text t_ar;
    public Text t_mr;
    public Transform bar;
    public Image[] mana;
    public Sprite manaFull;
    public Sprite manaEmpty;
    public Text abilityTitle;
    public Text abilityDescription;
    public Text abilityCost;

    [Header("Recruit Menu")]
    public GameObject recruitPanel;
    public Button recruitBtn1;
    public Button recruitBtn2;
    public Button recruitBtn3;
    public Button recruit1;
    public Button recruit2;
    public Button recruit3;
    public Button skip;
    public List<Unit> recruits = new List<Unit>();

    [Header("Reward Screen")]
    public GameObject rewardScreen;
    public GameObject block2;
    public GameObject block3;
    public TextMeshProUGUI block1GoldText;
    public Button continueButton;

    [Header("Artifacts")]
    [SerializeField] Button artifactButton;
    [SerializeField] GameObject artifactContainer;
    public GameObject infoboxEnabled;

    bool infoVisible;
    int goldBonus = 0;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        gm = FindObjectOfType<GameManager>();
        team = FindObjectOfType<Team>();
        endTurnBtn.onClick.AddListener(CheckEndTurn);
        infoButton.onClick.AddListener(delegate { ShowInfo(gc.selected, gc.selectedUnit); });
        useAbility.onClick.AddListener(delegate { Ability(gc.selectedUnit); });
        continueButton.onClick.AddListener(GiveRewards);

        recruitPanel.SetActive(false);
        infoPanel.SetActive(false);
        rewardScreen.SetActive(false);

        skip.onClick.AddListener(SkipOffer);

        if(gc.playerTurn == 1)
        {
            turnText.text = "Player Turn: <color=#2a9d8f>Yours</color>";
        }
        else
        {
            turnText.text = "Player Turn: <color=#e76f51>Enemy</color>";
        }

        roundText.text = "Round: " + gc.round;

        ShowArtifacts();
        
    }

    public void UpdateTurn()
    {
        if (gc.playerTurn == 1)
        {
            turnText.text = "Player Turn: <color=#2a9d8f>Yours</color>";
            roundText.text = "Round: " + gc.round;
        }
        else
        {
            turnText.text = "Player Turn: <color=#e76f51>Enemy</color>";
            roundText.text = "Round: " + gc.round;
        }
    }

    void CheckEndTurn()
    {
        if (gc.playerTurn == 1)
            gc.EndTurn();
    }

    public void RewardScreen(int goldAmount, bool recruit = false, bool relic = false)
    {
        rewardScreen.SetActive(true);
        goldBonus = goldAmount;
        block1GoldText.text = $"Gold - { goldAmount }";
        if (relic)
            block3.SetActive(true);
        else
            block3.SetActive(false);
    }

    void GiveRewards()
    {
        rewardScreen.SetActive(false);
        gm.gold += goldBonus;
        OfferRecruit(gm.CreateRecruitList());
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

        recruitBtn1.onClick.AddListener(delegate { AddUnit(offers[0]); });
        recruitBtn2.onClick.AddListener(delegate { AddUnit(offers[1]); });
        recruitBtn3.onClick.AddListener(delegate { AddUnit(offers[2]); });
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

        PlayerPrefs.SetInt("continued", 1);
        PlayerPrefs.Save();

        gc.EndGame(); // smooth transition here
    }

    void SkipOffer()
    {
        RemoveOffers();

        PlayerPrefs.SetInt("continued", 1);
        PlayerPrefs.Save();

        gc.EndGame();
    }

    void Ability(Unit unit)
    {
        unit.UseAbility();
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

    void ShowArtifacts()
    {
        for (int i = 0; i < team.artifacts.Count; i++)
        {
            Button artiBtn = Instantiate(artifactButton);
            artiBtn.transform.SetParent(artifactContainer.transform, false);
            artiBtn.image.sprite = team.artifacts[i].artifactVisual;

            switch (team.artifacts[i].artifactRarity)
            {
                case Artifact.Rarity.common:
                    artiBtn.GetComponentInChildren<FillArtifactInfo>().UpdateText($"<color=#073b4c>{ team.artifacts[i].artifactName }</color>", team.artifacts[i].artifactDescription);
                    break;
                case Artifact.Rarity.rare:
                    artiBtn.GetComponentInChildren<FillArtifactInfo>().UpdateText($"<color=#118ab2>{ team.artifacts[i].artifactName }</color>", team.artifacts[i].artifactDescription);
                    break;
                case Artifact.Rarity.epic:
                    artiBtn.GetComponentInChildren<FillArtifactInfo>().UpdateText($"<color=#d100d1>{ team.artifacts[i].artifactName }</color>", team.artifacts[i].artifactDescription);
                    break;
                case Artifact.Rarity.legendary:
                    artiBtn.GetComponentInChildren<FillArtifactInfo>().UpdateText($"<color=#ff6d00>{ team.artifacts[i].artifactName }</color>", team.artifacts[i].artifactDescription);
                    break;
            }
        }
    }
}
