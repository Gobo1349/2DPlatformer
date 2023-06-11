// скрипт Heart - объект - дополнительная жизнь
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class heart : MonoBehaviour // при нахождении объекта – сердца на уровне нам прибавляется жизнь
{
    private void OnTriggerEnter2D(Collider2D collider) // если ГГ подошел к объекту 
    {
        Character character = collider.GetComponent<Character>(); //проверяем, игрок ли это 

        if (character)
        {
            if (character.Lives < 5) // прибавляем жизнь
            {
                
                character.Lives++;
                print(character.Lives);
                Destroy(gameObject);
            }
        }
    }
}
