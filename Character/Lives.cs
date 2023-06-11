// Скрипт Lives - вывод количества жизней на экран
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour // вывод количества жизней на экран
{

    public static int LiveAmount;
     Text LivesText; // элемент интерфейса

    void Start()
    {
        LivesText = GetComponent<Text>();
        LiveAmount = 5; // изначально 5 жизней 
    }

    void Update()
    {
        LivesText.text = "Lives: " + LiveAmount; // выводим значение на экран
    }
}
