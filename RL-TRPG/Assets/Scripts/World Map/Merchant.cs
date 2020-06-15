using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Merchant : MonoBehaviour
{
    GameManager gm;
    WorldManager wm;

    public GameObject shopGUI;
    public Button continueBtn;
    [Space]
    public Button item1;
    public Image frontImage1;
    public TextMeshProUGUI title1;
    public TextMeshProUGUI desc1;
    public TextMeshProUGUI cost1;
    public int itemCost1;
    public Artifact artifact1;

    [Space]
    public Button item2;
    public Image frontImage2;
    public TextMeshProUGUI title2;
    public TextMeshProUGUI desc2;
    public TextMeshProUGUI cost2;
    public int itemCost2;
    public Artifact artifact2;

    [Space]
    public Button item3;
    public Image frontImage3;
    public TextMeshProUGUI title3;
    public TextMeshProUGUI desc3;
    public TextMeshProUGUI cost3;
    public int itemCost3;
    public Artifact artifact3;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        wm = FindObjectOfType<WorldManager>();

        item1.onClick.AddListener(delegate { Purchase(this.GetComponent<Button>(), itemCost1, artifact1); });
        item2.onClick.AddListener(delegate { Purchase(this.GetComponent<Button>(), itemCost2, artifact2); });
        item3.onClick.AddListener(delegate { Purchase(this.GetComponent<Button>(), itemCost3, artifact3); });

        continueBtn.onClick.AddListener(delegate { gameObject.SetActive(false); });

        shopGUI.SetActive(false);
    }

    public void CreateShop()
    {
        shopGUI.SetActive(true);
        List<Artifact> shops = gm.CreateArtifactList();
        artifact1 = shops[0];
        artifact2 = shops[1];
        artifact3 = shops[2];

        item1.GetComponent<Image>().sprite = artifact1.artifactVisual;
        item2.GetComponent<Image>().sprite = artifact2.artifactVisual;
        item3.GetComponent<Image>().sprite = artifact3.artifactVisual;

        frontImage1.sprite = artifact1.artifactVisual;
        frontImage2.sprite = artifact2.artifactVisual;
        frontImage3.sprite = artifact3.artifactVisual;

        title1.text = artifact1.artifactName;
        title2.text = artifact2.artifactName;
        title3.text = artifact3.artifactName;

        desc1.text = artifact1.artifactDescription;
        desc2.text = artifact2.artifactDescription;
        desc3.text = artifact3.artifactDescription;

        itemCost1 = artifact1.artifactCost;
        itemCost2 = artifact2.artifactCost;
        itemCost3 = artifact3.artifactCost;

        cost1.text = "$" + itemCost1.ToString();
        cost2.text = "$" + itemCost2.ToString();
        cost3.text = "$" + itemCost3.ToString();
    }

    void Purchase(Button which, int cost, Artifact artifact)
    {
        if(gm.gold >= cost)
        {
            gm.gold -= cost;
            wm.goldText.text = $"{gm.gold}";

            gm.AddArtifactToTeam(artifact);

            which.gameObject.SetActive(false);
        }
    }
}
