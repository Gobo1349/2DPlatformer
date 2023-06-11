//Скрипт FlyingMonster - летающий противник 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FlyingMonster : Monster
{
    [SerializeField]
    public Transform leftdown;
    [SerializeField]
    public Transform rightup;
    [SerializeField]
    float speed; // скорость

    bool movingRight = true; // переменная для определения направления по горизонтали
    bool movingUp = true; // переменная для определения направления по вертикали

    private SpriteRenderer sprite; // нужно будет менять анимацию - 
    private Vector3 direction_x; // направление для движения по горизонтали
    private Vector3 direction_y; // направление для движения по вертикали

    protected override void Awake() // переопределяем метод 
    {
        sprite = GetComponentInChildren<SpriteRenderer>(); // получаем ссылку 
    }

    protected override void Update() // нужно вызвать движение 
    {
        // движение по горизонтали
        if (transform.position.x > rightup.transform.position.x) // если дошли до поворота
        {
            movingRight = false;
            Turn_x();
        } // поворачиваем
        if (transform.position.x < leftdown.transform.position.x) // если не дошли до поворота
        {
            movingRight = true;
            Turn_x();
        } // просто идем
        if (movingRight) // если идем вправо
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y); // СКОРОСТЬ + значит вправо
        }
        else // если повернули
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y); // скорость - значит влево
        }

        // движение по вертикали - при наложении двух типов движения получаем полет зигзагом
        if (transform.position.y > rightup.transform.position.y) // если дошли до поворота
        {
            movingUp = false;
            Turn_y();
        } // поворачиваем
        if (transform.position.y < leftdown.transform.position.y) // если не дошли до поворота
        {
            movingUp = true;
            Turn_y();
        } // просто идем
        if (movingUp) // если идем вправо
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime); // СКОРОСТЬ + значит вправо
        }
        else // если повернули
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);  // скорость - значит влево
        }
    }

    void Turn_x() // поворот по горизонтали
    {
        direction_x *= -1;
        sprite.flipX = direction_x.x < 0.0f; // должен поворачиваться - как персонаж - опытным путем
    }

    void Turn_y() // поворот по вертикали
    {
        direction_y *= -1;
        //   sprite.flipX = direction.x < 0.0f; // должен поворачиваться - как персонаж - опытным путем
    }
    protected override void Start() // зададим направление по умолчанию
    {
        Explosion = Resources.Load("Explosion_red");
        direction_x = transform.right;
        direction_y = transform.up;

    }
}
