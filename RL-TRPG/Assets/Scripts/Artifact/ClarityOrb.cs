using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClarityOrb : Artifact
{
    public override void ArtifactPassive()
    {
        foreach (Unit unit in team.units)
        {
            unit.magicDamage += 4;
        }
    }
}
