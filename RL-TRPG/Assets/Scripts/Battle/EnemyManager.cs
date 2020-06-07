using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    TeamHandler team;
    GameController gc;

    List<Unit> enemies = new List<Unit>();

    private void Start()
    {
        team = GetComponent<TeamHandler>();
        gc = FindObjectOfType<GameController>();
    }

    /// <summary>
    /// Make the movements for each enemy unit
    /// </summary>
    /// <param name="enemy">enemy units</param>
    /// <param name="targets">List of player units</param>
    public void ControlEnemies(Unit enemy, List<Unit> targets)
    {
        // unit is selected
        enemy.selected = true;
        gc.selectedUnit = enemy;
        gc.selected = true;

        _ControlEnemies(enemy, FindTarget(enemy, targets));

    }

    /// <summary>
    /// Gets the closest unit to enemy unit
    /// </summary>
    /// <param name="enemy">Enemy unit to check for</param>
    /// <param name="targets">List of targets to check</param>
    /// <returns></returns>
    Unit FindTarget(Unit enemy, List<Unit> targets)
    {
        int val = 999;
        Unit targetUnit = null;
        for (int j = 0; j < targets.Count; j++)
        {
            float check = Mathf.Abs((targets[j].transform.position.x - enemy.transform.position.x)) + Mathf.Abs((targets[j].transform.position.x - enemy.transform.position.x));
            int newVal = Mathf.RoundToInt(check);
            if (newVal < val)
            {
                val = newVal;
                targetUnit = targets[j];
            }
        }
        return targetUnit;
    }


    public void _ControlEnemies(Unit enemy, Unit target)
    {
        // make sure the enemy can move.
        if (!enemy.hasMoved && target != null)
        {
            // distance from target
            float distance = 0;
            distance += Mathf.Abs(target.transform.position.x - enemy.transform.position.x);
            distance += Mathf.Abs(target.transform.position.y - enemy.transform.position.y);
            distance = distance / 2;
            distance = Mathf.CeilToInt(distance);

            int cover = enemy.range + enemy.moveSpeed;
            cover = cover / 2;

            if (distance <= enemy.range)
            {
                // if the unit is in range just attack it 
                // needs refactoring when we add ranged units
                enemy.Attack(target, true);
            }
            else
            {
                if (distance <= cover)
                {
                    if (CanMoveToAttackRange(enemy, target) != new Vector2(99, 99))
                    {
                        enemy.Move(CanMoveToAttackRange(enemy, target));
                        enemy.Attack(target, true);
                    }
                    else
                    {
                        enemy.Move(FindOptimalPath(enemy, target, true), true);
                    }
                }
                else
                {
                    enemy.Move(FindOptimalPath(enemy, target, true), true);
                }
            }
        }

        #region Legacy
        /*
        if (!enemy.hasMoved)
        {
            // check distance to enemy : range = distance
            float range = 0;
            range += Mathf.Abs(target.transform.position.x - enemy.transform.position.x);
            range += Mathf.Abs(target.transform.position.y - enemy.transform.position.y);
            range = range / 2;
            range = Mathf.CeilToInt(range);
            Debug.Log("range from Unit: " + range);

            if (range <= enemy.range)
            {
                enemy.Attack(target);
            }
            else if (range <= enemy.range + enemy.moveSpeed)
            {
                if (enemy.CanMove(new Vector2(target.transform.position.x + 2, target.transform.position.y)))
                {
                    enemy.Move(new Vector2(target.transform.position.x + 2, target.transform.position.y));
                    enemy.Attack(target);
                }
                else if (enemy.CanMove(new Vector2(target.transform.position.x - 2, target.transform.position.y)))
                {
                    enemy.Move(new Vector2(target.transform.position.x - 2, target.transform.position.y));
                    enemy.Attack(target);
                }
                else if (enemy.CanMove(new Vector2(target.transform.position.x, target.transform.position.y + 2)))
                {
                    enemy.Move(new Vector2(target.transform.position.x, target.transform.position.y + 2));
                    enemy.Attack(target);
                }
                else if (enemy.CanMove(new Vector2(target.transform.position.x, target.transform.position.y - 2)))
                {
                    enemy.Move(new Vector2(target.transform.position.x, target.transform.position.y - 2));
                    enemy.Attack(target);
                }
                else if (enemy.CanMove(new Vector2(target.transform.position.x + 2, target.transform.position.y + 2)))
                {
                    enemy.Move(new Vector2(target.transform.position.x + 2, target.transform.position.y + 2));
                    enemy.Attack(target);
                }
                else if (enemy.CanMove(new Vector2(target.transform.position.x - 2, target.transform.position.y + 2)))
                {
                    enemy.Move(new Vector2(target.transform.position.x - 2, target.transform.position.y + 2));
                    enemy.Attack(target);
                }
                else if (enemy.CanMove(new Vector2(target.transform.position.x + 2, target.transform.position.y - 2)))
                {
                    enemy.Move(new Vector2(target.transform.position.x + 2, target.transform.position.y - 2));
                    enemy.Attack(target);
                }
                else if (enemy.CanMove(new Vector2(target.transform.position.x - 2, target.transform.position.y - 2)))
                {
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
        */
        #endregion
    }

    /// <summary>
    /// Check neighbour cells of target to find a free position
    /// </summary>
    /// <param name="unit">Unit to check for</param>
    /// <param name="target">Target that we want the unit to be moving towards</param>
    /// <returns></returns>
    Vector2 CanMoveToAttackRange(Unit unit, Unit target)
    {
        switch (unit.range)
        {
            case 2:
                #region melee range
                if (unit.CanMove(new Vector2(target.transform.position.x + 2, target.transform.position.y)))
                {
                    return new Vector2(target.transform.position.x + 2, target.transform.position.y);
                }
                else if (unit.CanMove(new Vector2(target.transform.position.x - 2, target.transform.position.y)))
                {
                    return new Vector2(target.transform.position.x - 2, target.transform.position.y);
                }
                else if (unit.CanMove(new Vector2(target.transform.position.x, target.transform.position.y + 2)))
                {
                    return new Vector2(target.transform.position.x, target.transform.position.y + 2);
                }
                else if (unit.CanMove(new Vector2(target.transform.position.x, target.transform.position.y - 2)))
                {
                    return new Vector2(target.transform.position.x, target.transform.position.y - 2);
                }
                #endregion
                break;
            case 4:
                #region ranged range
                if (unit.CanMove(new Vector2(target.transform.position.x + 4, target.transform.position.y)))
                {
                    return new Vector2(target.transform.position.x + 4, target.transform.position.y);
                }
                else if (unit.CanMove(new Vector2(target.transform.position.x + 4, target.transform.position.y)))
                {
                    return new Vector2(target.transform.position.x + 4, target.transform.position.y);
                }
                else if (unit.CanMove(new Vector2(target.transform.position.x, target.transform.position.y + 4)))
                {
                    return new Vector2(target.transform.position.x, target.transform.position.y + 4);
                }
                else if (unit.CanMove(new Vector2(target.transform.position.x, target.transform.position.y - 4)))
                {
                    return new Vector2(target.transform.position.x, target.transform.position.y - 4);
                }
                else if (unit.CanMove(new Vector2(target.transform.position.x + 2, target.transform.position.y + 2)))
                {
                    return new Vector2(target.transform.position.x + 2, target.transform.position.y + 2);
                }
                else if (unit.CanMove(new Vector2(target.transform.position.x - 2, target.transform.position.y + 2)))
                {
                    return new Vector2(target.transform.position.x - 2, target.transform.position.y + 2);
                }
                else if (unit.CanMove(new Vector2(target.transform.position.x + 2, target.transform.position.y - 2)))
                {
                    return new Vector2(target.transform.position.x + 2, target.transform.position.y - 2);
                }
                else if (unit.CanMove(new Vector2(target.transform.position.x - 2, target.transform.position.y - 2)))
                {
                    return new Vector2(target.transform.position.x - 2, target.transform.position.y - 2);
                }
                if (unit.CanMove(new Vector2(target.transform.position.x + 2, target.transform.position.y)))
                {
                    return new Vector2(target.transform.position.x + 2, target.transform.position.y);
                }
                else if (unit.CanMove(new Vector2(target.transform.position.x - 2, target.transform.position.y)))
                {
                    return new Vector2(target.transform.position.x - 2, target.transform.position.y);
                }
                else if (unit.CanMove(new Vector2(target.transform.position.x, target.transform.position.y + 2)))
                {
                    return new Vector2(target.transform.position.x, target.transform.position.y + 2);
                }
                else if (unit.CanMove(new Vector2(target.transform.position.x, target.transform.position.y - 2)))
                {
                    return new Vector2(target.transform.position.x, target.transform.position.y - 2);
                }
                #endregion
                break;
            default:
                return new Vector2(99, 99);
        }
        return new Vector2(99, 99);
    }

    /// <summary>
    /// Returns a vector2 coord for the most optimal position to move towards, based on rather the unit is in range or not
    /// </summary>
    /// <param name="unit">The unit to move</param>
    /// <param name="target">The closet target</param>
    /// <param name="inRange">if the unit is in range of that target</param>
    /// <returns></returns>
    Vector2 FindOptimalPath(Unit unit, Unit target, bool inRange = false)
    {
        // get tiles you can move too
        List<Tile> tiles = unit.ReturnWalkableTiles();
      
        // get current distance
        float distance = 0;
        distance += Mathf.Abs(target.transform.position.x - unit.transform.position.x);
        distance += Mathf.Abs(target.transform.position.y - unit.transform.position.y);
        distance = distance / 2;
        distance = Mathf.CeilToInt(distance);

        //create base tile
        Tile location = gc.map.GetTile((int)unit.transform.position.x, (int)unit.transform.position.y);

        //use list of tiles to find closest
        for (int j = 0; j < tiles.Count; j++)
        {
            float newDistance = 0;
            newDistance += Mathf.Abs(target.transform.position.x - tiles[j].x);
            newDistance += Mathf.Abs(target.transform.position.y - tiles[j].y);
            newDistance = newDistance / 2;

            newDistance = Mathf.CeilToInt(newDistance);

            if (newDistance < distance)
            {
                distance = newDistance;
                location = tiles[j];
            }
        }

        if (!inRange)
            return new Vector2(location.x, location.y);
        else
            return new Vector2(location.x, location.y);

    }
}
