using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapSettings : MonoBehaviour
{
    [SerializeField] Button settings = null;
    [Space]
    [SerializeField] GameObject set = null;
    [SerializeField] GameObject fog = null;
    // Start is called before the first frame update
    void Start()
    {
        set.SetActive(false);
        fog.SetActive(false);
        settings.onClick.AddListener(Setting);
    }

    void Setting()
    {
        set.SetActive(true);
        fog.SetActive(true);
    }
}
