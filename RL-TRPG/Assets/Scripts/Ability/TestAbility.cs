using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAbility : MonoBehaviour
{
    Unit unit;

    void Start()
    {
        unit = GetComponentInParent<Unit>();
        Debug.Log("Ability test finished successfully.");
        Destroy(gameObject);
    }
}
