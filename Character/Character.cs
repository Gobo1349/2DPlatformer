// Самсонов Михаил Евгеньевич, А-12-17
// скрипт Character - основной персонаж 
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : Unit // основной скрипт главного героя  
{
    [SerializeField] // данная команда позволяет задать значение непосредственно из Unity
    public int lives = 5; // количество жизней
    private GameMaster gm; // объект для запоминания контрольных точек
    public int SceneNum; // номер текущей сцены - уровня 

    public AudioSource ShootSound; // для воспроизведения звука

    public CharacterController2D controller; // для связи с вспомогательным скриптом

    [SerializeField] public GameObject[] PagesOnScene = new GameObject[3]; // количество объектов - страниц на сцене

    public int[] Offset = new int[5]; // смещение (в страницах) к теме текущего уровня от начала учебника 


    public int Lives // создаем свойство для отображения количества жизней на экране
    {
        get { return lives;  }
        set
        {
            if (value <= 5) lives = value; // если изменилось кол-во жизней - 
            livesBar.Refresh(); // обновляем интерфейс
        }
    }

    private LivesBar livesBar; // для отображения количества жизней на экране

    [SerializeField]
    private Bullet bullet; // ссылка на prefub - объект, которым стреляет персонаж

    private void Awake() // Awake используется для инициализации любых переменных или состояния игры перед тем как игра будет загружена

    {
        livesBar = FindObjectOfType<LivesBar>(); // связываем объект с элементом на экране
    }

    private void Start() // вызывается один раз при запуске скрипта
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>(); // подгружаем ссылку на gamemaster
        transform.position = gm.LastCheckpointPos; // в начале игры игрок находится у первого чекпоинта; после смерти возрождается у последнего чекпоинта, который удалось посетить
        SceneNum = SceneManager.GetActiveScene().buildIndex + 0;
        Offset[0] = 0; // задаем значения смещений
        Offset[1] = 3;
        Offset[2] = 7;
        Offset[3] = 5;
        Offset[4] = 5;
    }
    private void Update() // Update вызывается каждый кадр, если MonoBehaviour включен.
    {
        for (int i = 1; (i <= PlayerPrefs.GetInt("PageNum") - Offset[SceneNum - 1]) && (i <= PagesOnScene.Length); i++) // определяем количество объектов - страниц, которые появятся на сцене
        {
            PagesOnScene[i - 1].gameObject.SetActive(false);
        }

        if (Time.timeScale == 0f) // для корректной работы паузы - чтобы персонаж не мог действовать во время паузы
        {
            return;
        }
        if (Input.GetButtonDown("Fire1")) Shoot(); // по нажатию кнопки стреляем 
        if (controller.isGrounded) controller.State = CharacterController2D.CharState.Idle; // определение анимации - покоя, если стоим на земле
        if (Input.GetButton("Horizontal")) controller.Run(); // по нажатию кнопки идем
        if (controller.isGrounded && Input.GetButtonDown("Jump")) controller.Jump(); // по нажатию кнопки - прыжок

        if (lives == 0 || transform.position.y < -100) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // смерть и перезапуск уровня - если кончились жизни или упали
       
    }

    // Эта функция вызывается с частотой фиксированных кадров (fixed framerate), если MonoBehaviour включен.
    private void FixedUpdate() // для физики
    {
        controller.CheckGround(); // проверяем нахождение на земле
    }

    private void Shoot() // метод стрельбы
    {
        Vector3 position = transform.position; // откуда полетит пуля
        position.y += 1.0f; 
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet; // создаем пулю (prefub - position - rotation)
        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (controller.sprite.flipX ? -1.0f : 1.0f);
        ShootSound.Play(); // звуковой эффект
    }

    public override void ReceiveDamage() // получение персонажем урона
    {
       Lives--; // уменьшаем свойство
        controller.rigidbody.velocity = Vector3.zero; // обнуляем ускорение - при падении на пику - иначе не будет подбрасывания 
        controller.rigidbody.AddForce(transform.up * 11.0f, ForceMode2D.Impulse); // при получении урона персонажа "отталкивает"      
    }

    private void OnTriggerEnter2D(Collider2D collider) // при взаимодействии коллайдеров
    {
        Bullet bullet = collider.gameObject.GetComponent<Bullet>(); // если в нас попала пуля
        if (bullet && bullet.Parent != gameObject) ReceiveDamage(); // получаем урон

        if (collider.tag.Equals("Coin")) // если нашли монетку 
        {
            CoinCollect.coin_count++; // счетчик +
            Destroy(collider.gameObject); // сама монетка исчезает
        }

        if (collider.tag.Equals("TextBook")) // если нашли страницу 
        {
            PlayerPrefs.SetInt("PageNum", (PlayerPrefs.GetInt("PageNum") + 1));
            collider.gameObject.SetActive(false); // сама страница исчезает
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // привязка к движущейся платформе 
    {
        if (collision.gameObject.CompareTag("Platform")) // если запрыгнули на движ. платформу
        {
            this.transform.parent = collision.transform; // теперь платформа - родительский объект, и персонаж перенимает у нее скорость
        }
    }

    private void OnCollisionExit2D(Collision2D collision) // если спрыгиваем с платформы
    {
        if (collision.gameObject.CompareTag("Platform")) 
        {
            this.transform.parent = null; // теперь род. объекта нет
        }
    }
}


