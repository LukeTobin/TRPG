using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    SpriteRenderer sr;
    public LayerMask obstacleLayer;

    public Color walkColor;
    public Color rangeColor;
    public Color attackable;
    public bool isWalkable;
    GameController gc;

    [Header("Pathfinding")]
    public int x;
    public int y;
    [Space]
    public int g;
    public int h;
    public int f;
    [Space]
    public Tile originNode;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gc = FindObjectOfType<GameController>();

       // x = (int)transform.position.x;
       // y = (int)transform.position.y;

        sr.color = new Color(1f, 1f, 1f, 1f);
    }

    public void OnMouseEnter()
    {
       // sr.color = new Color(1f, 1f, 1f, .7f);
    }

    public void OnMouseExit()
    {
       // sr.color = new Color(1f, 1f, 1f, 0f);
    }

    // check if the tile is clear (no obstacle or unit on it)
    public bool IsClear(Vector2 coord, int range, bool allyInteract)
    {
        // create a collider circle to check if anything is in the tile
        Collider2D obstacle = Physics2D.OverlapCircle(transform.position, 0.4f, obstacleLayer);
        if (obstacle != null)
        {
            // check if the found unit or obstacle is within range of the unit
            if (Mathf.Abs(transform.position.x - coord.x) + Mathf.Abs(transform.position.y - coord.y) <= range)
            {
                if (obstacle.CompareTag("Unit"))
                {
                    // check if what was found is a unit & if so color the tile
                    if (gc.playerTurn == 2 && obstacle.GetComponent<Unit>().playerNumber == 1)
                    {
                        if (this != obstacle)
                        {
                            sr.color = attackable;
                        }
                    }
                    else if (gc.playerTurn != 2 && obstacle.GetComponent<Unit>().playerNumber == 2)
                    {
                        if (this != obstacle)
                        {
                            sr.color = attackable;
                        }
                    }
                }
            }
            else
            {
                sr.color = rangeColor; // if its empty, color it with range color 
            }
            return false;
        }
        else
        {
            return true;
        }
    }

    // set a tile as walkable & color it
    public void Highlight()
    {
        sr.color = (walkColor);
        isWalkable = true;
    }

    // range color
    public void ShowRange()
    {
        sr.color = rangeColor;
    }

    //  reset a tiles color & is walkable state
    public void Reset()
    {
        sr.color = new Color(1f, 1f, 1f, 1f);
        isWalkable = false;
    }

    // clicking a tile moves the currently selectedunit to it, if its within range.
    private void OnMouseDown()
    {
        if (isWalkable && gc.selectedUnit != null)
        {
            gc.selectedUnit.Move(this.transform.position);
        }
    }

    public void CalculateF()
    {
        f = g + h;
    }
}
