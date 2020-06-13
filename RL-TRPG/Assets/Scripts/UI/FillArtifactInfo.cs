using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FillArtifactInfo : MonoBehaviour
{
    public TextMeshProUGUI title; 
    public TextMeshProUGUI desc;

    public void UpdateText(string titleText, string descText)
    {
        title.text = titleText;
        desc.text = descText;
    }
}
