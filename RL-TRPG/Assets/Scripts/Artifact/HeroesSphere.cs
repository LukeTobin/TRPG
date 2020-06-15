using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesSphere : Artifact
{
    public override void ArtifactPassive()
    {
        foreach (Unit unit in team.units)
        {
            unit.armor -= 5;
            if (unit.armor < 0)
            {
                unit.armor = 0;
            }
            unit.armorPen += 80;
        }
    }
}
