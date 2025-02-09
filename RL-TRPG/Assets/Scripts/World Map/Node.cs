﻿using System.Collections;
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
    
    public bool visited;
    [HideInInspector] public LineRenderer line;
    WorldManager world;
    SpriteRenderer sr;
    GameManager gm;
    Team team;

    bool updatedVisit;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        world = FindObjectOfType<WorldManager>();
        sr = GetComponent<SpriteRenderer>();
        team = GameObject.FindGameObjectWithTag("Team").GetComponent<Team>();

        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (visited)
            Visited();
    }

    private void Update()
    {
        if (!updatedVisit && visited)
        {
            Visited();
            updatedVisit = true;
        }
    }

    private void OnMouseDown()
    {
        if (world.currentStage == stage && !visited)
        {
            if (nodeType == Type.Battle)
            {
                //Visited();
                gm.currentNode = this;
                if (stage == 1)
                {
                    world.BlockPaths(row);
                }

                world.SaveWorldState();
                SceneManager.LoadScene("TestingBoard");
                // get team
                // create battle scenario
                // send team to battle

            }

            if (nodeType == Type.Campfire)
            {
                world.currentStage++;
                gm.currentStage++;

                Debug.Log("Going to campfire node");
                Visited();
                if (stage == 1)
                {
                    world.BlockPaths(row);
                }

                world.SaveWorldState();

                // open campfire screen
                world.campfire.SetCampfireActive();
                

            }

            if (nodeType == Type.Mystery)
            {
                world.currentStage++;
                gm.currentStage++;
                Debug.Log("Going to mystery node");
                Visited();

                if (stage == 1)
                {
                    world.BlockPaths(row);
                }

                world.SaveWorldState();
                // give mystery scenario
            }

            if (nodeType == Type.Shop)
            {
                world.currentStage++;
                gm.currentStage++;
                Debug.Log("Going to shop node");
                Visited();
                // shop menu

                if (stage == 1)
                {
                    world.BlockPaths(row);
                }

                world.SaveWorldState();

                world.shop.CreateShop();
            }
        }
        else if (world.currentStage > 5)
        {
            if (nodeType == Type.Battle)
            {
                world.currentStage++;
                gm.currentStage++;
                world.SaveWorldState();
                SceneManager.LoadScene("TestingBoard");
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
        if(sr != null)
            sr.color = new Color(.5f, .5f, .5f, 1f);
        PlayerPrefs.SetInt(stage + "-" + row + ".visited", 1);
        PlayerPrefs.Save();
    }
}
