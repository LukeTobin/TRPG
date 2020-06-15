using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitObject : MonoBehaviour
{
    public GameObject objectToClose;

    public void CloseObject()
    {
        objectToClose.SetActive(false);
    }
}
