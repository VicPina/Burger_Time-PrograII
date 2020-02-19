using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    public bool onStair;
    public float speed = 10.0f;
    public float boundX = 10.0f;
    public float reloadTime = 1.0f;
    private float lastTimeShot = 0f;


    public GameObject saltPrefab;

    public GameObject deathPrefab;

    private Rigidbody2D rigidBody;

    void OnTriggerEnter2D(Collider2D other)
    {
        //Check if the player collides with any stair
        if (other.tag == "Stairs") { onStair = true; }
    }


    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Get the new position of our character
        var x = transform.position.x + Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        var y = transform.position.y + Input.GetAxis("Vertical") * Time.deltaTime * speed;

        //Clamp along x-value according to boundX variable
        x = Mathf.Clamp(x, -boundX, boundX);

        //Set the position of our character through the RigidBody2D component (since we are using physics)
        rigidBody.MovePosition(new Vector2(x, transform.position.y));
        if (onStair) { Debug.Log("Subiendo"); }
        rigidBody.MovePosition(new Vector2(x, y));

        //Check if the player has fired
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1"))
        {
            //Check if the player can shoot since last time the spaceship has fired
            if (Time.time - lastTimeShot > reloadTime)
            {
                //Set the current time as the last time the spaceship has fired
                lastTimeShot = Time.time;

                //Create the bullet
                Instantiate(saltPrefab, transform.position, Quaternion.identity);
            }
        }

        //DEBUG CODE: simulates the Hit() function when the player presses the T key
        if (Input.GetKeyDown(KeyCode.T))
        {
            Hit(transform.position);
        }

    }

    public void Hit(Vector3 hitCoordinates)
    {
        //Create an explosion on the coordinates of the hit.
        Instantiate(deathPrefab, hitCoordinates, Quaternion.identity);

        //Remove a life
       // FindObjectOfType<LivesCounter>().RemoveLife();
    }
}