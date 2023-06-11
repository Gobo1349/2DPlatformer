// Скрипт CoinCollect - сохранение очков
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI; // для работы с интерфейсом

public class CoinCollect : MonoBehaviour // отвечает за счетчик собранных монеток 
{
    public static int coin_count; // количество собранных монет
    public static int book_count; // количество собранных страниц

    private Text counter; // меняет непосредственно текст

    void Start()
    {
        counter = GetComponent<Text>(); // счетчик будет считывать текстовый компонент 
        coin_count = 0; // начальное количество
    }

    // Update is called once per frame
    void Update()
    {
        counter.text = "x" + coin_count; // отображаем количество собранных элементов
    }
}
