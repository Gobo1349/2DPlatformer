// скрипт Test - контроль усвоения материала
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI; // для работы с интерфейсом
using System.IO; // для работы с текстовыми файлами

public class Test : MonoBehaviour
{
    private int QuestionNum; // номер вопроса 

    [SerializeField] public int MaxQuestionNum = 5; // количество вопросов в тесте
    private int Mark = 0; // оценка за выполнение теста

    [SerializeField] private Text Header; // заголовок вопроса в тесте 
    [SerializeField] private Text Question; // текст вопроса

    [SerializeField] private GameObject QuestionPic; // переменные для вывода изображений - вопросов на экран
    [SerializeField] public Image QuestionPicImg;

    [SerializeField] public GameObject LeftButton; // кнопка перехода к след вопросу 
    [SerializeField] public GameObject Computer; // объект, с которым взаимодействуем 
    [SerializeField] public GameObject End; // переход к следующему уровню игры 
    [SerializeField] public Toggle[] Variants = new Toggle[4]; // Четыре варианта ответа 

    [SerializeField] public int[] RightAnswer = new int[5]; // массив правильных ответов
    public int[] Answer; // массив ответов пользователя
    public bool[] Solved; // переменная, обозначающая, что ответ на вопрос получен

    private bool EndOfTest = false; // переменная, обозначающая, что тест завершен
    public int SceneNum = 0; // номер текущей сцены - уровня

    void Start() // вызывается один раз при запуске скрипта
    {
        if (Pause_menu.Testing) // если запущено тестирование
        {
            SceneNum = SceneManager.GetActiveScene().buildIndex + 0; // определяем номер текущей сцены

            QuestionPic.gameObject.SetActive(true); // делаем активным объект - форму вопроса
            QuestionPicImg.sprite = Resources.Load((@"TaskPics\theme" + SceneNum + @"\" + 1), typeof(Sprite)) as Sprite; // загружаем вопрос из файла

            Answer = new int[MaxQuestionNum]; // ответы игрока
            Solved = new bool[MaxQuestionNum];  // флаг - получен ответ на вопрос или нет 

            QuestionNum = 0; // начинаем с 0ого (первого) вопроса
            Header.text = "Вопрос " + (QuestionNum + 1).ToString(); // номер вопроса в заголовке

            for (int i = 0; i < Answer.Length; i++)
            {
                Answer[i] = -1; // изначально ответов пользователя нет 
            }
        }
    }

    public void Toggle_А(bool Value) // метод выбора ответа
    {
        if (Value) // если поставлена галочка 
        {
            Solved[QuestionNum] = true; // вопрос считается решенным
            for (int i = 0; i < Variants.Length; i++) // определяем ответ пользователя
            {
                if (Variants[i].isOn)
                {
                    Answer[QuestionNum] = i;
                }
            }
        }
        else // если галочка снимается
        {
            Solved[QuestionNum] = false; // вопрос считается нерешенным
            Answer[QuestionNum] = -1; // ответа пользователя нет 
        }
    }

    public void NextQuestion() // загрузка следующего вопроса
    {
        QuestionNum++; 
        if (QuestionNum < MaxQuestionNum) // если еще не конец теста
        {
            Header.text = "Вопрос " + (QuestionNum + 1).ToString(); // обновляем заголовок
            QuestionPicImg.sprite = Resources.Load((@"TaskPics\theme" + SceneNum + @"\" + (QuestionNum + 1)), typeof(Sprite)) as Sprite; // загружаем вопрос из файла

            if (!Solved[QuestionNum]) // если вопрос не решен
            {
                for (int i = 0; i < Variants.Length; i++) // нет ни одной галочки
                {
                    Variants[i].isOn = false;
                }
            }
            else // если вопрос решен 
            {
                Variants[Answer[QuestionNum]].isOn = true; // ставится галочка на выбранном ответе
            }
        } else // если вопросов больше нет 
        {
            if (QuestionNum == MaxQuestionNum)
            {
                EndOfTest = true;
                QuestionPic.gameObject.SetActive(false);
                Header.text = "Завершение теста";
                Question.text = "Завершить тестирование и узнать оценку?"; // предупреждение 
                for (int i = 0; i < Variants.Length; i++)
                {
                    Variants[i].gameObject.SetActive(false);
                }
            } else if (QuestionNum == MaxQuestionNum + 1)  // подсчитываем оценку пользователя
            {
                for (int i = 0; i < Answer.Length; i++)
                {
                    if (Answer[i] == RightAnswer[i])
                    {
                        Mark++;
                    }
                }
                if (Mark == 0 || Mark == 1) // оценки 0 и 1 в 5-балльной системе не предусмотрено
                {
                    Mark = 2;
                }
                LeftButton.gameObject.SetActive(false);
                Header.text = "Результаты тестирования";
                //Question.text = "Правильные ответы " + RightAnswer[0].ToString() + " " + RightAnswer[1].ToString() + " " + RightAnswer[2].ToString() + " " + RightAnswer[3].ToString() + " " + RightAnswer[4].ToString() + '\n' +
                Question.text = //"Ваши ответы       " + Answer[0].ToString() + " " + Answer[1].ToString() + " " + Answer[2].ToString() + " " + Answer[3].ToString() + " " + Answer[4].ToString() + '\n' +
                "Ваша оценка " + Mark.ToString();

                if (Mark > PlayerPrefs.GetInt("mark" + SceneNum)) 
                {
                    PlayerPrefs.SetInt("mark" + SceneNum, Mark); // сохраняем оценку
                }
                if (Mark > 2) // если тест пройден
                {
                    Question.text = Question.text + '\n' + "Поздравляю, тест пройден. Путь дальше свободен.";
                }
                else // если тест не пройден 
                {
                    Question.text = Question.text + '\n' + "К сожалению, слишком много ошибок. Придется пройти уровень еще раз";
                }
            } else if (QuestionNum == MaxQuestionNum + 2) // выход из тестирования
            {
                if (Mark > 2) // если прошли - появляется переход к следующей теме
                {
                    Pause_menu.TestingIsOver = true;
                    Destroy(Computer);
                    End.SetActive(true);
                }
                else // если не прошли - уровень начинается заново
                {
                    Pause_menu.TestingIsOver = true;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
                    Time.timeScale = 1f; // скорость должна восстановиться 
                }
            }
        }
    }

    public void PreviousQuestion() // предыдущий вопрос 
    {
        if (QuestionNum > 0)
        {
            QuestionNum--;
            if (EndOfTest) // если идем назад от конца теста, нужно снова "включить" возможность выбора вариантов
            {
                for (int i = 0; i < Variants.Length; i++)
                {
                    Variants[i].gameObject.SetActive(true);
                }
            }
            Header.text = "Вопрос " + (QuestionNum + 1).ToString(); // заголовок 
            QuestionPic.gameObject.SetActive(true); 
            QuestionPicImg.sprite = Resources.Load((@"TaskPics\theme" + SceneNum + @"\" + (QuestionNum + 1)), typeof(Sprite)) as Sprite; // загружаем вопрос из файла

            if (Solved[QuestionNum]) // если вопрос был решен - отмечаем ответ пользователя
            {
                Variants[Answer[QuestionNum]].isOn = true;
            }
            else
            {
                for (int i = 0; i < Variants.Length; i++) // если вопрос не был решен - галочки неактивны 
                {
                    Variants[i].isOn = false;
                }
            }
        }
    }
}
