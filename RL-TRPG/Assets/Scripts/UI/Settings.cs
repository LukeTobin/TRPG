using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Settings : MonoBehaviour
{
    [SerializeField] Button exit = null;
    [SerializeField] Button abandon = null;
    [SerializeField] Button credits = null;

    [SerializeField] GameObject creditObject = null;
    [SerializeField] GameObject background = null;
   // [SerializeField] Toggle music = null;
   // [SerializeField] Toggle sfx = null;

    GameManager gm;


    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        gameObject.SetActive(false);

        exit.onClick.AddListener(Close);
        abandon.onClick.AddListener(Abandon);
        credits.onClick.AddListener(ShowCredits);
    }

    void ShowCredits()
    {
        creditObject.SetActive(true);
        gameObject.SetActive(false);
    }

    void Close()
    {
        gameObject.SetActive(false);
        background.SetActive(false);
    }

    void Abandon()
    {
        gm.AbandonRun();
        Close();
    }
}
