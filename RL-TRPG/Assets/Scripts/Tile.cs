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

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gc = FindObjectOfType<GameController>();

        sr.color = new Color(1f, 1f, 1f, 0f);
    }

    public void OnMouseEnter()
    {
       // sr.color = new Color(1f, 1f, 1f, .7f);
    }

    public void OnMouseExit()
    {
       // sr.color = new Color(1f, 1f, 1f, 0f);
    }

    
    public bool IsClear(Vector2 coord, int range, bool allyInteract)
    {
        Collider2D obstacle = Physics2D.OverlapCircle(transform.position, 0.4f, obstacleLayer);
        if (obstacle != null)
        {
            if (Mathf.Abs(transform.position.x - coord.x) + Mathf.Abs(transform.position.y - coord.y) <= range)
            {
                if (obstacle.CompareTag("Unit"))
                {
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
                sr.color = rangeColor;
            }
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Highlight()
    {
        sr.color = (walkColor);
        isWalkable = true;
    }

    public void ShowRange()
    {
        sr.color = rangeColor;
    }

    public void Reset()
    {
        sr.color = new Color(1f, 1f, 1f, 0f);
        isWalkable = false;
    }

    private void OnMouseDown()
    {
        if (isWalkable && gc.selectedUnit != null)
        {
            gc.selectedUnit.Move(this.transform.position);
        }
    }
}
