// скрипт Bullet - снаряд персонажа
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour // скрипт, определяющий поведение пули
{
    private float speed = 10.0f;
    private Vector3 direction; // направление пули (вправо влево)
    public Vector3 Direction { set { direction = value; } } // направление пули задается извне (character)

    private GameObject parent; // стрелок - тоже юнит; это нужно, чтобы его пуля не сталкивалась с ним самим
    public GameObject Parent { set { parent = value; } get { return parent;  } }
    // ссылки на компоненты 
    private SpriteRenderer sprite; // нужна будет настройка цвета 

    public Color Color // открыт - будем использовать в shooting monster
    {
        set { sprite.color = value;  } // записываем значение цвета - для того, чтобы можно было задавать цвет
    }

    private void Awake() // получает ссылку 
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, 2f); // пуля пропадает через некоторое время 
    }

    private void Update() // движение пули 
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime); // откуда - куда - скорость*время между кадрами
    }

    private void OnTriggerEnter2D(Collider2D collider) // пуля уничтожается при столкновении 
    {
        Unit unit = collider.GetComponent<Unit>(); // юнит ли это?? 

        if (unit && unit.gameObject != parent) // при столкновении с чем-либо пуля уничтожается 
        {
             Destroy(gameObject);
        }
    }
}
