using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharpen : MonoBehaviour
{
    Unit unit;
    Ability ab;

    void Start()
    {
        unit = GetComponentInParent<Unit>();
        ab = GetComponent<Ability>();

        Debug.Log(unit.title + " used " + ab.title);

        unit.attackDamage += 5;

        Destroy(gameObject);
    }
}
