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
        Debug.Log("adding unit: " + ui.recruits[num]);
        ui.RemoveOffers();
    }
}
