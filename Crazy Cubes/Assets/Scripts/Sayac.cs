using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sayac : MonoBehaviour
{
    float currentTime ;
    float startingTime = 0f;

    [SerializeField] Text TimerText;

    void Start()
    {
        currentTime = startingTime; //zaman? s?f?rlamak i�in
    }

    void Update()
    {
        currentTime += 1 * Time.deltaTime; //zaman bilgilerini almak
        TimerText.text = currentTime.ToString(); //zaman? "TimerText" i�ine yazd?rmak i�in
    }
}
