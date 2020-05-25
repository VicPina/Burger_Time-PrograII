using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientParentController : MonoBehaviour
{
    private bool fall;
    int setNo;
    public bool[] parts;

    void Awake()
    {
        parts = new bool[4];

        for (int i = 0; i < 4; ++i) { parts[i] = GetComponentInChildren<IngredientController>().set; }

        fall = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!fall) { CheckFall(); }
        else if (fall) { ChangeFloor(); }
    }

    private void CheckFall()
    {
        for (int i =0; i < 4; ++i) { if (parts[i]) { setNo++; } }
        if (setNo == 4) { fall = true; }
    }

    private void ChangeFloor()
    {

    }
}
