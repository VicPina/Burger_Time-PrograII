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
        //Destroy the bullet if it didn't hit anything after 10 seconds
        Destroy(gameObject, timeBeforeDestruction);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            //Send the message to the enemy that the spaceship has been hit
            other.GetComponent<EnemyController>().Salted(transform.position);
        }
    }
}