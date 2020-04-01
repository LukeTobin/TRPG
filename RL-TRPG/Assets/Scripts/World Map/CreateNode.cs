using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNode : MonoBehaviour
{
    public enum GlobalNodeType
    {
        normal,
        boss
    }

    public GlobalNodeType type;
    public int stage, row; // stage info => y = stage ~ x = level
    [Space]
    public List<GameObject> nodes;
    [Space]
    public float padding;

    int val;
    WorldManager world;

    public void Start()
    {
        world = FindObjectOfType<WorldManager>();

        if(PlayerPrefs.GetInt("continued") == 1)
        {
            // load original node.
        }
        else
        {
            NewNode();
        }      
    }

    void NewNode()
    {
        if (type == GlobalNodeType.normal)
        {
            Collider2D thisCollider = GetComponent<BoxCollider2D>();
            Bounds bounds = thisCollider.bounds;

            float x = Random.Range(bounds.min.x + padding, bounds.max.x - padding);
            float y = Random.Range(bounds.min.y + padding, bounds.max.y - padding);

            val = Random.Range(0, 100);

            if (stage == 1)
            {
                GameObject node = Instantiate(nodes[0], new Vector3(x, y, 0), Quaternion.identity);
                node.transform.parent = transform;
                node.GetComponent<Node>().stage = stage;
                node.GetComponent<Node>().row = row;
                world.nodeList.Add(node.GetComponent<Node>());
            }
            else
            {
                if (val < 28)
                {
                    // random
                    GameObject node = Instantiate(nodes[Random.Range(1, nodes.Count)], new Vector3(x, y, 0), Quaternion.identity);
                    node.transform.parent = transform;
                    node.GetComponent<Node>().stage = stage;
                    node.GetComponent<Node>().row = row;
                    world.nodeList.Add(node.GetComponent<Node>());
                }
                else
                {
                    // battle node
                    GameObject node = Instantiate(nodes[0], new Vector3(x, y, 0), Quaternion.identity);
                    node.transform.parent = transform;
                    node.GetComponent<Node>().stage = stage;
                    node.GetComponent<Node>().row = row;
                    world.nodeList.Add(node.GetComponent<Node>());
                }
            }
        }
        else
        {
            GameObject node = Instantiate(nodes[0], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            node.GetComponent<Node>().stage = 99;
        }
    }
}
