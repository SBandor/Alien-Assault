using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    AudioManager audioManager;
    Game game; //Referencia al script Game que controla el juego.
    public GameObject bala; //Referencia a la bala que se instancia al disparar.
    private float velocidadEspera=1.2f; //Tiempo que tarda esperando a moverse.
    private float velocidadEsperaActual; //Tiempo que tarda esperando a moverse en cada frame.
    private float velocidadMovimiento=0.1f; //Tiempo que dura moviéndose.
    private float velocidadMovimientoActual; //Tiempo de duración del movimiento en cada frame.
    private bool canMove; //Interruptor que permite moverse en función de los timers de arriba.
    private float speed; //Velocidad de movimiento dada por el script Game.
    private int direccion; //Dirección de movimiento dada por el script Game.
    private float cadenciaTiro=4f; //Tiempo que pasa entre disparos.
    private float cadenciaActual; //Tiempo entre disparos por cada frame.
    private bool canShoot; //Interruptor que permite disparar en función de los timers de arriba.
    private bool fuegoAliado; //Interruptor que permite disparar en función de un Raycast.
    
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
        //Recibimos velocidad de juego y dirección de movimiento de aliens desde Game
        speed= game.NormalGameSpeed();
        direccion = game.GetDireccion();
        //Comprobamos que el alien no toca los márgenes de la pantalla.
        if(direccion==0 && transform.position.x >= 0.68f)
        {       
            game.SetDireccion(1);
            game.BajarUnaFilaTodos();                            
        }
        else if ( direccion== 1 && transform.position.x <=-0.72f)
        {
            game.SetDireccion(0);
            game.BajarUnaFilaTodos();
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
        FuegoAliado();
        if(canShoot && !fuegoAliado)
        {              
            game.alienReadys.Add(gameObject); //Se añade a la lista de aliens listos para disparar que guarda Game.
            cadenciaActual=cadenciaTiro; //Se resetea el timer de disparo.                      
        }    

        if(transform.position.y <= 0.5f)
        {
            game.AliensInvaden();
        }  
    }
    
    private void FuegoAliado()
    {
        //Rayo desde cada alien para comprobar si hay aliados debajo.
        RaycastHit2D[] enemyHit = Physics2D.RaycastAll(transform.position+new Vector3(0,-0.02f,0),Vector3.down,0.7f); 
        //Debug.DrawRay((transform.position+new Vector3(0,-0.02f,0)),Vector3.down,Color.cyan,0.001f);         
        foreach(RaycastHit2D hit in enemyHit)
        {
            if(hit.transform.tag=="Enemy")
            {
                fuegoAliado=true; 
            }   
            if(enemyHit.Length<2 && hit.transform.tag!="Enemy")
            {
                fuegoAliado=false;
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
            game.AliensInvaden();                    
        }
        else if(col.gameObject.tag=="Suelo") //Si colisiona con el suelo, desactiva su collider para pasar.
        {
            GetComponent<Collider2D>().enabled=false;
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
            if(direccion == 1)
            {
                transform.Translate(Vector3.left*speed*Time.deltaTime);         
            }
            else if (direccion == 0)
            {
                transform.Translate(Vector3.right*speed*Time.deltaTime);         
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
