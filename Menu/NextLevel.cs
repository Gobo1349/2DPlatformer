//Скрипт NextLevel - переход на следующую сцену
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private GameMaster gm;
    [SerializeField]
    public Vector2 Begin; // начало следующего уровня
    public int sceneNum = 0; 
    public AudioSource MoveSound; // звуковой эффект 

    private void Start()
    {
        //End.SetActive(false);
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>(); // подгружаем ссылку на gamemaster
        sceneNum = SceneManager.GetActiveScene().buildIndex; // номер текущей сцены 
    }

    private void Update()
    {
        if (Pause_menu.TestingIsOver)
        {

            //End.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) // при столкновении с игроком 
    {
        MoveSound.Play(); // звуковой эффект 
        gm.LastCheckpointPos = Begin; // при переходе на след уровень изменяем координаты на начало нового уровня
        LevelController.instance.IsEndGame(); // см скрипт LevelController
        Check(sceneNum);
   //     PlayerPrefs.SetInt("score" + sceneNum, CoinCollect.count);       
    }

    void Check(int sceneNum) // подсчет набранных очков и определение рекорда игрока 
    {
        int OldScore = PlayerPrefs.GetInt("score" + sceneNum);
        int OldPageNum = PlayerPrefs.GetInt("PageNum");

        if (OldScore <= CoinCollect.coin_count)
        {
            PlayerPrefs.SetInt("score" + sceneNum, CoinCollect.coin_count);
        } else
        {
           PlayerPrefs.SetInt("score" + sceneNum, OldScore);
        }

        //if (OldPageNum <= CoinCollect.book_count)
        //{
        //    PlayerPrefs.SetInt("PageNum", CoinCollect.book_count);
        //}
        //else
        //{
        //    PlayerPrefs.SetInt("PageNum", OldPageNum);
        //}
    }
}
