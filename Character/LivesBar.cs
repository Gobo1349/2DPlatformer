// Скрипт LivesBar - отвечает за отображение количества жизней
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesBar : MonoBehaviour // отвечает за отображение количества жизней – не цифра, а иконки
{
    private Transform[] Hearts = new Transform[5]; // ссылка на сердечки - массив из 5 элементов

    private Character character; // нужно знать количество жизней 

    private void Awake() //получаем эти ссылки
    {
        character = FindObjectOfType<Character>(); // находим игрока на сцене
        for (int i = 0; i < Hearts.Length; i++) // отображаем количество жизней в интерфейсе 
        {
            Hearts[i] = transform.GetChild(i);

        }
    }

    public void Refresh() // метод обновления количества жизней (если оно изменилось)
    {
        for (int i = 0; i < Hearts.Length; i++)
        {
            if (i < character.Lives) Hearts[i].gameObject.SetActive(true); // напр - 4 жизни - видно 0 1 2 3 сердца 
            else Hearts[i].gameObject.SetActive(false);
        }
    }
}
