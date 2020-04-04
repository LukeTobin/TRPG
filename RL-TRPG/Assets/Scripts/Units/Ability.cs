using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [Header("Ability Details")]
    public string title;
    public string description;
    [Space]
    public Sprite sprite;
    [Space]
    public int cost;
    [Space]
    public bool usesTurn;
}
