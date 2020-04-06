using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{

    /*
     * W.I.P
     */

    /*
    function reconstruct_path(cameFrom, current)
    total_path := {current}
    while current in cameFrom.Keys:
        current := cameFrom[current]
        total_path.prepend(current)
    return total_path

    // A* finds a path from start to goal.
    // h is the heuristic function. h(n) estimates the cost to reach goal from node n.
    function A_Star(start, goal, h)
        // The set of discovered nodes that may need to be (re-)expanded.
        // Initially, only the start node is known.
        openSet := {start}

        // For node n, cameFrom[n] is the node immediately preceding it on the cheapest path from start to n currently known.
        cameFrom := an empty map

        // For node n, gScore[n] is the cost of the cheapest path from start to n currently known.
        gScore := map with default value of Infinity
        gScore[start] := 0

        // For node n, fScore[n] := gScore[n] + h(n).
        fScore := map with default value of Infinity
        fScore[start] := h(start)

        while openSet is not empty
            current := the node in openSet having the lowest fScore[] value
            if current = goal
                return reconstruct_path(cameFrom, current)

            openSet.Remove(current)
            for each neighbor of current
                // d(current,neighbor) is the weight of the edge from current to neighbor
                // tentative_gScore is the distance from start to the neighbor through current
                tentative_gScore := gScore[current] + d(current, neighbor)
                if tentative_gScore < gScore[neighbor]
                    // This path to neighbor is better than any previous one. Record it!
                    cameFrom[neighbor] := current
                    gScore[neighbor] := tentative_gScore
                    fScore[neighbor] := gScore[neighbor] + h(neighbor)
                    if neighbor not in openSet
                        openSet.add(neighbor)

        // Open set is empty but goal was never reached
        return failur
    */

    GameController gc;

    private const int MOVE_STRAIGHT_COST = 10;

    public static Pathfinding Instance { get; private set; }

    private List<Tile> openList;
    private List<Tile> closedList;

    private void Start()
    {
        Instance = this;
        gc = FindObjectOfType<GameController>();   
    }

    public List<Vector2> FindPath(Vector2 startPos, Vector2 endPos)
    {
        List<Tile> path = FindPath((int)startPos.x, (int)startPos.y, (int)endPos.x, (int)endPos.y);
        if(path == null)
        {
            Debug.Log("couldnt move from " + (int)startPos.x + "," + (int)startPos.y + " : to : " + (int)endPos.x + "," + (int)endPos.y);
            return null;
        }
        else
        {
            List<Vector2> vectorPath = new List<Vector2>();
            foreach(Tile tile in path)
            {
                vectorPath.Add(new Vector2(tile.x, tile.y));
            }
            return vectorPath;
        }
    }

    private List<Tile> FindPath(int startX, int startY, int endX, int endY)
    {
        Tile startNode = gc.map.GetTile(startX, startY);
        Tile endNode = gc.map.GetTile(endX, endY);

        if (endNode == null)
            Debug.Log("no end node");

        openList = new List<Tile>() { startNode };
        closedList = new List<Tile>();

        for (int x = 0; x < gc.x; x++)
        {
            for (int y = 0; y < gc.y; y++)
            {
                Tile tile = gc.map.GetTile(x*2, y*2);
                if(tile != null)
                {
                    tile.g = int.MaxValue;
                    tile.CalculateF();
                    tile.originNode = null;
                }              
            }
        }

        if (startNode == null)
            Debug.Log("no start node");
        if (endNode == null)
            Debug.Log("no end node");
            
        startNode.g = 0;
        startNode.h = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateF();

        while(openList.Count > 0)
        {
            Tile currentNode = GetLowestTile(openList);
            if(currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach(Tile neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;
                
                if (!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.g + CalculateDistanceCost(currentNode, neighbourNode);
                if(tentativeGCost < neighbourNode.g)
                {
                    neighbourNode.originNode = currentNode;
                    neighbourNode.g = tentativeGCost;
                    neighbourNode.h = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateF();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        // no path
        Debug.Log("No path found.");
        return null;
    }

    private List<Tile> GetNeighbourList(Tile currentNode)
    {
        List<Tile> neighbour = new List<Tile>();

        if(currentNode.x - 2 >= 0)
        {
            // Left
            neighbour.Add(gc.map.GetTile(currentNode.x - 2, currentNode.y));
        }

        if(currentNode.x + 2 < gc.x * 2)
        {
            // Right
            if(gc.map.GetTile(currentNode.x + 2, currentNode.y) != null)
            {
                neighbour.Add(gc.map.GetTile(currentNode.x + 2, currentNode.y));
            }         
        }

        if(currentNode.y - 2 >= 0)
        {
            if(gc.map.GetTile(currentNode.x, currentNode.y - 2) != null)
                neighbour.Add(gc.map.GetTile(currentNode.x, currentNode.y - 2));
        }
        
        if(currentNode.y + 2 < gc.y * 2)
        {
            if(gc.map.GetTile(currentNode.x, currentNode.y + 2) != null)
                neighbour.Add(gc.map.GetTile(currentNode.x, currentNode.y + 2));
        }

        return neighbour;
    }

    private List<Tile> CalculatePath(Tile endNode)
    {
        List<Tile> path = new List<Tile>();
        path.Add(endNode);
        Tile currentNode = endNode;
        while(currentNode.originNode != null)
        {
            path.Add(currentNode.originNode);
            currentNode = currentNode.originNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(Tile a, Tile b)
    {
        if (a == null)
            Debug.Log("a null");
        if (b == null)
            Debug.Log("b null");
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance) * gc.cellSize;
        return MOVE_STRAIGHT_COST * remaining;
    }

    private Tile GetLowestTile(List<Tile> PathNodes)
    {
        Tile lowestCostNode = PathNodes[0];
        for (int i = 0; i < PathNodes.Count; i++)
        {
            if(PathNodes[i].f < lowestCostNode.f)
            {
                lowestCostNode = PathNodes[i];
            }
        }
        return lowestCostNode;
    }
}
