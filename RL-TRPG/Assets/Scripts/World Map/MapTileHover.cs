﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileHover : MonoBehaviour
{
    /*
     * When your mouse hovers over a tile with this script it changes the color of the sprite
     */

    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void OnMouseEnter()
    {
         sr.color = new Color(.4f, .8f, .4f, 1f);
    }

    public void OnMouseExit()
    {
         sr.color = new Color(1f, 1f, 1f, 0.5f);
    }
}
