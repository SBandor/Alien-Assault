using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaPlayer : MonoBehaviour
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
       speed= game.NormalGameSpeed();
    }
    
    void Update()//Se llama una vez por frame. (De Unity)
    {
        speed=speed+0.005f; //Añadimos aceleración cada frame.
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        StartCoroutine(WaitToDestroy());
        if(autodestruccion){Destroy(gameObject);}
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        Instantiate(explosionBala, col.transform.position,col.transform.rotation);
        if(col.gameObject.tag=="Enemy")
        {
            game.AumentarPuntos(col.gameObject);
            game.SumarDeadAlien();
            audioManager.Play("AlienDead",1.5f,0.15f);
            Destroy(col.gameObject);          
            Destroy(gameObject);         
        }
        else if(col.gameObject.tag=="NaveAlien")
        {
            game.AumentarPuntos(col.gameObject);
            audioManager.Play("AlienDead",1.5f,0.15f);
            Destroy(col.gameObject);          
            Destroy(gameObject);   
        }
        else if(col.gameObject.tag=="Boss")
        {
            game.AumentarPuntosHitBoss();
            col.gameObject.GetComponent<Boss>().RestarVidaBoss();
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
