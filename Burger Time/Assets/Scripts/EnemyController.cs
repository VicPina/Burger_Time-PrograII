using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Salt")
        {
            //Send the message to the enemy that the spaceship has been hit
            other.GetComponent<EnemyController>().Salted(transform.position);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Salted(Vector3 hitCoordinates)
    {
        Destroy(gameObject);
        //Create an explosion on the coordinates of the hit.
        //Instantiate(saltedPrefab, hitCoordinates, Quaternion.identity);
        Debug.Log("Stunned");
    }
}
