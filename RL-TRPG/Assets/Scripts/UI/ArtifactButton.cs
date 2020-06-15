using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactButton : MonoBehaviour
{

    [SerializeField] GameObject Infobox = null;
    UIManager uim;

    void Start()
    {
        uim = FindObjectOfType<UIManager>();
        Infobox.SetActive(false);
    }

    public void ClickToOpen()
    {
        if (!Infobox.activeInHierarchy)
        {
            if(uim.infoboxEnabled != null)
            {
                uim.infoboxEnabled.SetActive(false);
            }

            Infobox.SetActive(true);
            uim.infoboxEnabled = Infobox;
        }
        else
        {
            Infobox.SetActive(false);
            uim.infoboxEnabled = null;
        }
            
    }
}
