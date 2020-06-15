using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesMask : Artifact
{
    public override void ArtifactPassive()
    {
        foreach (Unit unit in team.units)
        {
            unit.mana = 1;
        }
    }
}
