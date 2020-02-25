using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Salted(Vector3 hitCoordinates)
    {
        //Create an explosion on the coordinates of the hit.
        //Instantiate(saltedPrefab, hitCoordinates, Quaternion.identity);
        Debug.Log("Stunned");
    }
}
