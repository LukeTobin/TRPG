using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI heart = null;
    [SerializeField] TextMeshProUGUI mana = null;

    #region LEGACY VARS
    /*
    [Space]
   // Slider slider; //healthbar
    //[Space]
    [SerializeField] Image heartContainer;
    [SerializeField] TextMeshProUGUI heartText;
    [Space]
    [SerializeField] Sprite heart_full;
    [SerializeField] Sprite heart_80;
    [SerializeField] Sprite heart_60;
    [SerializeField] Sprite heart_40;
    [SerializeField] Sprite heart_20;
    [SerializeField] Sprite heart_low;
    [Space]
    [SerializeField] Image manaContainer;
    [SerializeField] TextMeshProUGUI manaText;
    [Space]
    [SerializeField] Sprite mana_5;
    [SerializeField] Sprite mana_4;
    [SerializeField] Sprite mana_3;
    [SerializeField] Sprite mana_2;
    [SerializeField] Sprite mana_1;
    [SerializeField] Sprite mana_0;
    */
    #endregion

    Unit owner;

    // Start is called before the first frame update
    void Start()
    {
    //    slider = GetComponentInChildren<Slider>();
        owner = GetComponentInParent<Unit>();

        UpdateNormal();
    }

    public void UpdateNormal()
    {
        heart.text = $"{ owner.health }";
        if(mana != null)
            mana.text = $"{ owner.mana }";

        /*
        slider.gameObject.SetActive(false);

        heartContainer.gameObject.SetActive(true);
        heartText.text = "" + owner.health;
        heartContainer.sprite = SetHeartImage(owner.health);

        manaContainer.gameObject.SetActive(true);
        manaText.text = "" + owner.mana;
        manaContainer.sprite = SetManaImage(owner.mana);
        */
    }

    #region UI Components (LEGACY)
    /*
    Sprite SetHeartImage(int health)
    {
        if(health == owner.maxHealth)
        {
            return heart_full;
        }
        else if(health == Percentage(80, owner.maxHealth))
        {
            return heart_80;
        }
        else if (health == Percentage(60, owner.maxHealth))
        {
            return heart_60;
        }
        else if (health == Percentage(40, owner.maxHealth))
        {
            return heart_40;
        }
        else if (health == Percentage(20, owner.maxHealth))
        {
            return heart_20;
        }
        else if (health == Percentage(5, owner.maxHealth))
        {
            return heart_low;
        }
        else
        {
            return heart_low;
        }
    }

    Sprite SetManaImage(int mana)
    {
        switch (mana)
        {
            case 0:
                return mana_0;
            case 1:
                return mana_1;
            case 2:
                return mana_2;
            case 3:
                return mana_3;
            case 4:
                return mana_4;
            case 5:
                return mana_5;
            default:
                return mana_0;
        }
    }

    int Percentage(int value, int max)
    {
        return ((max / 100) * value) - max;
    }

    float SetHealthbarSize(float max, float current)
    {
        float OldRange = 0f - max; // get your old range
        float NewRange = 0f - 1f; // new range 0f - 1f (0-100%)
        float NewValue = (((current - 0f) * NewRange) / OldRange) + 0f; // creates a new value to reprsent our current health in terms of (0%-100%)
        return NewValue;
    }
    */
    #endregion
}
