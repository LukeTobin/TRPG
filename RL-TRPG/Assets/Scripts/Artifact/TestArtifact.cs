using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestArtifact : Artifact
{
    public override void ArtifactPassive()
    {
        Debug.Log($"Unit count: { team.units.Count }");
    }
}
