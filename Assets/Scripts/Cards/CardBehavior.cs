﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehavior : MonoBehaviour
{
    // Don't forget to create collider
    public int player;

    bool mouseIn = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script loaded");
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicked");
        }
        if (mouseIn == false)
        {
            Debug.Log("over");
            mouseIn = true;
        }
    }

    void OnMouseExit()
    {
        if (mouseIn == true)
        {
            Debug.Log("mouse exit");
            mouseIn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}