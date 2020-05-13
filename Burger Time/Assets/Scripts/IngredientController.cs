using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class IngredientController : MonoBehaviour
{
    private bool hit, set;

    private SpriteRenderer render;
    private Rigidbody2D rigidBody;

    // Awake is called at initialization of the object 
    void Awake()
    {
        render = GetComponentInChildren<SpriteRenderer>();
        rigidBody = GetComponentInChildren<Rigidbody2D>();
        
        hit = false;
        set = false;
    }

    public void OnTriggerEnter2D(Collider2D other) { if (other.tag == "Player") { hit = true; } }

    // Update is called once per frame
    void Update()
    {
        var x = transform.position.x;
        var y = transform.position.y;
        if (!set) { if (hit) { y -= 1f; set = true; } }
        rigidBody.MovePosition(new Vector2(x, y));
    }
}
