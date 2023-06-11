//Скрипт MovingPlatform_Y - двигающаяся по вертикали платформа
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform_Y : MovingPlatform
{
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > pos2.transform.position.y) // если дошли до поворота
        { movingRight = false; } // поворачиваем
        if (transform.position.y < pos1.transform.position.y) // если не дошли до поворота
        { movingRight = true; } // просто движемся 
        if (movingRight) // если идем вправо - движение
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime); // СКОРОСТЬ + значит вверх
        }
        else // если повернули - движение
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime); // скорость - значит вниз
        }
    }
}
