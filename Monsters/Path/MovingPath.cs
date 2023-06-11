//Скрипт MovingPath - движение по траектории
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPath : MonoBehaviour // скрипт, реализующий траекторию пути
{
public enum PathTypes // виды пути
    {
        linear, // незамкнутый 
        loop // замкнутый
    }

    public PathTypes PathType; // выбранный вид пути
    public int Direction = 1; // направление движения - вперед/по часовой стрелке
    public int movingTo = 0; // куда двигаться (к какой точке)
    public Transform[] PathElements; // точки 

    public void OnDrawGizmos() // отображение пути (линии)
    {
        if (PathElements.Length < 2) return; // проверка на наличие необходимого кол-ва точек пути (мин 2)

        for (int i = 1; i < PathElements.Length; i++) // прогоняем все точки 
        {
            Gizmos.DrawLine(PathElements[i - 1].position, PathElements[i].position); // рисуем линии
        }

        if (PathType == PathTypes.loop) // если замкнутый путь
        {
            Gizmos.DrawLine(PathElements[0].position, PathElements[PathElements.Length - 1].position); // соединяем первую и последнюю точки 
        }
    }
// интерфейс - описание типа; но это скорее сопрограмма, которая этот интерфейс возвращает и использует yield
    public IEnumerator<Transform> GetNextPathPoint() // определяем положение следующей точки 
    {
        if (PathElements.Length < 1) yield break; // есть ли точки, по которым нужно строить маршрут - если их нет, выходим
        while (true)
        {
            yield return PathElements[movingTo]; // определяет возвращаемый элемент

            if (PathElements.Length == 1) continue; // если одна точка, то нечего строить, выходим

            if (PathType == PathTypes.linear)
            {
                if (movingTo <= 0) // если двигаемся по нарастающей (вперед?)
                {
                    Direction = 1;
                }
                else if (movingTo >= PathElements.Length - 1) // назад?
                {
                    Direction = -1;
                }
            }

            movingTo = movingTo + Direction; // для движения к следующей точке 

            if (PathType == PathTypes.loop)
            {
                if (movingTo >= PathElements.Length)
                {
                    movingTo = 0; // если дошли до конца - идем не обратно, а к первой точке 
                }
                if (movingTo < 0)
                {
                    movingTo = PathElements.Length - 1;
                }
            }

        }
    }

}
