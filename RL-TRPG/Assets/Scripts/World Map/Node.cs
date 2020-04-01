using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Node : MonoBehaviour
{
    // node information handler

    public enum Type { Null, Battle, Campfire, Mystery, Shop }
    
    public Type nodeType;
    [Space]
    public int stage;
    public int row;
    
    bool visited;
    [HideInInspector] public LineRenderer line;
    WorldManager world;
    SpriteRenderer sr;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        world = FindObjectOfType<WorldManager>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if(world.currentStage == stage && !visited)
        {
            if (nodeType == Type.Battle)
            {
                world.currentStage++;
                Debug.Log("Going to battle node");
                SceneManager.LoadScene("TestingBoard");
                visited = true;

                if(stage == 1)
                {
                    world.BlockPaths(row);
                }

                // get team
                // create battle scenario
                // send team to battle

            }

            if (nodeType == Type.Campfire)
            {
                world.currentStage++;
                Debug.Log("Going to campfire node");
                Visited();
                if (stage == 1)
                {
                    world.BlockPaths(row);
                }
                // open campfire screen

                // prototype version: just restore health of all by X%
                // get team list
                // loop each and restore X% health

            }

            if (nodeType == Type.Mystery)
            {
                world.currentStage++;
                Debug.Log("Going to mystery node");
                Visited();

                if (stage == 1)
                {
                    world.BlockPaths(row);
                }
                // give mystery scenario
            }

            if(nodeType == Type.Shop)
            {
                world.currentStage++;
                Debug.Log("Going to shop node");
                Visited();
                // shop menu

                if (stage == 1)
                {
                    world.BlockPaths(row);
                }

            }
        }
        else
        {
            Debug.Log("incorrect stage");
        }
    }

    // if a node needs to be removed ever. Can be done for visualizing where on the map we currently are
    public void MakeNull()
    {
        nodeType = Type.Null;
    }

    public void Visited()
    {
        visited = true;
        sr.color = new Color(.5f, .5f, .5f, 1f);
    }

}
