﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class SaltController : MonoBehaviour
{
    public float timeBeforeDestruction = 3.0f;
    private Rigidbody2D rigidBody;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();


        //Get the new position of our bullet
        var x = FindObjectOfType<PlayerController>().transform.position.x + 1;
        var y = transform.position.y;

        //Set the position of our bullet through the RigidBody2D component (since we are using physics)
        rigidBody.MovePosition(new Vector2(transform.position.x, y));
    

        //Destroy the bullet if it didn't hit anything after 10 seconds
        Destroy(gameObject, timeBeforeDestruction);
    }
}