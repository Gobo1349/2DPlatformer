// скрипт TextBook - сборник теоретической информации
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI; // для работы с интерфейсом
using System.IO;

public class TextBook : MonoBehaviour
{
    public int PageNum;

    [SerializeField] private GameObject LeftPic; // правая и левая страницы учебника
    [SerializeField] private GameObject RightPic;

    [SerializeField] public Image LeftPageImg;
    [SerializeField] public Image RightPageImg;


    [SerializeField] private Text NumPageLeft; // номер страницы
    [SerializeField] private Text NumPageRight; 
    [SerializeField] private Text Info; // вспомогательная информация
     
    public int[] MaxPagesNum = new int[5]; // количество страниц, которое выделяется под каждую тему
    public int SceneNum; // номер текущей сцены - уровня

    void Start() // вызывается один раз при запуске скрипта
    {
        MaxPagesNum[0] = 11; // определяем, сколько страниц будет выделено под каждую тему
        MaxPagesNum[1] = 24;
        MaxPagesNum[2] = 36;
        SceneNum = SceneManager.GetActiveScene().buildIndex + 0; // определяем номер сцены 

        LeftPic.gameObject.SetActive(false); // в начале страницы не отображаются 
        RightPic.gameObject.SetActive(false);

        PageNum = 1; // номер страниц
        NumPageLeft.text = PageNum.ToString();
        NumPageRight.text = (PageNum + 1).ToString();


        if (PlayerPrefs.GetInt("PageNum") == 0) // вывод сообщения 
            {
                Info.text = "Учебник пуст. Страницы не найдены";
            }
        
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("PageNum") != 0) // если пользователь нашел объект - страницу
        {
            Info.text = ""; 
            LeftPic.gameObject.SetActive(true); // страницы в учебнике начинают отображаться
            RightPic.gameObject.SetActive(true);
            LeftPageImg.sprite = Resources.Load((@"Pages\theme1\" + PageNum), typeof(Sprite)) as Sprite; // загружаем файл с материалом по теме 

            if (PageNum + 1 < MaxPagesNum[SceneNum - 1]) 
            {
                RightPageImg.sprite = Resources.Load((@"Pages\theme1\" + (PageNum + 1)), typeof(Sprite)) as Sprite;
            }
            else // если все страницы уже отобразились - на "лишней" странице ничего не будет
            {
                RightPageImg.sprite = Resources.Load((@"Pages\null"), typeof(Sprite)) as Sprite;
            }
        }
    }
    public void NextPage() // "перелистывание страницы"
    {
        if (PlayerPrefs.GetInt("PageNum") * 3 >= PageNum && PageNum < MaxPagesNum[SceneNum - 1]) // если пользователь нашел достаточно страниц
        {
            PageNum = PageNum + 2; // изменяем номера
            NumPageLeft.text = PageNum.ToString();
            NumPageRight.text = (PageNum + 1).ToString();
        }
    }

    public void PreviousPage() // "перелистывание страницы назад"
    {
        if (PageNum > 1)
        {
            PageNum = PageNum - 2;  // изменяем номера
            NumPageLeft.text = PageNum.ToString();
            NumPageRight.text = (PageNum + 1).ToString();
        }
    }
}

