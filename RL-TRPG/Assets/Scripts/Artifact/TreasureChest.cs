using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : Artifact
{
    public override void ArtifactPassive()
    {
        GameController gc = FindObjectOfType<GameController>();
        if (gc != null)
            gc.bonusGold += 30;
    }
}
