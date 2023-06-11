// Скрипт Computer - объект, при взаимодействии с которым начинается тест
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI; // для работы с интерфейсом

public class Computer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider) // при взаимодействии коллайдеров
    {
        if (collider.tag.Equals("Player")) 
        {
            //Debug.Log("!!!");
            Pause_menu.atComputer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) // при взаимодействии коллайдеров
    {
        if (collider.tag.Equals("Player")) 
        {
            Pause_menu.atComputer = false;
        }
    }
}

