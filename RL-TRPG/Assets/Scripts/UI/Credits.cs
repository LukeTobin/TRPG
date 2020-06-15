using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject settings = null;
    [SerializeField] Button exit = null;

    private void Start()
    {
        gameObject.SetActive(false);
        exit.onClick.AddListener(Close);
    }

    void Close()
    {
        settings.SetActive(true);
        gameObject.SetActive(false);
    }
}
