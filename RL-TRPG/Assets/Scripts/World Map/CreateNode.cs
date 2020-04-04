using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNode : MonoBehaviour
{
    public int stage, row; // stage info => y = stage ~ x = level
    [Space]
    public List<GameObject> nodes;
    [Space]
    public float padding;

    int val;
    int rand;
    WorldManager world;

    public void Start()
    {
        world = FindObjectOfType<WorldManager>();

        if (PlayerPrefs.GetInt("continued") == 1)
        {
            LoadNode();
        }
        else
        {
            NewNode();
        }
    }

    void NewNode()
    {
        Collider2D thisCollider = GetComponent<BoxCollider2D>();
        Bounds bounds = thisCollider.bounds;

        float x = Random.Range(bounds.min.x + padding, bounds.max.x - padding);
        float y = Random.Range(bounds.min.y + padding, bounds.max.y - padding);

        val = Random.Range(0, 100);
        rand = Random.Range(1, nodes.Count);

        if (val < 28 && stage != 1)
        {
            // random
            GameObject node = Instantiate(nodes[rand], new Vector3(x, y, 0), Quaternion.identity);
            node.transform.parent = transform;
            node.GetComponent<Node>().stage = stage;
            node.GetComponent<Node>().row = row;
            world.nodeList.Add(node.GetComponent<Node>());
        }
        else
        {
            rand = 0;

            // battle node
            GameObject node = Instantiate(nodes[0], new Vector3(x, y, 0), Quaternion.identity);
            node.transform.parent = transform;
            node.GetComponent<Node>().stage = stage;
            node.GetComponent<Node>().row = row;
            world.nodeList.Add(node.GetComponent<Node>());
        }


        // save location
        PlayerPrefs.SetFloat(stage + "-" + row + ".x", x);
        PlayerPrefs.SetFloat(stage + "-" + row + ".y", y);
        PlayerPrefs.SetInt(stage + "-" + row + ".node", rand);

        PlayerPrefs.Save();
    }

    void LoadNode()
    {
        GameObject node = Instantiate(nodes[PlayerPrefs.GetInt(stage + "-" + row + ".node")], new Vector3(PlayerPrefs.GetFloat(stage + "-" + row + ".x"), PlayerPrefs.GetFloat(stage + "-" + row + ".y"), 0), Quaternion.identity);
        node.transform.parent = transform;
        node.GetComponent<Node>().stage = stage;
        node.GetComponent<Node>().row = row;
        if (PlayerPrefs.GetInt(stage + "-" + row + ".vitied") == 1)
            node.GetComponent<Node>().visited = true;
        world.nodeList.Add(node.GetComponent<Node>());
    }
}
