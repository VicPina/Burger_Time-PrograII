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
    bool side = false;
    private Animator anim;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        //Check if the player collides with any stair
        if (other.tag == "Stairs") { onStair = true; }
        if (other.tag == "Enemy") { Debug.Log("You died"); }
    }

    private void Update()
    {
        
       
        if(Input.GetKeyDown(KeyCode.A)) { side = true; }
        else if (Input.GetKeyDown(KeyCode.D)) { side = true; }
        anim.SetBool("Side", side);

        //Get the new position of our character
        var x = transform.position.x + Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        
        if (onStair) 
        {         
            //var y = transform.position.y + Input.GetAxis("Vertical") * Time.deltaTime * speed;
            Debug.Log("Subiendo"); 
        }
        
        //Set the position of our character through the RigidBody2D component (since we are using physics)
        rigidBody.MovePosition(new Vector2(x, transform.position.y));
            //rigidBody.MovePosition(new Vector2(x, y));


        //Check if the player has fired
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1"))
        {
            //Check if the player can shoot since last time it threw salt
            if (Time.time - lastTimeShot > reloadTime)
            {
                //Set the current time as the last time the spaceship has fired
                lastTimeShot = Time.time;

                //Create the bullet
                Instantiate(saltPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    public void Death()
    {
        //Create an explosion on the coordinates of the hit.
        //Instantiate(deathPrefab, hitCoordinates, Quaternion.identity);

        //Remove a life
       // FindObjectOfType<LivesCounter>().RemoveLife();
    }
}