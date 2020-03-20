using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Node : MonoBehaviour
{
    // node information handler

    public enum Type { Null, Battle, Campfire, Mystery }

    public Type nodeType;

    private void OnMouseDown()
    {
        if(nodeType == Type.Battle)
        {
            Debug.Log("Going to battle node");
            SceneManager.LoadScene("TestingBoard");

            // get team
            // create battle scenario
            // send team to battle

        }

        if (nodeType == Type.Campfire)
        {
            Debug.Log("Going to campfire node");

            // open campfire screen
            
            // prototype version: just restore health of all by X%
            // get team list
            // loop each and restore X% health

        }

        if (nodeType == Type.Mystery)
        {
            Debug.Log("Going to mystery node");

            // give mystery scenario
        }
    }

    // if a node needs to be removed ever. Can be done for visualizing where on the map we currently are
    public void MakeNull()
    {
        nodeType = Type.Null;
    }

}
