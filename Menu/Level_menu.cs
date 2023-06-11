//Скрипт Level_menu - меню выбора уровней
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // для работы с интерфейсом

public class Level_menu : MonoBehaviour
{
    private GameMaster gm; // для работы чекпоинтов в начале игры 
    [SerializeField]
    public Vector2 Begin;
    public bool sound = true;

    [SerializeField]
    private Text text;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>(); // подгружаем ссылку на gamemaster
    }
    public void LoadLevel(int num) // метод выбора уровня 
    {
        gm.LastCheckpointPos = Begin; // при переходе на след уровень изменяем координаты на начало нового уровня
        SceneManager.LoadScene(num); // позволит запускаться игре - запускаем следующую сцену
    }

    public void Reset() // метод сброса прогресса 
    {
        PlayerPrefs.DeleteAll();
    }

    public void Sound() // метод включения / выключения звука 
    {
        if (sound)
        {
            AudioListener.pause = true;
            sound = false;
            text.text = "ON";
        } else
        {
            AudioListener.pause = false;
            sound = true;
            text.text = "OFF";
        }
        
    }

    public void GameExit() // метод выхода из игры 
    {
        Application.Quit();
    }
}
