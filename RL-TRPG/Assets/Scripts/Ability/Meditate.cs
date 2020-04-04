using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meditate : MonoBehaviour
{
    Unit unit;
    Ability ab;

    void Start()
    {
        unit = GetComponentInParent<Unit>();
        ab = GetComponent<Ability>();

        Debug.Log(unit.title + " used " + ab.title);

        int percent = (unit.maxHealth / 100) * 10;
        unit.health += percent;

        Destroy(gameObject);
    }
}
