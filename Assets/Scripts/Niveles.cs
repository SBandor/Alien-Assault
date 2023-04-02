using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Niveles : MonoBehaviour
{
    public GameObject fila1;
    public GameObject fila2;
    public GameObject fila3;
    public GameObject fila4;
    public GameObject fila5;

    public GameObject alien1;
    public GameObject alien2;
    public GameObject alien3;
    public GameObject alien4;

    public GameObject player;

    private int columns = 12;

    public void CargarNivel1()
    {
        Debug.Log("Cargando nivel 1");
        if(!GameObject.FindGameObjectWithTag("Player"))
        {
            Instantiate(player,transform); //Instanciamos al jugador en la partida.
        }
       
        //Por cada fila se instancian la cantidad determinada de aliens según el número de columnas.
        for(int i=0;i<=columns;i++)
        {
            GameObject alien = Instantiate(alien4,fila1.transform);
            alien.name="Alien"+i;
            
            if(i>0)
            {
                alien.transform.position= new Vector3(GameObject.Find("Alien"+(i-1)).transform.position.x+0.09f, 
                fila1.transform.position.y,fila1.transform.position.z); //Movemos el alien a su derecha de su clon anterior.
            }        
        }

        for(int i=0;i<=columns;i++)
        {
            GameObject alien = Instantiate(alien2,fila2.transform);
            alien.name="Alien"+i;
            
            if(i>0)
            {
                alien.transform.position= new Vector3(GameObject.Find("Alien"+(i-1)).transform.position.x+0.09f,
                fila2.transform.position.y,fila2.transform.position.z);
            }          
        }

        for(int i=0;i<=columns;i++)
        {
            GameObject alien = Instantiate(alien3,fila3.transform);
            alien.name="Alien"+i;
            
            if(i>0)
            {
                alien.transform.position= new Vector3(GameObject.Find("Alien"+(i-1)).transform.position.x+0.09f,
                fila3.transform.position.y,fila3.transform.position.z);
            }          
        }

        for(int i=0;i<=columns;i++)
        {
            GameObject alien = Instantiate(alien2,fila4.transform);
            alien.name="Alien"+i;
            
            if(i>0)
            {
                alien.transform.position= new Vector3(GameObject.Find("Alien"+(i-1)).transform.position.x+0.09f,
                fila4.transform.position.y,fila4.transform.position.z);
            }         
        }

        for(int i=0;i<=columns;i++)
        {
            GameObject alien = Instantiate(alien1,fila5.transform);
            alien.name="Alien"+i;
            
            if(i>0)
            {
                alien.transform.position= new Vector3(GameObject.Find("Alien"+(i-1)).transform.position.x+0.09f,
                fila5.transform.position.y,fila5.transform.position.z);
            }          
        }
    }
}
