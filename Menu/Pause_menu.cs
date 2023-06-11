// Скрипт Pause_menu - меню паузы
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using System.Threading;
using UnityEngine;

public class Pause_menu : MonoBehaviour
{
    private GameMaster gm;
    public static bool GameIsPaused = false; // изначально игра не на паузе
    public static bool BookIsOpen = false; // изначально учебник закрыт
    public static bool TestIsOpen = false; // изначально учебник закрыт
    public static bool Testing = false; // изначально учебник закрыт


    public GameObject pauseMenuUI; // меню паузы
    public GameObject textbook; // учебник с теоретическим материалом
    public GameObject warning; // учебник с теоретическим материалом
    public GameObject test; // учебник с теоретическим материалом

    public static bool atComputer = false;
    public static bool TestingIsOver = false;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>(); // подгружаем ссылку на gamemaster
    }
    void Update() // Update is called once per frame
    {
       if (!TestIsOpen && !BookIsOpen && Input.GetKeyDown(KeyCode.Escape)) // при нажатии esc 
        {
            if (GameIsPaused)  // если игра была на паузе - выходим из паузы
            {
                GameIsPaused = TimeGo(pauseMenuUI, GameIsPaused);
            }
            else    // если шла игра - будет пауза
            {
                GameIsPaused = TimeStop(pauseMenuUI, GameIsPaused);
            }
        }
        if (!TestIsOpen && !GameIsPaused && Input.GetKeyDown(KeyCode.T)) // при нажатии T - открываем учебник 
        {
            if (BookIsOpen)  // если учебник уже был открыт - закрываем 
            {
                BookIsOpen = TimeGo(textbook, BookIsOpen);
            }
            else    // если шла игра - откроется учебник
            {
                BookIsOpen = TimeStop(textbook, BookIsOpen);
            }
        }
        if (!GameIsPaused && atComputer && !Testing && Input.GetKeyDown(KeyCode.E))
        {
            //UnityEngine.Debug.Log("!!!");
            if (TestIsOpen)  // если учебник уже был открыт - закрываем 
            {
                TestIsOpen = TimeGo(warning, TestIsOpen);
            }
            else    // если шла игра - откроется учебник
            {
                TestIsOpen = TimeStop(warning, TestIsOpen);
            }
        }
        if (TestingIsOver)
        {
            Testing = false;
            TestIsOpen = false;
            test.SetActive(false);
            Time.timeScale = 1f;
            TestingIsOver = false;
        }
    }

    public void Resume() // продолжение игры 
    {
        GameIsPaused = TimeGo(pauseMenuUI, GameIsPaused);
    }

    public void LoadMenu() // выход в главное меню
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f; // скорость должна восстановиться 
        GameIsPaused = false;
    }

    public void Restart() // перезапуск уровня 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        Time.timeScale = 1f; // скорость должна восстановиться 
        GameIsPaused = false;
    }

    bool TimeStop(GameObject Object, bool flag)
    {
        Object.SetActive(true); // активно меню паузы
        Time.timeScale = 0f; // останавливаем все
       return flag = !flag;
    }

    bool TimeGo(GameObject Object, bool flag)
    {
        Object.SetActive(false); // активно меню паузы
        Time.timeScale = 1f; // останавливаем все
        return flag = !flag;
    }

    public void Test()
    {
        Testing = true;
        warning.SetActive(false);
        test.SetActive(true);
    }

    public static void EndOfTestMessage()
    {
        //Testing = false;
        //test.SetActive(false);
        //Time.timeScale = 1f;
    }
}
