//Скрипт Press - двигающееся препятствие
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press : Obstacle
{
    [SerializeField]
    public float speed; // скорость

    [SerializeField]
    bool movingUp = true; // переменная для определения направления  

    [SerializeField]
    public Transform pos1; // точка начала движения  
    [SerializeField]
    public Transform pos2; // точка конца 


    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > pos2.transform.position.y) // если дошли до поворота
        { movingUp = false; } // поворачиваем
        if (transform.position.y < pos1.transform.position.y) // если не дошли до поворота
        { movingUp = true; } // просто идем
        if (movingUp) // если движемся вверх
        {            
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime); 
        }
        else // если повернули вниз 
        {
            Invoke("Move", 2.0f); // начинаем движение вниз с задержкой 
        }
    }

    void Move() // движение вниз 
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime); 
    }
}
