// Скрипт MoveableMonster - двигающийся противник
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 
using System.Runtime.InteropServices;

public class MoveableMonster : Monster // двигающийся противник
{
    [SerializeField]
    public Transform pos1;
    [SerializeField]
    public Transform pos2; // движение между двумя точками
    [SerializeField]
    float speed; // скорость

    public bool movingRight = true; // переменная для определения направления 

    public SpriteRenderer sprite; // нужно будет менять анимацию - 
    public Vector3 direction; // направление для бега

    protected override void Awake() // переопределяем метод 
    {
        sprite = GetComponentInChildren<SpriteRenderer>(); // получаем ссылку  на изображение
    }

    protected override void Update() // нужно вызвать движение 
    {
        if (transform.position.x > pos2.transform.position.x) // если дошли до поворота
        {
            movingRight = false;
            Turn();
        } // поворачиваем
        if (transform.position.x < pos1.transform.position.x) // если не дошли до поворота
        {
            movingRight = true;
            Turn();
        } // просто идем
        if (movingRight) // если идем вправо
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y); // СКОРОСТЬ + значит вправо
        }
        else // если повернули
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y); // скорость - значит влево
        }
    }

    void Turn() // поворот
    {
        direction *= -1;
        sprite.flipX = direction.x < 0.0f; // поворот изображения 
    }
    protected override void Start() // зададим направление по умолчанию
    {
        Explosion = Resources.Load("Explosion_blue"); // эффект при уничтожении 
       direction = transform.right;
        
    }
    protected override void OnTriggerEnter2D(Collider2D collider) // уничтожается прыжком сверху
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit is Character)
        { // нужно проверить, что удар сверху - для этого определим расст между монстром и игроком 
            if (Mathf.Abs(unit.transform.position.y - transform.position.y) > 1f)  // слева отр, справа + - нужно модуль
            {
                ReceiveDamage(); 
            } 
            else { unit.ReceiveDamage();
            } 

        }
    }
}

