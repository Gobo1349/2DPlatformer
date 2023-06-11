// скрипт CharacterController2D - движение персонажа
using UnityEngine;

public class CharacterController2D : MonoBehaviour // вспомогательный скрипт ГГ - определяет движение 
{
    public SpriteRenderer sprite; // ссылка на изображение 

    [SerializeField]
    private float speed = 3f; 
    [SerializeField]
    private float jumpforce = 15f; // сила прыжка

    public bool isGrounded = false; // прыгать можно, только если стоим на земле

    public Animator animator; // для изменения анимаций

    new public Rigidbody2D rigidbody;

    [SerializeField] private LayerMask m_WhatIsGround;  // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;   // A position marking where to check if the player is grounded.

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded



    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>(); //   спрайт - дочерний объект 
        animator = GetComponent<Animator>(); // это ссылки на необходимые компоненты
        rigidbody = GetComponent<Rigidbody2D>(); 
    }

    public void Run() // ходьба
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal"); // локальная переменная - определяем направление
        // теперь двигаем персонажа
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);  // откуда - куда - расст за один фрейм
        // нужно разворачивать персонажа при движении влево - это зависит от направления (если компонента х > 0 - не нужно и наоборот)
        sprite.flipX = direction.x < 0.0f;
        if (isGrounded) State = CharState.Run; // анимация ходьбы, если стоим на земле
    }

    public void Jump() 
    {
        // приложим силу к объекту - используем вторую перегрузку - вектор силы и куда прыгать , тип силы - импульс 
        rigidbody.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
    }

    public void CheckGround() // прыгать можно, только если стоим на земле
    {
        isGrounded = false;
        // получим массив коллайдеров
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround); // проверяет коллайдеры в круге опр радиуса 
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject) // если под ГГ есть коллайдер
                isGrounded = true;
        }
        if (!isGrounded) State = CharState.Jump; // анимация прыжка
    }

    public CharState State // свойство связывает перечисление и аниматор 
    {
        get { return (CharState)animator.GetInteger("State"); } // текущее состояние 
        set { animator.SetInteger("State", (int)value); }
    }

    public enum CharState // возможные состояния анимации игрока 
    {
        Idle, // 0 - значения выбрали в аниматоре 
        Run, // 1 
        Jump // 2 
    }

}

