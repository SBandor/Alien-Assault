using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveAlien : MonoBehaviour
{
    AudioManager audioManager;
    Game game; //Referencia al script Game que controla el juego.
    private float speed; //Velocidad de movimiento dada por el script Game.  
     void Start() //Se llama una sola vez. (De Unity)
    {
        game= GameObject.Find("Canvas").GetComponent<Game>();  //Busca ref. del script Game.        
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        audioManager.Play("Engine",0.25f,0.15f);
        audioManager.Loop("Engine",true); 
    }

    void Update() //Se llama una vez por frame. (De Unity)
    {               
        //Recibimos velocidad de juego y movemos la nave hasta el margen opuesto. Cuando llega se autodestruye.
        speed= game.NormalGameSpeed();                         
        transform.Translate(Vector3.left*speed*Time.deltaTime);                                            
        if(transform.position.x <=-2f)
        {
            audioManager.Stop("Engine"); 
            audioManager.Loop("Engine",false);
            Destroy(gameObject);
        }                 
    }
}
