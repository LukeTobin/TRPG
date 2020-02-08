using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{

    public GameObject myLeader;//will be refactored later

    public static GameObject leader;
    public static List<GameObject> units = new List<GameObject>();

    private void Start()
    {
        leader = myLeader;
        leader.GetComponent<Unit>().isLeader = true;
    }
}
