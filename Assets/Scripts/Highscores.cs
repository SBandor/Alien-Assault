using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Highscores : MonoBehaviour
{
    public GameObject ranking;
    public GameObject fichaJugador;

    public List<FichaJugador> fichasJugadores = new List<FichaJugador>(); 

    private void Start()
    {
        StartCoroutine(Highscore());
    }

    IEnumerator Highscore()
    {
        //Creación formulario con información para consulta.
        WWWForm form = new WWWForm();
       
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/alienassault-sqlconnect/highscores.php", form);
       
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
                                                         
        if(www.downloadHandler.text[0] == '0') //Si 0 en el primer char, entonces todo ha ido bien. 
        {       
            
            for(int j=0; j < www.downloadHandler.text.Split('\t').Length-1 ; j++) // Inicio en el tercer char que es el primer jugador, si < todos los chars menos los dos primeros, entonces creo un objeto ficha y sumo 4 para saltar al siguiente tramo de datos.
            {
                GameObject nuevaficha = Instantiate(fichaJugador,ranking.transform);
                nuevaficha.transform.Find("Text").GetComponent<Text>().text=www.downloadHandler.text.Split('\t')[j+1]+
                "       Rango: "+www.downloadHandler.text.Split('\t')[j+2]+
                "     Max. Score: "+www.downloadHandler.text.Split('\t')[j+3]+
                "     Max. Oleadas: "+www.downloadHandler.text.Split('\t')[j+4];
                
                j+=3;
            }     
        }
        else
        {
            Debug.Log("User login failed! Error #"+ www.downloadHandler.text);
        }
    }

    public void ToMainMenu()
    {
        //audioManager.Play("Back",0.5f,0.15f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0); 
    }
}
