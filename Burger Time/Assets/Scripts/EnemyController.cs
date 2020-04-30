using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EnemyController : MonoBehaviour
{
    public bool onStair;
    public float speed = 0.2f;

    private Animator anim;

    private SpriteRenderer render;
    private Rigidbody2D rigidBody;

    private GameObject player;

    // Awake is called at initialization of the object 
    public void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        render = GetComponentInChildren<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collides with any stair
        if (other.tag == "Stairs") { onStair = true; }
        // Check if the player collides with any stair
        if (other.tag == "Salt")
        {
            //Send the message to the enemy that they've been salted
            Salted();
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        //Check if the player collides with any stair
        if (other.tag == "Stairs") { onStair = false; }
    }
    // Update is called once per frame
    void Update()
    {
        var x = transform.position.x;
        var y = transform.position.y;
        //Get the new position of our character
        if (player.transform.position.x < transform.position.x) { x = transform.position.x - 0.5f * Time.deltaTime * speed; } 
        else if(player.transform.position.x > transform.position.x) { x = transform.position.x + 0.5f * Time.deltaTime * speed; }
        if (onStair)
        {
            if (player.transform.position.y != transform.position.y)
            {
                if (player.transform.position.y < transform.position.y) { y = transform.position.y - 0.5f * Time.deltaTime * speed; }
                else if (player.transform.position.y > transform.position.y) { y = transform.position.y + 0.5f * Time.deltaTime * speed; }
            }
        }
        //Set the position of our character through the RigidBody2D component (since we are using physics)
        rigidBody.MovePosition(new Vector2(x, y));
    }
    public void Salted()
    {
        Debug.Log("Stunned");
    }
}
