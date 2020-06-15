using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatCoin : Artifact
{
    public override void ArtifactPassive()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        gm.gold += 20;
    }
}
