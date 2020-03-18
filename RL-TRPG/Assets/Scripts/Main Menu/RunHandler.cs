using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RunHandler : MonoBehaviour
{

    public Button run;

    void Start()
    {
        run.onClick.AddListener(NormalRun);
    }

    void NormalRun()
    {
        SceneManager.LoadScene("Map");
    }
}
