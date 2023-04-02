using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    AudioManager audioManager;
    Game game; //Referencia al script Game que controla el juego.
    public GameObject bala; //Referencia a la bala que se instancia al disparar.
    public GameObject explosionBoss;
    private float velocidadEspera=0.5f; //Tiempo que tarda esperando a moverse.
    private float velocidadEsperaActual; //Tiempo que tarda esperando a moverse en cada frame.
    private float velocidadMovimiento=0.5f; //Tiempo que dura moviéndose.
    private float velocidadMovimientoActual; //Tiempo de duración del movimiento en cada frame.
    private bool canMove; //Interruptor que permite moverse en función de los timers de arriba.
    private float speed; //Velocidad de movimiento dada por el script Game.
    private int direccionX; //Dirección de movimiento dada por el script Game.
    private int direccionY;
    private float cadenciaTiro=4f; //Tiempo que pasa entre disparos.
    private float cadenciaActual; //Tiempo entre disparos por cada frame.
    private bool canShoot; //Interruptor que permite disparar en función de los timers de arriba.
    
    private float tiempoHastaFinDeBoos=60f;
    private bool finDeBoss;
    private int vidasBoss=5;

    public void RestarVidaBoss(){vidasBoss--;}

    public void SetVelocidadEspera(float f){velocidadEspera =f;}
    public float GetVelocidadEspera(){return velocidadEspera;}

     void Start() //Se llama una sola vez. (De Unity)
    {
        game= GameObject.Find("Canvas").GetComponent<Game>();  //Busca ref. del script Game.   
        cadenciaActual=cadenciaTiro; //Resetea los timers de disparo.
        audioManager = GameObject.FindObjectOfType<AudioManager>();     
    }

    void Update() //Se llama una vez por frame. (De Unity)
    {
        //Recibimos velocidad de juego
        speed= game.NormalGameSpeed();
        if ( direccionY== 1 && transform.position.y >=1f)
        {
            transform.Translate(Vector3.down*speed*Time.deltaTime);    
            direccionY=0;
        }
        else if(direccionY==0 && transform.position.y <=0.4f)
        {
            transform.Translate(Vector3.up*speed*Time.deltaTime);    
            direccionY=1;
        } 
        //Proceso de movimiento del alien.
        if(canMove)
        {          
            StartCoroutine(Mover()); 
            audioManager.Play("AlienMoving",0.5f,0.2f);     
        }
        else
        {                    
            StartCoroutine(Esperar());    
        }
        //Corutinas de comprobación de tiro y cadencia de tiro
        StartCoroutine(CanShoot());
        if(canShoot)
        {              
            Disparar();
            cadenciaActual=cadenciaTiro; //Se resetea el timer de disparo.                      
        }    
       
        if(vidasBoss==0){ Instantiate(explosionBoss, transform.position, transform.rotation);game.BossDead(); Destroy(gameObject);}
        else
        {
            tiempoHastaFinDeBoos-=Time.deltaTime;
            if(tiempoHastaFinDeBoos<=0)
            {
                finDeBoss=true;
            }   
            if(finDeBoss)
            {
                game.AliensInvaden();
            }
        }
    }
     
    public void Disparar()
    {
        audioManager.Play("AlienShot",0.5f,0.15f);
        GameObject balaInstanciada= Instantiate(bala, transform);
        balaInstanciada.transform.parent=null; //Desenganchamos el gameobject de su parent para que pueda moverse independiente.
        balaInstanciada.transform.position= balaInstanciada.transform.position+new Vector3(0,-0.02f,0); //Ajuste de posición del spawn.               
    }

     void OnCollisionEnter2D(Collision2D col)
    {      
        if(col.gameObject.tag=="Player")
        {
            audioManager.Play("PlayerDead",0.5f,0.15f);
            finDeBoss=true;                           
        }    
        else if(col.gameObject.tag=="Suelo" && finDeBoss)
        {
            finDeBoss=true;                           
        }    
    }
    IEnumerator CanShoot()
    {
        cadenciaActual-=Time.deltaTime;
        if(cadenciaActual<=0)
        {
            canShoot=true;                       
        }
        else
        {
            canShoot=false;
        }
        yield return canShoot;
    }
    IEnumerator Esperar()
    {
        velocidadEsperaActual-=Time.deltaTime;
        if(velocidadEsperaActual<=0)
        {
            direccionX= Random.Range(0,2);
            direccionY= Random.Range(0,2); 
            canMove=true; 
            velocidadEsperaActual=velocidadEspera;                        
        }           
        yield return canMove;         
    }
    IEnumerator Mover()
    {
        velocidadMovimientoActual-=Time.deltaTime;
        if(velocidadMovimientoActual>0)
        {         
            if(direccionX == 0 )
            {
                transform.Translate(Vector3.left*speed*Time.deltaTime);         
            }
            else if (direccionX == 1 )
            {
                transform.Translate(Vector3.right*speed*Time.deltaTime);            
            }      
           
            if (direccionY ==0)
            {
                transform.Translate(Vector3.down*speed*Time.deltaTime);            
            }   
            else if (direccionY ==1)
            {
                transform.Translate(Vector3.up*speed*Time.deltaTime);            
            }                         
        } 
        else
        {             
            canMove=false;
            velocidadMovimientoActual=velocidadMovimiento;
        }          
        yield return canMove;
    }   
}
