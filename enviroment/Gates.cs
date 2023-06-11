//Скрипт Gates - дверь, открываемая ключом 
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Gates : Unit
{

    // Update is called once per frame
    void Update()
    {
        if (Key.destroy == true)
        {
            Destroy(gameObject);
        }
    }
}
