using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traits : MonoBehaviour
{
    /*
     * Fill in the classes/origins the unit can have.
     * Add it too the TraitBonuses script also too allow for checking of that class / origin
     */
    public enum Origin
    {
        human,
        tiefling,
        nymph
    }

    public enum Class
    {
        duelist,
        arcane,
        brawler,
        ranger,
        rogue
    }

    [Space]
    public Origin _origin;
    public Class _class;
}
