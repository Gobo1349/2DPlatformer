// Скрипт LevelController - Вспомогательный скрипт для смены сцен
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController instance = null;
    int sceneIndex; // номер уровня
    int levelComplete; // какие уровни завершены

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        sceneIndex = SceneManager.GetActiveScene().buildIndex; // определяем номер текущей сцены
        Check(sceneIndex - 1); // определяем уровень, дальше которого игрок не прошел 
        levelComplete = PlayerPrefs.GetInt("LevelComplete"); 
    }

    public void IsEndGame()
    {
        
        if (sceneIndex == 5) // если игрок прошел все 5 уровней 
        {
            Invoke("LoadEnd", 1f); // окно автора 
        }
        else
        {              
                Check(sceneIndex); // загрузка следующего уровня 
                Invoke("NextLevel", 1f);
        }
    }

    void Check(int index) // для корректной работы меню уровней - иначе пройденные уровни могут "забыться"
    { // определяем уровень, дальше которого игрок не прошел
        int num = PlayerPrefs.GetInt("LevelComplete");
        if (num <= index) 
        { 
            PlayerPrefs.SetInt("LevelComplete", index);
        } else
        {
            PlayerPrefs.SetInt("LevelComplete", num);
        }      
    }
    
    void NextLevel() // переход к следующему уровню
    {
        SceneManager.LoadScene(sceneIndex + 1);
    }
    void LoadEnd() // загрузка окна автора
    {
        SceneManager.LoadScene(6);
    }
}
