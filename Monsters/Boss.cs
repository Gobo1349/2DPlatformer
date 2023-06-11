//Скипт Boss - противник
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MoveableMonster
{
    [SerializeField]
    private float rate = 2.0F; // частота выстрелов 

    [SerializeField]
    private Bullet bullet; // ссылка на префаб (пулю)

    [SerializeField]
    private Color bulletColor;

    protected override void Start() // зададим направление по умолчанию
    {
        direction = transform.right;
        InvokeRepeating("Shoot", rate, rate); // метод, нач задержка, задержка перед каждым вызовом  - вызываем метод и связываем со временем 
    }

    private void Shoot() // стрельба 
    {
        Vector3 position = transform.position; position.y += 1f; // откуда стреляем

        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet; // создание копии пули 
        newBullet.Parent = gameObject; // стрелять будет текущий объект 
        newBullet.Direction = direction; // направление - влево
        newBullet.Color = bulletColor; // меняем цвет
    }
}
