using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosOrb : Artifact
{
    public override void ArtifactPassive()
    {
        foreach (Unit unit in team.units)
        {
            unit.armor -= 4;
            if(unit.armor < 0)
            {
                unit.armor = 0;
            }
            unit.resist -= 4;
            if (unit.resist < 0)
            {
                unit.resist = 0;
            }
            unit.magicDamage += 10;
        }
    }
}
