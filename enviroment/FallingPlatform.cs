// Скрипт FallingPlatform - падающая платформа
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class FallingPlatform : Block
{
    Rigidbody2D rb;
    Vector2 currentPosition; // начальное положение 
    bool moveBack; // возвращение назад 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // реализовывает работу rigidbody 
        currentPosition = transform.position; // сохренили в памяти начальное положение 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Character") && moveBack == false) // после соприкосновения с ГГ платформа падает
        {
            Invoke("FallPlatform", 1f);
        }
    }

    void BackPlatform() // возвращение платформы на изначальную позицию
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        moveBack = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    void FallPlatform() // падение
    {
        rb.isKinematic = false; // если не kinematic, то dynamic
        Invoke("BackPlatform", 2f);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    void Update()
    {
        if (moveBack) // если платформа падает
        {
            transform.position = Vector2.MoveTowards(transform.position, currentPosition, 20f * Time.deltaTime); // движение обратно
        }

        if (transform.position.y == currentPosition.y) // достигнута изначальная позиция
        {
            moveBack = false; // останавливается 
        }
    }
}
