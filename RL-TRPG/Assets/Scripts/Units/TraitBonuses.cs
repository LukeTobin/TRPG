using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitBonuses : MonoBehaviour
{
    TeamHandler teamHandler;

    List<Unit> units = new List<Unit>();

    // origin
    int human;
    int tiefling;

    // classes
    int duelist;
    int arcane;
    int brawler;
    int ranger;
    int rogue;

    void Start()
    {
        teamHandler = GetComponent<TeamHandler>();
        units = teamHandler.children;

        human = 0; tiefling = 0; duelist = 0; arcane = 0; brawler = 0; ranger = 0; rogue = 0;

        CheckOrigins(units);
        CheckClasses(units);
    }

    void CheckOrigins(List<Unit> list)
    {
        foreach (Unit unit in list)
        {
            switch (unit.GetComponent<Traits>()._origin)
            {
                case Traits.Origin.human:
                    human++;
                    break;
                case Traits.Origin.tiefling:
                    tiefling++;
                    break;
                default:
                    break;
            }
        }

        if(human > 0)
        {
            
        }

        if(tiefling > 0)
        {

        }
    }

    void CheckClasses(List<Unit> list)
    {
        foreach (Unit unit in list)
        {
            switch (unit.GetComponent<Traits>()._class)
            {
                case Traits.Class.duelist:
                    duelist++;
                    break;
                case Traits.Class.arcane:
                    arcane++;
                    break;
                case Traits.Class.brawler:
                    brawler++;
                    break;
                case Traits.Class.ranger:
                    ranger++;
                    break;
                case Traits.Class.rogue:
                    rogue++;
                    break;
                default:
                    break;
            }
        }

        if(duelist > 0)
        {

        }

        if (arcane > 0)
        {

        }

        if (brawler > 0)
        {

        }

        if (ranger > 0)
        {

        }

        if (rogue > 0)
        {

        }

    }

}
