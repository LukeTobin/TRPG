using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stoneplate : Artifact
{
    public override void ArtifactPassive()
    {
        foreach (Unit unit in team.units)
        {
            unit.armor += 5;
        }
    }
}
