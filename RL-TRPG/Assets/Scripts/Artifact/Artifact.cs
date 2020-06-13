using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : MonoBehaviour
{
    public enum Rarity
    {
        common,
        rare,
        epic,
        legendary
    }

    [Header("Artifact Information")]
    public string artifactName;
    public string artifactDescription;
    public Rarity artifactRarity;
    public Sprite artifactVisual;

    // access classes
    [HideInInspector] public Team team;

    private void Start()
    {
        team = FindObjectOfType<Team>();
        
        ArtifactPassive();
    }

    public virtual void ArtifactPassive()
    {

    }
}
