using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    AudioManager audioManager;
    // Referencias a los botones y textos del interfaz
    public Button registerButton;
    public Button loginButton;
    public Text playerDisplay;

    private void Start()//Se llama una sola vez. (De Unity)
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        audioManager.Stop("Engine");
         audioManager.Stop("PlayerShot");
         audioManager.Stop("AlienMoving");

        if(DBManager.LoggedIn)
        {
            playerDisplay.text= "Player: "+DBManager.username;
        }
        //Interactividad de los botones.
        registerButton.interactable = !DBManager.LoggedIn;
        loginButton.interactable = !DBManager.LoggedIn;
    }
    
    public void GoToRegister()
    {
        SceneManager.LoadScene(1);
    }
    public void GoToLogin()
    {
        SceneManager.LoadScene(2);
    }

    public void GoToPlayOffline()
    {
        SceneManager.LoadScene(4);
    }

    public void GoToHighscores()
    {
        SceneManager.LoadScene(5);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    // Funciones de sonido llamadas a traves del mouse.
    public void ChangeOnPointerEnter()
    {
        audioManager.Play("HoverButton",1f,0.15f);
    }

    public void ChangeOnPointerClick()
    {
        audioManager.Play("Accept",0.5f,0.15f);  
    }
}
