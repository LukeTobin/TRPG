using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fortify : MonoBehaviour
{
    Unit unit;
    Ability ab;

    void Start()
    {
        unit = GetComponentInParent<Unit>();
        ab = GetComponent<Ability>();

        Debug.Log(unit.title + " used " + ab.title);

        unit.tempAR += 10;
        unit.tempMR += 10;

        Destroy(gameObject);
    }
}
