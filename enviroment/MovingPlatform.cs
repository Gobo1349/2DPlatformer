// Скрипт MovingPlatform - передвигающаяся платформа
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class MovingPlatform : Block 
{
    [SerializeField]
    public float speed; // скорость

    [SerializeField]
    public bool movingRight = true; // переменная для определения направления  

    [SerializeField]
    public Transform pos1; // точка начала пути  
    [SerializeField]
    public Transform pos2; // точка конца пути - платформа движется между 2мя точками


    void Update()
    {
        if (transform.position.x > pos2.transform.position.x) // если дошли до поворота
        { movingRight = false; } // поворачиваем
        if (transform.position.x < pos1.transform.position.x) // если не дошли до поворота
        { movingRight = true; } // просто движемся 
        if (movingRight) // если идем вправо - движение
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y); // СКОРОСТЬ + значит вправо
        } else // если повернули - движение
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y); // скорость - значит влево
        }
    }
}
