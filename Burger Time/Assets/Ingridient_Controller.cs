using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Ingridient_Controller : MonoBehaviour
{
    private SpriteRenderer render;
    // Start is called before the first frame update
    void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
        }
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
