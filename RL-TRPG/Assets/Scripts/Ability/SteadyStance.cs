using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteadyStance : MonoBehaviour
{
    Unit unit;
    Ability ab;

    void Start()
    {
        unit = GetComponentInParent<Unit>();
        ab = GetComponent<Ability>();

        Debug.Log(unit.title + " used " + ab.title);

        unit.armor += 5;
        unit.resist += 5;

        Destroy(gameObject);
    }
}
