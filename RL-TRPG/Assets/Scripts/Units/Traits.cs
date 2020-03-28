using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traits : MonoBehaviour
{
    public enum Origin
    {
        human,
        tiefling 
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
