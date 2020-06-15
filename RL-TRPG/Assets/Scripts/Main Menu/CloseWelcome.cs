using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWelcome : MonoBehaviour
{
    [SerializeField] GameObject parentObj = null;
    [SerializeField] GameObject nextMessage = null;

    public void CloseWelcomeMessage()
    {
        PlayerPrefs.SetInt("firstBoot", 1);
        PlayerPrefs.Save();

        if (nextMessage != null)
            nextMessage.gameObject.SetActive(true);

        parentObj.SetActive(false);
    }

    public void CloseAfiliate()
    {
        parentObj.SetActive(false);
    }

    public void GoToDiscord()
    {
        FirstBoot boot = FindObjectOfType<FirstBoot>();
        if (!boot.affiliateArtifact)
        {
            boot.affiliateArtifact = true;
            // give artifact
        }

        //Application.OpenURL("");

    }

    public void GoToTwitter()
    {
        FirstBoot boot = FindObjectOfType<FirstBoot>();
        if (!boot.affiliateArtifact)
        {
            boot.affiliateArtifact = true;
            // give artifact
        }

        //Application.OpenURL("");
    }
}
