using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyWeight : Artifact
{
    public override void ArtifactPassive()
    {
        foreach (Unit unit in team.units)
        {
            unit.health += 3;
        }
    }
}

