using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageOrb : Artifact
{
    public override void ArtifactPassive()
    {
        foreach (Unit unit in team.units)
        {
            unit.attackDamage += 4;
        }
    }
}
