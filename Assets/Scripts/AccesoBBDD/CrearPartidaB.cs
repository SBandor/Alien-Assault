using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CrearPartidaB : MonoBehaviour
{
     public Button partida2Button;
    public void CrearPartida()
    {       
        if(DBManager.partidaIDB ==0) // Si no hay partida en el slot.
        {
            StartCoroutine(NuevaPartidaB());
        }
        else
        {
            EntrarEnPartidaB.EntrarEnPartida();    
        }
        
        IEnumerator NuevaPartidaB()
        {
            WWWForm form = new WWWForm();
            form.AddField("name", DBManager.username);
       
            UnityWebRequest www = UnityWebRequest.Post("http://localhost/alienassault-sqlconnect/nuevapartida.php", form);
       
            yield return www.SendWebRequest();
            Debug.Log(www.downloadHandler.text);
                                                         
            if(www.downloadHandler.text[0] == '0') // 0 = Todo correcto. Entonces guardamos los siguientes carácteres en DBManager.
            {                             
                DBManager.partidaIDB= int.Parse(www.downloadHandler.text.Split('\t')[6]);
                DBManager.rangoB= int.Parse(www.downloadHandler.text.Split('\t')[7]);
                DBManager.killScoreB= int.Parse(www.downloadHandler.text.Split('\t')[8]);
                DBManager.maxScoreB= int.Parse(www.downloadHandler.text.Split('\t')[9]);
                DBManager.oleadaB= int.Parse(www.downloadHandler.text.Split('\t')[10]);
            
                Debug.Log(DBManager.username+" ha creado una nueva partida en slot B."); 
                DBManager.actualpartidaID=DBManager.partidaIDB;
                   
            }
            else
            {
                Debug.Log("User login failed! Error #"+ www.downloadHandler.text);
            }
        }   
    }
}
