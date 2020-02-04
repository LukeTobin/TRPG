using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public bool selected;
    GameController gc;
    public SpriteRenderer sr;

    // Core Identity of unit
    [Header("Identity")]
    public string title;
    public int moveSpeed;
    public int range;

    // Base Stats
    [Header("Base Stats")]
    public int health;
    public int attackDamage; // both physical & magical
    public int armor;

    // Ability Info
    [Header("Ability Info")]
    public bool AllyInteractable;

    // Extra
    [Header("Extra's - Typically Left Untouched")]
    public bool hasMoved;
    public bool hasAttacked;
    public float transitionSpeed = 0.8f;

    [Header("1 = Player      2 = Enemy")]
    public int playerNumber;

    List<Unit> enemiesInRange = new List<Unit>();

    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
