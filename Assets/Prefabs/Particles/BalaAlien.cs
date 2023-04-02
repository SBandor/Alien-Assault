using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaAlien : MonoBehaviour
{
     Game game;
    AudioManager audioManager;
     public GameObject explosionBala;
     private float tiempoHastaDestruccion = 4f;
     private bool autodestruccion;
     private float speed;
    
    void Start()//Se llama una sola vez. (De Unity)
    {
       game= GameObject.Find("Canvas").GetComponent<Game>();
       audioManager = GameObject.FindObjectOfType<AudioManager>();
    }
   
    void Update()//Se llama una vez por frame. (De Unity)
    {
        speed= game.SlowGameSpeed();
        transform.Translate(Vector3.down * speed *3* Time.deltaTime);
        StartCoroutine(WaitToDestroy());
        if(autodestruccion){Destroy(gameObject);}
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        Vector3 closestPoint = col.collider.ClosestPoint(transform.position);
        GameObject explosion = Instantiate(explosionBala, col.transform.position, col.transform.rotation);
        
        explosion.transform.position = new Vector3(closestPoint.x,closestPoint.y+0.06f, 1); //Posicionamos la explosión en el punto más cercano del impacto.
          
        if(col.gameObject.tag=="Player")
        {
            game.RestarPuntosDisparoAlien();
            audioManager.Play("Explosion",1f,0.2f);
            col.gameObject.GetComponent<PlayerController>().SetVidas(col.gameObject.GetComponent<PlayerController>().GetVidas()-1);         
            Destroy(gameObject);         
        }
        else if(col.gameObject.tag=="Suelo")
        {
            audioManager.Play("Explosion",0.5f,0.15f);
            Destroy(gameObject);    
        }
    }
    
    IEnumerator WaitToDestroy()
    {
        tiempoHastaDestruccion-=Time.deltaTime;
        if(tiempoHastaDestruccion<0)
        {
            autodestruccion=true;
        }
        yield return autodestruccion;
    }
}
