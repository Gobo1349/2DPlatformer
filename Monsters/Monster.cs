// Скрипт Monster - обычный противник
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Versioning;
using UnityEngine;

public class Monster : Unit // простой неподвижный противник
{
    public UnityEngine.Object Explosion; // эффект разрушения при уничтожении игроком 
    [SerializeField] 
    public string Explosion_name;

    public virtual void ReceiveDamage() // получение урона - будем переопределять (virtual)
    {
        
        Destroy(gameObject); // противник уничтожается и происходит эффект разрушения на частицы
        GameObject explosionref = (GameObject)Instantiate(Explosion);
        explosionref.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    protected virtual void Awake()
    { }
    protected virtual void Update()
    { }
    protected virtual void Start()
    {
        Explosion = Resources.Load(Explosion_name); // ссылка на эффект 
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider) // в монстра попала пуля -  protected virtual, т к будет наследоваться
    {
        Bullet bullet = collider.GetComponent<Bullet>();

        if (bullet) // если попала пуля
        {
            ReceiveDamage();
        }

        Unit unit = collider.GetComponent<Unit>(); // проверяем, не юнит ли это (проверка на наличие компонента)
        if (unit && unit is Character) // столкновение с ГГ 
            unit.ReceiveDamage(); // ГГ получает урон
    }
}
