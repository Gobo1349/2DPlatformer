// Скрипт Key - ключ, открывающий дверь
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Key : MonoBehaviour
{
    public SpriteRenderer sprite; // ссылка на изображение 
    public static bool destroy = false;
    public Animator animator; // для изменения анимаций
    bool turn = false;

    private void OnTriggerEnter2D(Collider2D collider) // если ГГ подошел к объекту 
    {
        Character character = collider.GetComponent<Character>(); //проверяем, игрок ли это 

        if (character)
        {
            animator.SetBool("Move", true);           
            Invoke("SetAnimateFalse", 1f);
        }
    }

    void SetAnimateFalse()
    {
        animator.SetBool("Move", false);
        destroy = true;
    }
    public void Awake()
    {
        animator = GetComponent<Animator>(); // это ссылки на необходимые компоненты
    }

    

   public void Update()
    {
        if (turn)
        {
            print("ffff");

        }
    }
}
