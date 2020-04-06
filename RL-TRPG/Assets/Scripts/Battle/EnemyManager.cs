using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    TeamHandler team;
    GameController gc;

    private void Start()
    {
        team = GetComponent<TeamHandler>();
        gc = FindObjectOfType<GameController>();
    }

    public void ControlEnemies()
    {
        List<Unit> enemies = new List<Unit>();
        List<Unit> targets = new List<Unit>();
        int radius = 0;

        for (int i = 0; i < team.children.Count; i++)
        {
            enemies.Add(team.children[i]);
        }

        foreach (Unit unit in enemies)
        {
            if (!unit.hasMoved)
            {
                targets.Clear();
                while (targets.Count == 0)
                {
                    radius++;
                    Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, radius);

                    if (col != null)
                    {
                        for (int i = 0; i < col.Length; i++)
                        {
                            if (col[i].tag == "Unit")
                            {
                                if (col[i].GetComponent<Unit>().playerNumber == 1)
                                {
                                    targets.Add(col[i].GetComponent<Unit>());
                                    break;
                                }
                            }
                        }
                    }
                }

                // check range
                float range = 0;
                range += Mathf.Abs(targets[0].transform.position.x - unit.transform.position.x);
                range += Mathf.Abs(targets[0].transform.position.y - unit.transform.position.y);
                range = range / 2;
                Debug.Log("range from unit: " + range);

                gc.selectedUnit = unit;

                if (range < unit.range + unit.moveSpeed)
                {
                    // movement command
                    if (unit.CanMove(new Vector2(targets[0].transform.position.x + 2, targets[0].transform.position.y + 2)))
                    {
                        Debug.Log("can move");
                        unit.Move(new Vector2(targets[0].transform.position.x + 2, targets[0].transform.position.y + 2));
                        unit.Attack(targets[0]);
                    }
                    else if (unit.CanMove(new Vector2(targets[0].transform.position.x - 2, targets[0].transform.position.y + 2)))
                    {
                        Debug.Log("can move");
                        unit.Move(new Vector2(targets[0].transform.position.x - 2, targets[0].transform.position.y + 2));
                        unit.Attack(targets[0]);
                    }
                    else if (unit.CanMove(new Vector2(targets[0].transform.position.x + 2, targets[0].transform.position.y - 2)))
                    {
                        Debug.Log("can move");
                        unit.Move(new Vector2(targets[0].transform.position.x + 2, targets[0].transform.position.y - 2));
                        unit.Attack(targets[0]);
                    }
                    else if (unit.CanMove(new Vector2(targets[0].transform.position.x - 2, targets[0].transform.position.y - 2)))
                    {
                        Debug.Log("can move");
                        unit.Move(new Vector2(targets[0].transform.position.x - 2, targets[0].transform.position.y - 2));
                        unit.Attack(targets[0]);
                    }

                }
                else
                {
                    bool left = false;
                    bool right = false;
                    bool up = false;
                    bool down = false;

                    if (targets[0].transform.position.x > unit.transform.position.x)
                    {
                        right = true;
                    }
                    if (targets[0].transform.position.x < unit.transform.position.x)
                    {
                        left = true;
                    }
                    if (targets[0].transform.position.y > unit.transform.position.y)
                    {
                        up = true;
                    }
                    if (targets[0].transform.position.y < unit.transform.position.y)
                    {
                        down = true;
                    }

                    if (left && up && !down && !right)
                    {
                        unit.Move(new Vector2(unit.transform.position.x - 2f, unit.transform.position.y + 2f));
                        /*
                        if (unit.CanMove(new Vector2(unit.transform.position.x - 2f, unit.transform.position.y + 2f)))
                        {
                            Debug.Log("can move");
                            unit.Move(new Vector2(unit.transform.position.x - 2f, unit.transform.position.y + 2f));
                            break;
                        }
                        else
                        {
                            unit.hasMoved = true;
                            unit.hasAttacked = true;
                            unit.fade(true);
                        }*/
                    }
                    else if (left && down && !right && !up)
                    {
                        if (unit.CanMove(new Vector2(unit.transform.position.x - 2f, unit.transform.position.y - 2f)))
                        {
                            Debug.Log("can move");
                            unit.Move(new Vector2(unit.transform.position.x - 2, unit.transform.position.y - 2));
                            break;
                        }
                        else
                        {
                            unit.hasMoved = true;
                            unit.hasAttacked = true;
                            unit.fade(true);
                        }
                    }
                    else if (right && up && !down && !left)
                    {
                        if (unit.CanMove(new Vector2(unit.transform.position.x + 2f, unit.transform.position.y + 2f)))
                        {
                            Debug.Log("can move");
                            unit.Move(new Vector2(unit.transform.position.x + 2, unit.transform.position.y + 2));
                            break;
                        }
                        else
                        {
                            unit.hasMoved = true;
                            unit.hasAttacked = true;
                            unit.fade(true);
                        }
                    }
                    else if (right && down && !up && !left)
                    {
                        if (unit.CanMove(new Vector2(unit.transform.position.x + 2f, unit.transform.position.y - 2f)))
                        {
                            Debug.Log("can move");
                              unit.Move(new Vector2(unit.transform.position.x + 2, unit.transform.position.y + 2));
                            break;
                        }
                        else
                        {
                            unit.hasMoved = true;
                            unit.hasAttacked = true;
                            unit.fade(true);
                        }
                    }
                    else if (left && !down && !right && !up)
                    {
                        if (unit.CanMove(new Vector2(unit.transform.position.x - 2f, unit.transform.position.y)))
                        {
                            Debug.Log("can move");
                            unit.Move(new Vector2(unit.transform.position.x - 2, unit.transform.position.y));
                            break;
                        }
                        else
                        {
                            unit.hasMoved = true;
                            unit.hasAttacked = true;
                            unit.fade(true);
                        }
                    }
                    else if (right && !down && !left && !up)
                    {
                        if (unit.CanMove(new Vector2(unit.transform.position.x + 2f, unit.transform.position.y)))
                        {
                            Debug.Log("can move");
                            unit.Move(new Vector2(unit.transform.position.x + 2, unit.transform.position.y));
                            break;
                        }
                        else
                        {
                            unit.hasMoved = true;
                            unit.hasAttacked = true;
                            unit.fade(true);
                        }
                    }
                    else if (up && !down && !right && !left)
                    {
                        if (unit.CanMove(new Vector2(unit.transform.position.x, unit.transform.position.y + 2f)))
                        {
                            Debug.Log("can move");
                            unit.Move(new Vector2(unit.transform.position.x, unit.transform.position.y + 2));
                            break;
                        }
                        else
                        {
                            unit.hasMoved = true;
                            unit.hasAttacked = true;
                            unit.fade(true);
                        }
                    }
                    else if (down && !left && !right && !up)
                    {
                        if (unit.CanMove(new Vector2(unit.transform.position.x, unit.transform.position.y - 2f)))
                        {
                            Debug.Log("can move");
                            unit.Move(new Vector2(unit.transform.position.x, unit.transform.position.y - 2));
                            break;
                        }
                        else
                        {
                            unit.hasMoved = true;
                            unit.hasAttacked = true;
                            unit.fade(true);
                        }
                    }
                }

            }
        }

    }
}
