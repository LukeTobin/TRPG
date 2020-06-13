using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddRecruit : MonoBehaviour
{
    UIManager ui;

    public int num;

    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("BoardUI").GetComponent<UIManager>();
    }

    public void AddUnit()
    {
        ui.AddUnit(ui.recruits[num]);
        ui.RemoveOffers();
    }

    public void AddArtifact()
    {
        ui.AddArtifact(ui.artifactList[num]);
        ui.RemoveOffers();
    }
}
