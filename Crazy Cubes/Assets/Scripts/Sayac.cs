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
        currentTime = startingTime; //zaman? s?f?rlamak için
    }

    void Update()
    {
        currentTime += 1 * Time.deltaTime; //zaman bilgilerini almak
        TimerText.text = currentTime.ToString(); //zaman? "TimerText" içine yazd?rmak için
    }
}
