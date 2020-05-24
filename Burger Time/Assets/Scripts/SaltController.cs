using System.Collections;
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
        var x = FindObjectOfType<PlayerController>().transform.position.x;
        var y = FindObjectOfType<PlayerController>().transform.position.y;

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) { y -= 0.5f; }
        else if (Input.GetAxis("Vertical") < 0)   { y -= 0.5f; }
        else if (Input.GetAxis("Vertical") > 0)   { y += 0.5f; }
        else if (Input.GetAxis("Horizontal") > 0) { x += 0.5f; }
        else if (Input.GetAxis("Horizontal") < 0) { x -= 0.5f; }


        //Set the position of our bullet through the RigidBody2D component (since we are using physics)
        rigidBody.MovePosition(new Vector2(x, y));

        //Destroy the bullet if it didn't hit anything after 10 seconds
        Destroy(gameObject, timeBeforeDestruction);
    }
}