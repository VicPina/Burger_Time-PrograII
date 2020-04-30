using System.Collections;
using System.Collections.Generic;
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
    bool side = false;
        
    private SpriteRenderer render;
    private Rigidbody2D rigidBody;

    public GameObject saltPrefab;

    // Use this for initialization
    public void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        render = GetComponentInChildren<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
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
        
       
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) { side = true; }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) { side = true; }
        anim.SetBool("Side", side);

        //Get the new position of our character
        var x = transform.position.x + Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var y = transform.position.y;

        if (onStair) { y = transform.position.y + Input.GetAxis("Vertical") * Time.deltaTime * speed; }
        
        //Set the position of our character through the RigidBody2D component (since we are using physics)
        rigidBody.MovePosition(new Vector2(x, y));

        

        //Check if the player has fired
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1"))
        {
            //Check if the player can shoot since last time it threw salt
            if (Time.time - lastTimeShot > reloadTime)
            {
                //Set the current time as the last time the salt was thrown
                lastTimeShot = Time.time;

                //Create the bullet
                Instantiate(saltPrefab, transform.position, Quaternion.identity);
                //saltPrefab.transform.position
            }
        }
    }

    public void Death()
    {
        //Create an explosion on the coordinates of the hit.
        //Instantiate(deathPrefab, hitCoordinates, Quaternion.identity);
        Debug.Log("You died");
        //Remove a life
       // FindObjectOfType<LivesCounter>().RemoveLife();
    }
}