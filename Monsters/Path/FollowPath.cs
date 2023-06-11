//Скрипт FollowPath - движение по траектории
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public MovingPath MyPath; // по какому пути двигаться 
    public float speed = 1f;
    public float maxDistance = 0.1f; // на какое расст нужно подойти к точке, чтобы понять что это точка

    public SpriteRenderer sprite; // нужно будет менять анимацию - 

    public IEnumerator<Transform> PointInPath; // проверяем точки 

    void Awake() // переопределяем метод 
    {
        sprite = GetComponentInChildren<SpriteRenderer>(); // получаем ссылку  на изображение
    }

    void Start()
    {
        if (MyPath == null)
        {
            return; // нет пути - ничего не возвращаем
        }
        PointInPath = MyPath.GetNextPathPoint(); // обращение к корутину GetNextPathPoint
        PointInPath.MoveNext(); // получение следующей точки в пути - через интерфейс

        if (PointInPath.Current == null) // на который указывает указатель (метод интерфейса)
        {
            return; // нет пути - ничего не возвращаем
        } // если некуда двигаться - нчего не делаем 

        transform.position = PointInPath.Current.position; // встаем на начальную точку пути 
    }

    void Update()
    {
        if (PointInPath == null || PointInPath.Current == null)
        {
            return; // нет пути - ничего не возвращаем
        } //аналогичная проверка (если пути нет)

        transform.position = Vector3.MoveTowards(transform.position, PointInPath.Current.position, Time.deltaTime * speed); // непосредственно движение 
        var distanceSqure = (transform.position - PointInPath.Current.position).sqrMagnitude; // проверим, достаточно ли мы близко к точке, чтобы начать двигаться к следующей 
        if (distanceSqure < maxDistance * maxDistance) // достаточно ли мы близко по теореме Пифагора
        {
            PointInPath.MoveNext(); // двигаемся к следующей точке
            sprite.flipX = MyPath.Direction < 0.0f; // поворот изображения 
        }
    }
}
