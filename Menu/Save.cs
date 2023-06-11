// Скрипт Save - сохранение прогресса
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using System.Globalization;

public class Save : MonoBehaviour
{
    [SerializeField]
    private Button[] levels = new Button[4]; // 2 3 4 5 уровни - 1 всегда открыт 
    int levelComplete;

    void Awake()
    {
        for (int i = 0; i < 4; i++) // изначально они недоступны 
        {
            levels[i].interactable = false;
        }
    }
    void Start()
    {
        levelComplete = PlayerPrefs.GetInt("LevelComplete"); // сколько завершили уровней

        for (int i = 1; i <= levelComplete; i++) // даем досткп к кнопкам, уровни которых открыты 
        {
          levels[i - 1].interactable = true;
        }
    }

    public void Reset() // при сбросе прогресса 
    {
        for (int i = 0; i < 4; i++)
        {
            levels[i].interactable = false;
        }
    }
}
