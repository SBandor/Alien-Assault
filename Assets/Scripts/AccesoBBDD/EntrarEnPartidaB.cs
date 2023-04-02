using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrarEnPartidaB : MonoBehaviour
{
     public static void EntrarEnPartida()
    {
         if(DBManager.partidaIDA !=0)
         {
            DBManager.actualpartidaID= DBManager.partidaIDB;
            UnityEngine.SceneManagement.SceneManager.LoadScene(4);
         }
    } 
}
