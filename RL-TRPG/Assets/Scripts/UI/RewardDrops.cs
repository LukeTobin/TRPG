using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardDrops : MonoBehaviour
{
    public enum Drops
    {
        gold,
        recruit,
        artifact,
        legendaryArtifact
    }

    public Drops drop;
}
