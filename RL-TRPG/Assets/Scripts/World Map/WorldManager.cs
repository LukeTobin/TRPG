using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public GameObject boss;
    [Space]
    public int currentStage = 1;
    public List<Node> nodeList = new List<Node>();

    int maxVal = 15;
    bool madeLines;

    [Header("Admin Tests")]
    public bool ClearPrefs;

    private void Start()
    {
        if (PlayerPrefs.GetInt("continued") == 1)
        {
            LoadWorld();
        }
    }

    private void Update()
    {
        if (!madeLines && maxVal >= nodeList.Count - 1)
        {
            for (int i = 0; i < nodeList.Count; i++)
            {
                for (int j = 0; j < nodeList.Count; j++)
                {
                    if (nodeList[i].stage + 1 == nodeList[j].stage && nodeList[i].row == nodeList[j].row)
                    {
                        nodeList[i].line.SetPosition(0, nodeList[i].transform.position);
                        nodeList[i].line.SetPosition(1, nodeList[j].transform.position);
                        break;
                    }
                }

                if (nodeList[i].stage == 5)
                {
                    nodeList[i].line.SetPosition(0, nodeList[i].transform.position);
                    nodeList[i].line.SetPosition(1, boss.transform.position);
                }
            }
            madeLines = true;
        }

        if (currentStage > 5 && currentStage != 99)
        {
            currentStage = 99;
        }

        if (ClearPrefs)
        {
            PlayerPrefs.DeleteAll();
            ClearPrefs = false;
        }
    }

    public void BlockPaths(int mainRow)
    {
        foreach (Node node in nodeList)
        {
            if (node.row != mainRow)
            {
                node.Visited();
            }
        }
    }

    public void UpdatePaths()
    {
        foreach (Node node in nodeList)
        {
            if (node.stage < currentStage)
            {
                node.Visited();
            }
        }
    }

    public void SaveWorldState()
    {
        PlayerPrefs.SetInt("stage", currentStage);
        PlayerPrefs.Save();
    }

    void LoadWorld()
    {
        currentStage = PlayerPrefs.GetInt("stage");
    }
}
