using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkarTotem : Artifact
{
    public override void ArtifactPassive()
    {
        foreach (Unit unit in team.units)
        {
            unit.resist += Mathf.RoundToInt(unit.resist / 2);
        }
    }
}
