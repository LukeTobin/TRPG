using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampiricSceptre : Artifact
{
    public override void ArtifactPassive()
    {
        foreach (Unit unit in team.units)
        {
            unit.lifesteal += 15;
        }
    }
}
