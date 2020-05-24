using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EnemyController : MonoBehaviour
{
    private bool onStair, newFloor, walking, onSign, changeDirection;
    private bool[] direction;
    public float stuntTime = 3.0f;
    public float saltHit = 0f;
    public float speed = 0.2f;

    private Animator anim;

    private SpriteRenderer render;
    private Rigidbody2D rigidBody;

    private GameObject player;

    // Awake is called at initialization of the object 
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        render = GetComponentInChildren<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

        walking = true;
        direction = new bool[2];   // Left/Right
                                  // Up/Down
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the enemy enters an object
        if (other.tag == "Stairs") { onStair = true; }
        if (other.tag == "FloorSign") { onSign = true; }
        if (other.tag == "Salt") { Salted(); }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        //Check if the enemy leaves an object
        if (other.tag == "Stairs") { onStair = false; }
        if (other.tag == "FloorSign") { onSign = false; }
    }
    // Update is called once per frame
    private void Update()
    {
        // Check the player's position
        if (player.transform.position.y < transform.position.y) { direction[1] = false; }
        else if (player.transform.position.y > transform.position.y) { direction[1] = true; }
        if (player.transform.position.x < transform.position.x) { direction[0] = true; }
        else if (player.transform.position.x > transform.position.x) { direction[0] = false; }

        Move();
    }
    // Commands the enemy to move around the environment
    public void Move()
    {
        anim.SetBool("Salted", false);

        var x = transform.position.x;
        var y = transform.position.y;

        if (walking)
        {
            anim.SetBool("Walking", true);
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            //Get the new position of our character
            if (direction[0]) { x = transform.position.x - 0.5f * Time.deltaTime * speed; }
            else if (!direction[0]) { x = transform.position.x + 0.5f * Time.deltaTime * speed; }
            if (onStair && player.transform.position.y != transform.position.y)
            {
                walking = false;
                newFloor = true;
            }
        }
        else if (!walking)
        {
            anim.SetBool("Walking", false);
            if (direction[1]) 
            { 
                y = transform.position.y + 0.5f * Time.deltaTime * speed;
                anim.SetBool("Up", true);
            }
            else if (!direction[1]) 
            { 
                y = transform.position.y - 0.5f * Time.deltaTime * speed;
                anim.SetBool("Down", true);
            }
            if (onSign)
            {
                if (direction[0]) { x = transform.position.x - 0.5f * Time.deltaTime * speed; }
                else if (!direction[0]) { x = transform.position.x + 0.5f * Time.deltaTime * speed; }
                if (newFloor)
                {
                    walking = true;
                    newFloor = false;
                }
            }
        }

        rigidBody.MovePosition(new Vector2(x, y));
    }
    public void Salted()
    {        
           anim.SetBool("Salted", true);
           //Set the current time as the last time the salt hit the enemy
           saltHit = Time.time;

           var x = transform.position.x;
           var y = transform.position.y;
           rigidBody.MovePosition(new Vector2(x, y));        
    }
}
