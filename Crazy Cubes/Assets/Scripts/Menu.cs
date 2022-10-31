using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void PlayGame() //"Start" ve "Reset" için
    {
        //2.Ekrana gitmesi için (Game Ekranı):
        SceneManager.LoadScene(3);
        /*Test*/ Debug.Log("start çalışıyor");
    }

    public void QuitGame() //"Quit" için
    {
        //Oyundan Çıkması için:
        Application.Quit();
        /*Test*/ Debug.Log("quit çalışıyor");
    }

    public void NextLevel() //"Next Level" için 
    {
        //Sonraki Ekrana gitmek için (Sonraki Seviye):
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        /*Test*/ Debug.Log("nextLevel çalışıyor");
    }
}
