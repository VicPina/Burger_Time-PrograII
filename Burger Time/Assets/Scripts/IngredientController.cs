using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class IngredientController : MonoBehaviour
{
    public bool hit, set;

    private SpriteRenderer render;

    // Awake is called at initialization of the object 
    private void Awake()
    {
        render = GetComponentInChildren<SpriteRenderer>();
        
        hit = false;
        set = false;
    }

    private void OnTriggerEnter2D(Collider2D other) { if (other.tag == "Player") { hit = true; } }

    // Update is called once per frame
    private void Update()
    {
        var x = transform.position.x;
        var y = transform.position.y;
        var z = transform.position.z;
        if (!set) { if (hit) { y -= 0.125f; set = true; } }
        gameObject.transform.position = new Vector3(x, y, z);
    }
}
