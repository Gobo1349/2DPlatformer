// Скрипт Guard - противник, преследующий игрока
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class Guard : Monster
{
    [SerializeField]
    public float speed;
    [SerializeField]
    public int PositionOfPatrol; // расстояние патрулирования 
    [SerializeField]
    public Transform point; // точка отсчета патрулирования
    public float StoppingDistance; // расстояние, при преодолении которого начинается атака
    Transform player;
 
    bool movingRight = true; // движется вправо
    bool chill = true; // состояние покоя (патрулирования)
    bool angry = false;// преследования 
    bool goBack = false; // возврата к точке патрулирования 
  // bool turn = false;

    private SpriteRenderer sprite; // нужно будет менять анимацию -
    private Animator animator;
    private Vector3 direction; // направление для бега


    protected override void Awake() // переопределяем метод 
    {
        sprite = GetComponentInChildren<SpriteRenderer>(); // получаем ссылку 
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        animator = GetComponent<Animator>(); 
        Explosion = Resources.Load("Explosion"); // эффект при разрушении 
        player = GameObject.FindGameObjectWithTag("Player").transform; // поиск положения игрока 
        direction = transform.right;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Vector2.Distance(transform.position, point.position) < PositionOfPatrol && angry == false) chill = true;   // если страж в зоне, которую он охраняет - то chill 
        if (Vector2.Distance(transform.position, player.position) < StoppingDistance) // если ГГ в зоне преследования - объект начинает его преследовать 
        {
            angry = true; // если ГГ подошел слишком близко            
            chill = false;
            goBack = false;

        }  

        if (Vector2.Distance(transform.position, player.position) > StoppingDistance) // если ГГ смог оторваться от погони на опр. расстояние - объект возвращается назад к точке 
        {
            goBack = true;  
            angry = false;
           
        }

        if (chill == true) Chill();
        else if (angry == true)
        {   
            Angry();
        }
        else if (goBack == true)
        {          
            GoBack();
        }

        //if (turn)
        //{
        //    print("wwww");
        //    direction *= -1;
        //    sprite.flipX = direction.x < 0.0f; // должен поворачиваться - как персонаж - опытным путем
        //    turn = false;
        //}

    }

    void Chill() // патрулирование - обычное движение вправо - влево
    {
        speed = 1;
        animator.SetBool("angry", false);
        if (transform.position.x > point.position.x + PositionOfPatrol)
        {
            movingRight = false;
         //   turn = true;
        }// дошли до границы - поворачиваем 
        else if (transform.position.x < point.position.x - PositionOfPatrol)
        {
            movingRight = true;
          //  turn = true;
        }
            if (movingRight) transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y); // СКОРОСТЬ + значит вправо
            else transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y); // скорость - значит влево
    }

    void Angry() // преследование игрока 
    {
        speed = 2.5f;
        animator.SetBool("angry", true);
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime); // движение в сторону игрока 
        
    }

    void GoBack() // возвращение к точке 
    {
        speed = 1;
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
    }
}
