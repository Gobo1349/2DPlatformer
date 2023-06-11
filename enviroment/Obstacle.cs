// Скрипт Obstacle - препятствие
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Unit
{
    // тольк оодна проверка - касание триггера 
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>(); // проверяем, не юнит ли это (проверка на наличие компонента)
        if (unit && unit is Character)
            unit.ReceiveDamage(); // ГГ получает урон 
    }
}
