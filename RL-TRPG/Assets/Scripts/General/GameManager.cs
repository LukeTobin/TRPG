using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentStage = 0;

    [Header("Enemy Pool")]
    public List<Unit> enemyList = new List<Unit>();

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
