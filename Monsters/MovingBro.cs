//Скрипт MovingBro - противник
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBro : Monster
{
    // Start is called before the first frame update
    protected override void Start()
    {
        Explosion = Resources.Load("Explosion_blue"); // эффект при уничтожении 
    }

    protected override void OnTriggerEnter2D(Collider2D collider) // уничтожается прыжком сверху
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit is Character)
        { // нужно проверить, что удар сверху - для этого определим расст между монстром и игроком 
            if (Mathf.Abs(unit.transform.position.y - transform.position.y) > 0.8f)  // слева отр, справа + - нужно модуль
            {
                ReceiveDamage();
                print(Mathf.Abs(unit.transform.position.y - transform.position.y));
            }
            else { unit.ReceiveDamage();
                print(Mathf.Abs(unit.transform.position.y - transform.position.y));
            }

        }
    }
}
