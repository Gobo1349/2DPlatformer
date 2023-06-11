// Скрипт ShootingMonster - противник, имеющий возможность стрелять
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMonster : Monster
{
    [SerializeField]
    private float rate = 2.0F; // частота выстрелов 

    [SerializeField]
    private Bullet bullet; // ссылка на префаб (пулю)

    [SerializeField]
    private Color bulletColor = Color.white;

    protected override void Awake()
    {
    }

    protected override void Start()
    {
        Explosion = Resources.Load("Explosion_green"); // эффект при разрушении 
        InvokeRepeating("Shoot", rate, rate); // метод, нач задержка, задержка перед каждым вызовом  - вызываем метод и связываем со временем 
    }
    private void Shoot() // стрельба 
    {
        Vector3 position = transform.position; position.y += 1f; // откуда стреляем

        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet; // создание копии пули 
        newBullet.Parent = gameObject; // стрелять будет текущий объект 
        newBullet.Direction = - newBullet.transform.right; // направление - влево
        newBullet.Color = bulletColor; // меняем цвет
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        // чтобы растение не убило само себя - переопределяем метод
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit is Character)
        { // нужно проверить, что удар сверху - для этого определим расст между монстром и игроком 
            if (Mathf.Abs(unit.transform.position.y - transform.position.y) > 0.8F) { ReceiveDamage(); } // слева отр, справа + - нужно модуль
            else { unit.ReceiveDamage(); } // аналогично moveablemonster

        } 
    }
}
