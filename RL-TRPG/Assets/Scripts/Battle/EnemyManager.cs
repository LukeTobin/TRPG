using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    TeamHandler team;
    GameController gc;

    List<Unit> enemies = new List<Unit>();

    int d = -1;

    private void Start()
    {
        team = GetComponent<TeamHandler>();
        gc = FindObjectOfType<GameController>();
    }

    /// <summary>
    /// Make the movements for each enemy unit
    /// </summary>
    /// <param name="enemies">List of enemy units</param>
    /// <param name="targets">List of player units</param>
    public void ControlEnemies(List<Unit> enemies, List<Unit> targets)
    {
        d = -1;

        for (int i = 0; i < enemies.Count; i++)
        {
            d++;

            // unit is selected
            enemies[d].selected = true;
            gc.selectedUnit = enemies[d];
            gc.selected = true;

            _ControlEnemies(enemies[d], FindTarget(enemies[i], targets));
        }

        gc.EndTurn();
    }

    /// <summary>
    /// Gets the closest unit to enemy unit
    /// </summary>
    /// <param name="enemy">Enemy unit to check for</param>
    /// <param name="targets">List of targets to check</param>
    /// <returns></returns>
    private Unit FindTarget(Unit enemy, List<Unit> targets)
    {
        int val = 999;
        for (int j = 0; j < targets.Count; j++)
        {
            if ((targets[j].transform.position.x - enemy.transform.position.x) + (targets[j].transform.position.x - enemy.transform.position.x) < val)
            {
                return targets[j];
            }
        }
        return null;
    }


    public void _ControlEnemies(Unit enemy, Unit target)
    {
        enemy.selected = true;
        gc.selectedUnit = enemy;

        if (!enemy.hasMoved)
        {
            // check range
            float range = 0;
            range += Mathf.Abs(target.transform.position.x - enemy.transform.position.x);
            range += Mathf.Abs(target.transform.position.y - enemy.transform.position.y);
            range = range / 2;
            range = Mathf.CeilToInt(range);
            Debug.Log("range from Unit: " + range);

            if (range < enemy.range + enemy.moveSpeed)
            {
                // movement command
                if (enemy.CanMove(new Vector2(target.transform.position.x + 2, target.transform.position.y + 2)))
                {
                    Debug.Log("can move");
                    enemy.Move(new Vector2(target.transform.position.x + 2, target.transform.position.y + 2));
                    enemy.Attack(target);
                }
                else if (enemy.CanMove(new Vector2(target.transform.position.x - 2, target.transform.position.y + 2)))
                {
                    Debug.Log("can move");
                    enemy.Move(new Vector2(target.transform.position.x - 2, target.transform.position.y + 2));
                    enemy.Attack(target);
                }
                else if (enemy.CanMove(new Vector2(target.transform.position.x + 2, target.transform.position.y - 2)))
                {
                    Debug.Log("can move");
                    enemy.Move(new Vector2(target.transform.position.x + 2, target.transform.position.y - 2));
                    enemy.Attack(target);
                }
                else if (enemy.CanMove(new Vector2(target.transform.position.x - 2, target.transform.position.y - 2)))
                {
                    Debug.Log("can move");
                    enemy.Move(new Vector2(target.transform.position.x - 2, target.transform.position.y - 2));
                    enemy.Attack(target);
                }
                else
                {
                    bool left = false;
                    bool right = false;
                    bool up = false;
                    bool down = false;

                    if (target.transform.position.x > enemy.transform.position.x)
                    {
                        right = true;
                    }
                    if (target.transform.position.x < enemy.transform.position.x)
                    {
                        left = true;
                    }
                    if (target.transform.position.y > enemy.transform.position.y)
                    {
                        up = true;
                    }
                    if (target.transform.position.y < enemy.transform.position.y)
                    {
                        down = true;
                    }

                    if (left && up && !down && !right)
                    {
                        //enemy.Move(new Vector2(enemy.transform.position.x - 2f, enemy.transform.position.y + 2f));

                        if (enemy.CanMove(new Vector2(enemy.transform.position.x - 2f, enemy.transform.position.y + 2f)))
                        {
                            Debug.Log("can move");
                            enemy.Move(new Vector2(enemy.transform.position.x - 2f, enemy.transform.position.y + 2f));
                        }
                        else
                        {
                            enemy.hasMoved = true;
                            enemy.hasAttacked = true;
                            enemy.fade(true);
                        }
                    }
                    else if (left && down && !right && !up)
                    {
                        if (enemy.CanMove(new Vector2(enemy.transform.position.x - 2f, enemy.transform.position.y - 2f)))
                        {
                            Debug.Log("can move");
                            enemy.Move(new Vector2(enemy.transform.position.x - 2, enemy.transform.position.y - 2));
                        }
                        else
                        {
                            enemy.hasMoved = true;
                            enemy.hasAttacked = true;
                            enemy.fade(true);
                        }
                    }
                    else if (right && up && !down && !left)
                    {
                        if (enemy.CanMove(new Vector2(enemy.transform.position.x + 2f, enemy.transform.position.y + 2f)))
                        {
                            Debug.Log("can move");
                            enemy.Move(new Vector2(enemy.transform.position.x + 2, enemy.transform.position.y + 2));
                        }
                        else
                        {
                            enemy.hasMoved = true;
                            enemy.hasAttacked = true;
                            enemy.fade(true);
                        }
                    }
                    else if (right && down && !up && !left)
                    {
                        if (enemy.CanMove(new Vector2(enemy.transform.position.x + 2f, enemy.transform.position.y - 2f)))
                        {
                            Debug.Log("can move");
                            enemy.Move(new Vector2(enemy.transform.position.x + 2, enemy.transform.position.y + 2));
                        }
                        else
                        {
                            enemy.hasMoved = true;
                            enemy.hasAttacked = true;
                            enemy.fade(true);
                        }
                    }
                    else if (left && !down && !right && !up)
                    {
                        if (enemy.CanMove(new Vector2(enemy.transform.position.x - 2f, enemy.transform.position.y)))
                        {
                            Debug.Log("can move");
                            enemy.Move(new Vector2(enemy.transform.position.x - 2, enemy.transform.position.y));
                        }
                        else
                        {
                            enemy.hasMoved = true;
                            enemy.hasAttacked = true;
                            enemy.fade(true);
                        }
                    }
                    else if (right && !down && !left && !up)
                    {
                        if (enemy.CanMove(new Vector2(enemy.transform.position.x + 2f, enemy.transform.position.y)))
                        {
                            Debug.Log("can move");
                            enemy.Move(new Vector2(enemy.transform.position.x + 2, enemy.transform.position.y));
                        }
                        else
                        {
                            enemy.hasMoved = true;
                            enemy.hasAttacked = true;
                            enemy.fade(true);
                        }
                    }
                    else if (up && !down && !right && !left)
                    {
                        if (enemy.CanMove(new Vector2(enemy.transform.position.x, enemy.transform.position.y + 2f)))
                        {
                            Debug.Log("can move");
                            enemy.Move(new Vector2(enemy.transform.position.x, enemy.transform.position.y + 2));
                        }
                        else
                        {
                            enemy.hasMoved = true;
                            enemy.hasAttacked = true;
                            enemy.fade(true);
                        }
                    }
                    else if (down && !left && !right && !up)
                    {
                        if (enemy.CanMove(new Vector2(enemy.transform.position.x, enemy.transform.position.y - 2f)))
                        {
                            Debug.Log("can move");
                            enemy.Move(new Vector2(enemy.transform.position.x, enemy.transform.position.y - 2));
                        }
                        else
                        {
                            enemy.hasMoved = true;
                            enemy.hasAttacked = true;
                            enemy.fade(true);
                        }
                    }
                }

            }
            else
            {
                bool left = false;
                bool right = false;
                bool up = false;
                bool down = false;

                if (target.transform.position.x > enemy.transform.position.x)
                {
                    right = true;
                }
                if (target.transform.position.x < enemy.transform.position.x)
                {
                    left = true;
                }
                if (target.transform.position.y > enemy.transform.position.y)
                {
                    up = true;
                }
                if (target.transform.position.y < enemy.transform.position.y)
                {
                    down = true;
                }

                if (left && up && !down && !right)
                {
                    //enemy.Move(new Vector2(enemy.transform.position.x - 2f, enemy.transform.position.y + 2f));

                    if (enemy.CanMove(new Vector2(enemy.transform.position.x - 2f, enemy.transform.position.y + 2f)))
                    {
                        Debug.Log("can move");
                        enemy.Move(new Vector2(enemy.transform.position.x - 2f, enemy.transform.position.y + 2f));
                    }
                    else
                    {
                        enemy.hasMoved = true;
                        enemy.hasAttacked = true;
                        enemy.fade(true);
                    }
                }
                else if (left && down && !right && !up)
                {
                    if (enemy.CanMove(new Vector2(enemy.transform.position.x - 2f, enemy.transform.position.y - 2f)))
                    {
                        Debug.Log("can move");
                        enemy.Move(new Vector2(enemy.transform.position.x - 2, enemy.transform.position.y - 2));
                    }
                    else
                    {
                        enemy.hasMoved = true;
                        enemy.hasAttacked = true;
                        enemy.fade(true);
                    }
                }
                else if (right && up && !down && !left)
                {
                    if (enemy.CanMove(new Vector2(enemy.transform.position.x + 2f, enemy.transform.position.y + 2f)))
                    {
                        Debug.Log("can move");
                        enemy.Move(new Vector2(enemy.transform.position.x + 2, enemy.transform.position.y + 2));
                    }
                    else
                    {
                        enemy.hasMoved = true;
                        enemy.hasAttacked = true;
                        enemy.fade(true);
                    }
                }
                else if (right && down && !up && !left)
                {
                    if (enemy.CanMove(new Vector2(enemy.transform.position.x + 2f, enemy.transform.position.y - 2f)))
                    {
                        Debug.Log("can move");
                        enemy.Move(new Vector2(enemy.transform.position.x + 2, enemy.transform.position.y + 2));
                    }
                    else
                    {
                        enemy.hasMoved = true;
                        enemy.hasAttacked = true;
                        enemy.fade(true);
                    }
                }
                else if (left && !down && !right && !up)
                {
                    if (enemy.CanMove(new Vector2(enemy.transform.position.x - 2f, enemy.transform.position.y)))
                    {
                        Debug.Log("can move");
                        enemy.Move(new Vector2(enemy.transform.position.x - 2, enemy.transform.position.y));
                    }
                    else
                    {
                        enemy.hasMoved = true;
                        enemy.hasAttacked = true;
                        enemy.fade(true);
                    }
                }
                else if (right && !down && !left && !up)
                {
                    if (enemy.CanMove(new Vector2(enemy.transform.position.x + 2f, enemy.transform.position.y)))
                    {
                        Debug.Log("can move");
                        enemy.Move(new Vector2(enemy.transform.position.x + 2, enemy.transform.position.y));
                    }
                    else
                    {
                        enemy.hasMoved = true;
                        enemy.hasAttacked = true;
                        enemy.fade(true);
                    }
                }
                else if (up && !down && !right && !left)
                {
                    if (enemy.CanMove(new Vector2(enemy.transform.position.x, enemy.transform.position.y + 2f)))
                    {
                        Debug.Log("can move");
                        enemy.Move(new Vector2(enemy.transform.position.x, enemy.transform.position.y + 2));
                    }
                    else
                    {
                        enemy.hasMoved = true;
                        enemy.hasAttacked = true;
                        enemy.fade(true);
                    }
                }
                else if (down && !left && !right && !up)
                {
                    if (enemy.CanMove(new Vector2(enemy.transform.position.x, enemy.transform.position.y - 2f)))
                    {
                        Debug.Log("can move");
                        enemy.Move(new Vector2(enemy.transform.position.x, enemy.transform.position.y - 2));
                    }
                    else
                    {
                        enemy.hasMoved = true;
                        enemy.hasAttacked = true;
                        enemy.fade(true);
                    }
                }
            }
        }
        else
        {
            enemy.selected = false;
            gc.selectedUnit = null;
        }
    }
}
