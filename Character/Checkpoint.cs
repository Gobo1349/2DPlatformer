// Скрипт CheckPoint - точка сохранения
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour // точка сохранения 
{
    private GameMaster gm;
    public UnityEngine.Object Light;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>(); // подгружаем ссылку на gamemaster
        Light = Resources.Load("Light"); // для эффекта мерцания при активации
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) // проверяем - игрок подошел к чекпоинту
        {
            gm.LastCheckpointPos = transform.position; // присваиваем координаты нового чекпоинта
            GameObject lightref = (GameObject)Instantiate(Light); // эффект мерцания 
            lightref.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }      
    }
}
