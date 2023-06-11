// Скрипт CameraController - управление камерой на сцене
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour // устанавливает положение камеры
{
    [SerializeField]
    private float speed = 2.0f;

    [SerializeField]
    private Transform target; // за кем камера движется  

    private void Awake() // если забыли установить 
    {
        if (!target) FindObjectOfType<Character>();
    }

    private void Update() // движение камеры 
    {
        Vector3 position = target.position;
        position.z = -10f; // это для того, чтобы камера и плоскость игры не совпадали
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime); // лерп - сглаживание
    }
}
