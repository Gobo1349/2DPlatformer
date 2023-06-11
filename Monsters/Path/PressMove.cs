//Скрипт PressMove - движение по траектории
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PressMove : FollowPath
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

        void Next()
    {
        print("aa");
        PointInPath.MoveNext();
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
            Next();
          //  Invoke("Next", 2.0f);
         //   sprite.flipX = MyPath.Direction < 0.0f; // поворот изображения 
        }
    }
}
