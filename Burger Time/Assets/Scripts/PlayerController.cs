using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    public bool onStair;
    public float speed = 10.0f;
    public float reloadTime = 1.0f;
    private float lastTimeShot = 0f;

    private Animator anim;
    public bool[] direction;
        
    private SpriteRenderer render;
    private Rigidbody2D rigidBody;

    public GameObject saltPrefab;

    // Use this for initialization
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        render = GetComponentInChildren<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();

        direction = new bool[2];   // Left/Right
                                   // Up/Down
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //Check if the player collides with any stair
        if (other.tag == "Stairs") 
        { 
            onStair = true;
            rigidBody.gravityScale = 0.0f;
        }
        //Check if  player hits an enemy
        if (other.tag == "Enemy") { Death(); }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        //Check if the player collides with any stair
        if (other.tag == "Stairs") 
        { 
            onStair = false; 
            rigidBody.gravityScale = 2.8f;
        }
    }

    private void Update()
    {
        Move();

        
    }

    public void Move()
    {
        Vector3 spriteFlip = transform.localScale;
        if (Input.GetAxis("Horizontal") == 0)
        {
            direction[1] = false;
            anim.SetBool("Side", false);
            anim.SetBool("Down", false);
            anim.SetBool("Up", false);
        
        }
        if (Input.GetAxis("Horizontal") < 0) 
        {
            direction[0] = true;
            anim.SetBool("Down", false);
            anim.SetBool("Up", false);
            anim.SetBool("Side", true); 
        }
        if (Input.GetAxis("Horizontal") > 0) 
        {
            direction[0] = false;
            spriteFlip.x *= -1;
            anim.SetBool("Down", false);
            anim.SetBool("Up", false);
            anim.SetBool("Side", true);
            transform.localScale = spriteFlip;
        }
        if (Input.GetAxis("Vertical") < 0) 
        {
            direction[1] = false;
            anim.SetBool("Side", false);
            anim.SetBool("Up", false);
            anim.SetBool("Down", true); 
        }
        if (Input.GetAxis("Vertical") > 0) 
        {
            direction[1] = true;
            anim.SetBool("Side", false);
            anim.SetBool("Down", false);
            anim.SetBool("Up", true); 
        }

        //Get the new position of our character
        var x = transform.position.x + Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var y = transform.position.y;

        if (onStair) { y = transform.position.y + Input.GetAxis("Vertical") * Time.deltaTime * speed; }

        //Set the position of our character through the RigidBody2D component (since we are using physics)
        rigidBody.MovePosition(new Vector2(x, y));

        //Check if the player has fired
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1")) { Salting(); }
    }
    public void Salting()
    {  
        //Check if the player can shoot since last time it threw salt
        if (Time.time - lastTimeShot > reloadTime)
        {
            //Set the current time as the last time the salt was thrown
            lastTimeShot = Time.time;
           
            //Create the bullet
            Instantiate(saltPrefab, transform.position, Quaternion.identity);
        }
    }
    public void Death()
    {
        SceneManager.LoadScene("Game Over");
    }
}