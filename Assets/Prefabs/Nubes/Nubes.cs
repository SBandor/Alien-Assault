using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nubes : MonoBehaviour
{
     public GameObject fila1;
    public GameObject fila2;
    public GameObject fila3;
    public GameObject fila4;
    public GameObject fila5;
    public GameObject fila6;

    private int columns;
    private float tiempoHastaNuevasNubes=15f;
    private float timerNubes;

    private float nubeSize;
    public GameObject nube;

    public void RandomNumberColumn(){ columns = Random.Range(1,6); }
   
    void Update() //Se llama una vez por frame. (De Unity)
    {
        //Cada paso de ciclo, instanciar nubes.
        timerNubes-=Time.deltaTime;
        if(timerNubes<=0)
        {
            instanciarNubes();
            timerNubes= tiempoHastaNuevasNubes;
        }
    }

    private void instanciarNubes()
    {
        Debug.Log("Cargando nubes");
        RandomNumberColumn(); //Se establece un número de nubes aleatorio.
        for(int i=0;i<=columns;i++)
        {
            GameObject nubeInstance = Instantiate(nube,fila1.transform);
            nubeInstance.name="Nube"+i;
            nubeInstance.transform.localScale= new Vector3(Random.Range(0.2f,0.7f), Random.Range(0.4f,1.0f)); //Alteramos el tamaño de la nube.
            if(i>0)
            {
                nubeInstance.transform.position= new Vector3(GameObject.Find("Nube"+(i-1)).transform.position.x+0.18f, 
                fila1.transform.position.y,fila1.transform.position.z); //Movemos cada nube al lado de su anterior.
            }        
        }
        RandomNumberColumn();
        for(int i=0;i<=columns;i++)
        {
            GameObject nubeInstance = Instantiate(nube,fila2.transform);
            nubeInstance.name="Nube"+i;
            nubeInstance.transform.localScale= new Vector3(Random.Range(0.2f,0.7f), Random.Range(0.4f,1.0f));
            if(i>0)
            {
                nubeInstance.transform.position= new Vector3(GameObject.Find("Nube"+(i-1)).transform.position.x+0.18f,
                fila2.transform.position.y,fila2.transform.position.z);
            }               
        }
        RandomNumberColumn();
        for(int i=0;i<=columns;i++)
        {
            GameObject nubeInstance = Instantiate(nube,fila3.transform);
            nubeInstance.name="Nube"+i;
            nubeInstance.transform.localScale= new Vector3(Random.Range(0.2f,0.7f), Random.Range(0.4f,1.0f));
            if(i>0)
            {
                nubeInstance.transform.position= new Vector3(GameObject.Find("Nube"+(i-1)).transform.position.x+0.18f,
                fila3.transform.position.y,fila3.transform.position.z);
            }             
        }
        RandomNumberColumn();
        for(int i=0;i<=columns;i++)
        {
            GameObject nubeInstance = Instantiate(nube,fila4.transform);
            nubeInstance.name="Nube"+i;
            nubeInstance.transform.localScale= new Vector3(Random.Range(0.2f,0.7f), Random.Range(0.4f,1.0f));
            if(i>0)
            {
                nubeInstance.transform.position= new Vector3(GameObject.Find("Nube"+(i-1)).transform.position.x+0.18f,
                fila4.transform.position.y,fila4.transform.position.z);
            }               
        }
        RandomNumberColumn();
        for(int i=0;i<=columns;i++)
        {
            GameObject nubeInstance = Instantiate(nube,fila5.transform);
            nubeInstance.name="Nube"+i;
            nubeInstance.transform.localScale= new Vector3(Random.Range(0.2f,0.7f), Random.Range(0.4f,1.0f));
            if(i>0)
            {
                nubeInstance.transform.position= new Vector3(GameObject.Find("Nube"+(i-1)).transform.position.x+0.18f,
                fila5.transform.position.y,fila5.transform.position.z);
            }               
        }
        RandomNumberColumn();
        for(int i=0;i<=columns;i++)
        {
            GameObject nubeInstance = Instantiate(nube,fila6.transform);
            nubeInstance.name="Nube"+i;
            nubeInstance.transform.localScale= new Vector3(Random.Range(0.2f,0.7f), Random.Range(0.4f,1.0f));
            if(i>0)
            {
                nubeInstance.transform.position= new Vector3(GameObject.Find("Nube"+(i-1)).transform.position.x+0.18f,
                fila6.transform.position.y,fila6.transform.position.z);
            }                 
        }
    }
}
