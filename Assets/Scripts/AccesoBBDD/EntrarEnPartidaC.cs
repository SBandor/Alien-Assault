using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrarEnPartidaC : MonoBehaviour
{
     public static void EntrarEnPartida()
    {
         if(DBManager.partidaIDA !=0)
         {
            DBManager.actualpartidaID= DBManager.partidaIDC;
            UnityEngine.SceneManagement.SceneManager.LoadScene(4);
         }
    } 
}
