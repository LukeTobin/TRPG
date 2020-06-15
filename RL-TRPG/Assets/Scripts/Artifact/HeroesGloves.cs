using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesGloves : Artifact
{
    public override void ArtifactPassive()
    {
        foreach (Unit unit in team.units)
        {
            unit.mrPen += 5;
        }
    }
}
