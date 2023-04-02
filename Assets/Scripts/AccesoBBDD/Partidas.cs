using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Partidas : MonoBehaviour
{
    public Sprite tanque1;
    public Sprite tanque2;
    public Sprite tanque3;
    
    public Button partida1Button;
    public Button partida2Button;
    public Button partida3Button;
    public Button volverButton;

    public Text playerDisplay;
    public Text logrosDisplay;

    private int idPartidaSeleccionada;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }
    void Update()//Se llama una vez por frame. (De Unity)
    {
        if(DBManager.LoggedIn)
        {
            playerDisplay.text= "Jugador: "+DBManager.username;
            logrosDisplay.text= "Logros: "+DBManager.logros;

            partida1Button.transform.Find("Image").GetComponent<Image>().sprite= SpriteSegunRango(DBManager.rangoA);
            partida1Button.transform.Find("Text").GetComponent<Text>().text=
            "Rango: "+ DBManager.rangoA+ "      Max. Score: "+ DBManager.maxScoreA+ "      Max. Oleadas: "+ DBManager.oleadaA;

            if(DBManager.partidaIDB!=0)
            {
                partida2Button.transform.Find("Image").GetComponent<Image>().sprite= SpriteSegunRango(DBManager.rangoB);
                partida2Button.transform.Find("Text").GetComponent<Text>().text=
                "Rango: "+ DBManager.rangoB+ "      Max. Score: "+ DBManager.maxScoreB+ "      Max. Oleadas: "+ DBManager.oleadaB;
            }
            else
            {
                partida2Button.transform.Find("Image").GetComponent<Image>().CrossFadeAlpha(0,0,true);
                partida2Button.transform.Find("Text").GetComponent<Text>().text= "Nueva partida";
            }

            if(DBManager.partidaIDC!=0)
            {
                partida3Button.transform.Find("Image").GetComponent<Image>().sprite= SpriteSegunRango(DBManager.rangoC);
                partida3Button.transform.Find("Text").GetComponent<Text>().text=
                "Rango: "+ DBManager.rangoC+ "      Max. Score: "+ DBManager.maxScoreC+ "      Max. Oleadas: "+ DBManager.oleadaC;
            }
            else
            {
                partida3Button.transform.Find("Image").GetComponent<Image>().CrossFadeAlpha(0,0,true);
                partida3Button.transform.Find("Text").GetComponent<Text>().text= "Nueva partida";
            }
        }                
    }

    private Sprite SpriteSegunRango(int rango)
    {
        Sprite rangoSprite;
        if(rango>=1 && rango<5)
        {
            rangoSprite= tanque1;
        }
        else if(rango>=5 && rango<10)
        {
            rangoSprite= tanque2;
        }
        else
        {
            rangoSprite= tanque3;
        }
        return rangoSprite;
    }
    public void Volver()
    {
        DBManager.LoggedOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    } 

    //Funciones de sonido llamadas a través del mouse.
    public void ChangeOnPointerEnter()
    {
        audioManager.Play("HoverButton",1f,0.15f);
    }

    public void ChangeOnPointerClick()
    {      
        audioManager.Play("Back",0.5f,0.15f);  
    }

    public void OnPointerClickPartida()
    {      
        audioManager.Play("PartidaSelected",0.5f,0.15f);  
    }   
}
