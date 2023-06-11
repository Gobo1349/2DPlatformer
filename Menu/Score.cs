// Скрипт Score - сохранение прогресса 
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI; // для работы с интерфейсом

public class Score : MonoBehaviour
{
    [SerializeField]
    private Text[] counter = new Text[5]; // меняет непосредственно текст под кнопками соответствующих уровней
    [SerializeField]
    private Text[] marks = new Text[5]; // меняет непосредственно текст под кнопками соответствующих уровней

    void Start()
    {
        refresh();
    }

    public void refresh() // выводим количество собранных очков для всех уровней 
    {
        for (int i = 0; i < 5; i++)
        {
            counter[i].text = ("x" + PlayerPrefs.GetInt("score" + (i + 1))) + "/10";
            marks[i].text = ("" + PlayerPrefs.GetInt("mark" + (i + 1)));
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
