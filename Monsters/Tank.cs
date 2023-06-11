// Скрипт Tank - противник, которого нельзя уничтожить 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tank : Monster
{
    protected override void OnTriggerEnter2D(Collider2D collider) 
    {
        Unit unit = collider.GetComponent<Unit>(); // проверяем, не юнит ли это (проверка на наличие компонента)
        if (unit && unit is Character) // при столкновении с ГГ
            unit.ReceiveDamage(); // ГГ получает урон 
    }
}
