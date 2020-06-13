using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    /*
     * UI manager
     */

    #region Vars & Initilizing
    GameController gc;
    GameManager gm;
    Team team;

    [Header("Basic UI Elements")]
    public Button endTurnBtn;
    public Button infoButton;
    public Button useAbility;
    public TextMeshProUGUI turnText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI goldText;

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
    bool offerQueue;

    [Header("Reward Screen")]
    [SerializeField] GameObject rewardScreen;
    [SerializeField] Button block;
    [SerializeField] GameObject rewardsHolder;
    [SerializeField] Button continueButton;
    [SerializeField] Sprite gold;
    [SerializeField] Sprite recruiting;
    [SerializeField] Sprite arti;
    [SerializeField] int recruitsLeft = 0;
    bool artifactReward;
    bool goldReward;

    [Header("Artifacts")]
    [SerializeField] Button artifactButton;
    [SerializeField] GameObject artifactContainer;
    public GameObject infoboxEnabled;
    [SerializeField] GameObject artifactPanel;
    [SerializeField] Button artifactBtn1;
    [SerializeField] Image artifact1;
    [SerializeField] TextMeshProUGUI artifact1Title;
    [SerializeField] TextMeshProUGUI artifact1Desc;
    [Space]
    [SerializeField] Button artifactBtn2;
    [SerializeField] Image artifact2;
    [SerializeField] TextMeshProUGUI artifact2Title;
    [SerializeField] TextMeshProUGUI artifact2Desc;
    [Space]
    [SerializeField] Button artifactBtn3;
    [SerializeField] Image artifact3;
    [SerializeField] TextMeshProUGUI artifact3Title;
    [SerializeField] TextMeshProUGUI artifact3Desc;
    [Space]
    public List<Artifact> artifactList = new List<Artifact>();

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
        artifactPanel.SetActive(false);

        skip.onClick.AddListener(SkipOffer);

        if (gc.playerTurn == 1)
        {
            turnText.text = "Player Turn: <color=#2a9d8f>Yours</color>";
        }
        else
        {
            turnText.text = "Player Turn: <color=#e76f51>Enemy</color>";
        }

        roundText.text = "Round: " + gc.round;
        goldText.text = $"<color=#f9c74f>Gold: {gm.gold}</color>";

        ShowArtifacts();

    }

    #endregion

    #region Game UI
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
        else if (infoVisible)
        {
            infoPanel.SetActive(false);
            infoVisible = false;
        }
    }

    #endregion

    #region Rewards

    public void RewardScreen(int goldAmount, int recruitAmount, bool relic = false)
    {
        rewardScreen.SetActive(true);
        Button goldButton = Instantiate(block);
        goldButton.transform.SetParent(rewardsHolder.transform, false);
        goldBonus += goldAmount;
        goldButton.GetComponent<RewardDrops>().drop = RewardDrops.Drops.gold;
        goldButton.GetComponentInChildren<UIIconRef>().GetComponent<Image>().sprite = gold;
        goldButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Gold - <color=#f9c74f>{goldBonus}</color>";
        //goldButton.onClick.AddListener(delegate { AddGold(goldAmount, goldButton.gameObject); });

        for (int i = 0; i < recruitAmount; i++)
        {
            Button recruitButton = Instantiate(block);
            recruitButton.transform.SetParent(rewardsHolder.transform, false);
            recruitButton.GetComponent<RewardDrops>().drop = RewardDrops.Drops.recruit;
            recruitButton.GetComponentInChildren<UIIconRef>().GetComponent<Image>().sprite = recruiting;
            recruitButton.GetComponentInChildren<TextMeshProUGUI>().text = "<color=#02c39a>Recruit</color> a Unit";
            // recruitButton.onClick.AddListener(delegate { OfferRecruit(gm.CreateRecruitList(), true, recruitButton.gameObject); });
        }

        recruitsLeft = recruitAmount;

        if (relic)
        {
            Button relicButton = Instantiate(block);
            relicButton.transform.SetParent(rewardsHolder.transform, false);
            relicButton.GetComponent<RewardDrops>().drop = RewardDrops.Drops.artifact;
            relicButton.GetComponentInChildren<UIIconRef>().GetComponent<Image>().sprite = arti;
            relicButton.GetComponentInChildren<TextMeshProUGUI>().text = "Obtain an <color=#f95738>Artifact</color>";
            // relicButton.onClick.AddListener(delegate { OfferArtifact(gm.CreateArtifactList(), relicButton.gameObject); });

            artifactReward = true;
        }
    }

    /// <summary>
    /// If continue is clicked check what rewards are available.
    /// </summary>
    void GiveRewards()
    {
        rewardScreen.SetActive(false);

        if (goldReward)
        {
            AddGold(goldBonus, null);
        }

        if (recruitsLeft > 0)
        {
            if (recruitsLeft == 1)
            {
                offerQueue = false;
                OfferRecruit(gm.CreateRecruitList());
            }
            else if (recruitsLeft == 2)
            {
                OfferRecruit(gm.CreateRecruitList(), true);
            }
        }
        else if (artifactReward)
        {
            OfferArtifact(gm.CreateArtifactList());
        }
        else
        {
            PlayerPrefs.SetInt("continued", 1);
            PlayerPrefs.Save();

            gc.EndGame(); // smooth transition here
        }
    }

    void OfferRecruit(List<Unit> offers, bool offerAgain = false, GameObject obj = null) // pass units
    {
        recruitPanel.SetActive(true);
        recruits.Clear();

        recruit1.GetComponent<Image>().sprite = offers[0].profile;
        recruit2.GetComponent<Image>().sprite = offers[1].profile;
        recruit3.GetComponent<Image>().sprite = offers[2].profile;

        recruits.Add(offers[0]);
        recruits.Add(offers[1]);
        recruits.Add(offers[2]);

        recruitBtn1.onClick.AddListener(delegate { AddUnit(offers[0]); });
        recruitBtn2.onClick.AddListener(delegate { AddUnit(offers[1]); });
        recruitBtn3.onClick.AddListener(delegate { AddUnit(offers[2]); });

        recruitsLeft--;

        if (offerAgain)
            offerQueue = true;

        if (obj != null)
            Destroy(obj);
    }

    public void RemoveOffers()
    {
        recruitPanel.SetActive(false);
        artifactPanel.SetActive(false);

        recruits.Clear();
        artifactList.Clear();
    }

    public void AddUnit(Unit unit)
    {
        team.AddNewUnit(unit);
        RemoveOffers();

        if (offerQueue)
        {
            offerQueue = false;
            OfferRecruit(gm.CreateRecruitList());
        }
        else if (artifactReward)
        {
            OfferArtifact(gm.CreateArtifactList());
        }
        else
        {
            RemoveOffers();

            PlayerPrefs.SetInt("continued", 1);
            PlayerPrefs.Save();

            gc.EndGame(); // smooth transition here
        }
    }

    void SkipOffer()
    {
        RemoveOffers();

        if (offerQueue)
        {
            offerQueue = false;
            OfferRecruit(gm.CreateRecruitList());
        }
        else if (artifactReward)
        {
            OfferArtifact(gm.CreateArtifactList());
        }
        else
        {
            RemoveOffers();

            PlayerPrefs.SetInt("continued", 1);
            PlayerPrefs.Save();

            gc.EndGame(); // smooth transition here
        }
    }

    void AddGold(int amount, GameObject ObjectToDestroy = null)
    {
        goldText.text = $"<color=#f9c74f>Gold: {gm.gold} (+{goldBonus})</color>";
        gm.gold += goldBonus;
        goldBonus = 0;
        goldReward = false;
        if (ObjectToDestroy != null)
            Destroy(ObjectToDestroy);
    }

    void OfferArtifact(List<Artifact> options, GameObject obj = null)
    {
        artifactPanel.SetActive(true);
        artifactList.Clear();

        artifact1.sprite = options[0].artifactVisual;
        artifact2.sprite = options[1].artifactVisual;
        artifact3.sprite = options[2].artifactVisual;

        artifact1Title.text = options[0].artifactDescription;
        artifact2Title.text = options[1].artifactDescription;
        artifact3Title.text = options[2].artifactDescription;

        #region Get title color
        switch (options[0].artifactRarity)
        {
            case Artifact.Rarity.common:
                artifact1Title.text = $"<color=#073b4c> { options[0].artifactName }</color>";
                break;
            case Artifact.Rarity.rare:
                artifact1Title.text = $"<color=#118ab2> { options[0].artifactName }</color>";
                break;
            case Artifact.Rarity.epic:
                artifact1Title.text = $"<color=#d100d1> { options[0].artifactName }</color>";
                break;
            case Artifact.Rarity.legendary:
                artifact1Title.text = $"<color=#ff6d00> { options[0].artifactName }</color>";
                break;
        }

        switch (options[1].artifactRarity)
        {
            case Artifact.Rarity.common:
                artifact2Title.text = $"<color=#073b4c> { options[1].artifactName }</color>";
                break;
            case Artifact.Rarity.rare:
                artifact2Title.text = $"<color=#118ab2> { options[1].artifactName }</color>";
                break;
            case Artifact.Rarity.epic:
                artifact2Title.text = $"<color=#d100d1> { options[1].artifactName }</color>";
                break;
            case Artifact.Rarity.legendary:
                artifact2Title.text = $"<color=#ff6d00> { options[1].artifactName }</color>";
                break;
        }

        switch (options[2].artifactRarity)
        {
            case Artifact.Rarity.common:
                artifact3Title.text = $"<color=#073b4c> { options[2].artifactName }</color>";
                break;
            case Artifact.Rarity.rare:
                artifact3Title.text = $"<color=#118ab2> { options[2].artifactName }</color>";
                break;
            case Artifact.Rarity.epic:
                artifact3Title.text = $"<color=#d100d1> { options[2].artifactName }</color>";
                break;
            case Artifact.Rarity.legendary:
                artifact3Title.text = $"<color=#ff6d00> { options[2].artifactName }</color>";
                break;
        }
        #endregion

        artifactList.Add(options[0]);
        artifactList.Add(options[1]);
        artifactList.Add(options[2]);

        artifactBtn1.onClick.AddListener(delegate { AddArtifact(options[0]); });
        artifactBtn2.onClick.AddListener(delegate { AddArtifact(options[1]); });
        artifactBtn3.onClick.AddListener(delegate { AddArtifact(options[2]); });

        if (obj != null)
            Destroy(obj);
    }

    public void AddArtifact(Artifact artifact)
    {
        team.AddNewArtifact(artifact);
        artifactReward = false;

        if (offerQueue || recruitsLeft > 0)
        {
            RemoveOffers();
            GiveRewards();
        }
        else
        {
            RemoveOffers();

            PlayerPrefs.SetInt("continued", 1);
            PlayerPrefs.Save();

            gc.EndGame(); // smooth transition here
        }
    }

    #endregion

    #region Artifacts

    void ShowArtifacts()
    {
        if (team.artifacts.Count > 0)
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

    #endregion
}
