using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{

    /*
     * Setting the health left for the unit in a hp bar
     */

    Transform bar; 

    void Start()
    {
        bar = transform.Find("Bar"); // find bar sprite
    }

    // Math formula for setting the correct size of the players health bar
    // takes in the max hp a unit has and their current health. Then translates the range into a correct format (0-100% , rather than 0hp-137hp [as an example])
    public void SetSize(float max, float current)
    {
        float OldRange = 0f - max; // get your old range
        float NewRange = 0f - 1f; // new range 0f - 1f (0-100%)
        float NewValue = (((current - 0f) * NewRange) / OldRange) + 0f; // creates a new value to reprsent our current health in terms of (0%-100%)
        bar.localScale = new Vector3(NewValue, 1f); 
    }
}
