using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Node : MonoBehaviour
{
    public int nodeType; // 0 = battle, 1 = walk, 2 = campfire, 3 = event


    /*
     * Start of node script - TODO
     * Typically will load whatever that tile/node holds
     */

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Node Pressed");

            if (nodeType == 0)
            {
                Debug.Log("Loading New Scene");
                SceneManager.LoadScene(2);
            }
        }
    }
}
