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
    int nymph;
    int satyr;

    // classes
    int dueler;
    int caster;
    int enchanter;
    int brawler;
    int ranger;
    int rogue;

    void Start()
    {
        teamHandler = GetComponent<TeamHandler>();
        units = teamHandler.friendlyUnits;

        human = 0; tiefling = 0; dueler = 0; caster = 0; enchanter = 0; brawler = 0; ranger = 0; rogue = 0;

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
                case Traits.Origin.nymph:
                    nymph++;
                    break;
                case Traits.Origin.satyr:
                    satyr++;
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

        if(nymph > 0)
        {

        }

        if(satyr > 0)
        {

        }
    }

    void CheckClasses(List<Unit> list)
    {
        foreach (Unit unit in list)
        {
            switch (unit.GetComponent<Traits>()._class)
            {
                case Traits.Class.dueler:
                    dueler++;
                    break;
                case Traits.Class.caster:
                    caster++;
                    break;
                case Traits.Class.enchanter:
                    enchanter++;
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

        if(dueler > 0)
        {

        }

        if (caster > 0)
        {

        }

        if(enchanter > 0)
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
