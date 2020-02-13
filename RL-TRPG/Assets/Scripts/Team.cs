using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    /*
     * Team information holder 
     */

    public GameObject myLeader;//will be refactored later

    public static GameObject leader; // leader unit
    public static List<GameObject> units = new List<GameObject>(); // list of units

    private void Start()
    {
        // setting leader unit
        leader = myLeader;
        leader.GetComponent<Unit>().isLeader = true;
    }
}
