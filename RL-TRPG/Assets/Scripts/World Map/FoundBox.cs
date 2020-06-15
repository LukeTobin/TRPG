using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundBox : MonoBehaviour
{
    WorldManager wm;
    GameManager gm;

    public GameObject parent;
    public GameObject campfire;
    public int gold = 0;
    public Artifact artifact = null;
    public Artifact trash;

    public void Start()
    {
        wm = FindObjectOfType<WorldManager>();
        gm = FindObjectOfType<GameManager>();
    }

    public void Fill(bool _gold, bool _artifact, int goldAmount = 0, Artifact arti = null)
    {
        if (_gold)
            gold = goldAmount;

        if (_artifact)
            artifact = arti;

        if(!_gold && !_artifact)
        {
            artifact = trash;
        }
    }

    public void TakeLoot()
    {
        if(gold > 0)
        {
            gm.gold += gold;
            wm.goldText.text = $"{gm.gold}";
            campfire.SetActive(false);
            parent.SetActive(false);
        }

        if(artifact != null)
        {
            gm.AddArtifactToTeam(artifact);
            campfire.SetActive(false);
            parent.SetActive(false);
        }
    }
}
