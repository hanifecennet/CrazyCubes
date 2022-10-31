using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;

    void Start ()
    {
        Resume();
    }
    void Update()
    {
       
        //esc tuşunu kullanarak PauseMenu çıkması için:
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }

    //Oyun devam ettiği durumunda:
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    //Oyunu durduğumuzda:
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //zamanı durdurmak için
        GameIsPaused = true;
    }

    //"Menu"ya basıca:
    public void LoadMenu()
    {
        Debug.Log("Loding menu...");
    }

    // "Quit"e basınca:
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
    }
}
