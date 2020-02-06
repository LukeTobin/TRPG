using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    Transform bar; 

    void Start()
    {
        bar = transform.Find("Bar");
    }

    public void SetSize(float max, float current)
    {
        float OldRange = 0f - max;
        float NewRange = 0f - 1f;
        float NewValue = (((current - 0f) * NewRange) / OldRange) + 0f;
        bar.localScale = new Vector3(NewValue, 1f);
    }
}
