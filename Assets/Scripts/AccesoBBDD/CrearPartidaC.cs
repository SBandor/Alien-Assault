using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CrearPartidaC : MonoBehaviour
{
     public Button partida3Button;
   public void CrearPartida()
    {
        if(DBManager.partidaIDC==0)
        {
            StartCoroutine(NuevaPartidaC());
        }  
        else
        {
            EntrarEnPartidaB.EntrarEnPartida();    
        } 
        
        IEnumerator NuevaPartidaC()
        {
            WWWForm form = new WWWForm();
            form.AddField("name", DBManager.username);
       
            UnityWebRequest www = UnityWebRequest.Post("http://localhost/alienassault-sqlconnect/nuevapartida.php", form);
       
            yield return www.SendWebRequest();
            Debug.Log(www.downloadHandler.text);
                                                         
            if(www.downloadHandler.text[0] == '0')
            {                             
                DBManager.partidaIDC= int.Parse(www.downloadHandler.text.Split('\t')[11]);
                DBManager.rangoC= int.Parse(www.downloadHandler.text.Split('\t')[12]);
                DBManager.killScoreC= int.Parse(www.downloadHandler.text.Split('\t')[13]);
                DBManager.maxScoreC= int.Parse(www.downloadHandler.text.Split('\t')[14]);
                DBManager.oleadaC= int.Parse(www.downloadHandler.text.Split('\t')[15]);
            
                Debug.Log(DBManager.username+" ha creado una nueva partida en slot C."); 
                DBManager.actualpartidaID=DBManager.partidaIDC;
                      
            }
            else
            {
                Debug.Log("User login failed! Error #"+ www.downloadHandler.text);
            }
        }
    }
}
