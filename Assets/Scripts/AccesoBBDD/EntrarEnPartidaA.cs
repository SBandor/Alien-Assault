using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrarEnPartidaA : MonoBehaviour
{
     public void EntrarEnPartida()
    {
         if(DBManager.partidaIDA !=0)
         {
            DBManager.actualpartidaID= DBManager.partidaIDA;
            UnityEngine.SceneManagement.SceneManager.LoadScene(4);
         }
    } 
}
