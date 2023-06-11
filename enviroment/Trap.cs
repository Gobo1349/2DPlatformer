// Скрипт Trap - внезапно появляющееся препятствие 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Press
{
    [SerializeField]
    float pos; // точка остановки
    [SerializeField]
    public float StoppingDistance; // расстояние, при преодолении которого появляется ловушка

    Transform player;

    bool up = false;
    
    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // поиск положения игрока 
    }

    protected void Update()
    {

        if (Mathf.Abs(player.transform.position.x - transform.position.x) < StoppingDistance) up = true; // если игрок подошел близко

        if (up) Up(); // появляется ловушка

        if (transform.position.y > pos) speed = 0; // прекращает движение при достижении точки остановки
    }

    void Up() // движение вверх
    { 
        transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime); 
    }
}
