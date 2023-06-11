// Скрипт GameMaster - вспомогательный скрипт для работы сохранения
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour // для работы чекпоинтов - зафиксировать последний чекпоинт, где был игрок 
    // при загрузке сцены - поместить персонаж в позицию чекпоинта
{
    private static GameMaster instance; // вспомогательная переменная 
    public Vector2 LastCheckpointPos; // позиция последней точки сохранения 

    void Awake() // до старта
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); // объект не будет уничтожаться между сценами
        }
        else { Destroy(gameObject); } // не будет нескольких gamemaster
    }
}
