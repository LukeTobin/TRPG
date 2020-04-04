using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flux : MonoBehaviour
{
    Unit unit;
    Ability ab;

    void Start()
    {
        unit = GetComponentInParent<Unit>();
        ab = GetComponent<Ability>();

        Debug.Log(unit.title + " used " + ab.title);

        unit.tempMD += 10;

        Destroy(gameObject);
    }
}
