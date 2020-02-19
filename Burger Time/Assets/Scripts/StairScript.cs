using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairScript : MonoBehaviour
{
    public float speed = 100.0f;
    void OnTriggerStay2D(Collider2D other)
    {
        //Check if the player collides with any stair
        if (other.tag == "Player" && Input.GetKey(KeyCode.W)) { 
           var y = transform.position.y + Input.GetAxis("Vertical") * Time.deltaTime * speed;
            other.GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x, y)); 
            Debug.Log("Subiendo");

        }
    }

}
