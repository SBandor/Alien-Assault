using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioManager audioManager;
    public Game game;
    public GameObject bala;
    public GameObject ruedas;
    private float speed;
    private float cadenciaTiro=0.5f;
    private float cadenciaActual;
    private bool canShoot;
   
    private int vidas = 3;
    private int vidasActuales;
    
    public void SetVidas(int v){vidas=v;}
    public int GetVidas(){return vidas;}
    public int GetVidasActuales(){return vidasActuales;}
    void Start()//Se llama una sola vez. (De Unity)
    {
        game= GameObject.Find("Canvas").GetComponent<Game>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        vidasActuales=vidas;
    }
   
    void Update()//Se llama una vez por frame. (De Unity)
    {
        speed= game.NormalGameSpeed();
        vidasActuales=vidas;
        //Input teclas de movimiento
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {      
            transform.Translate(Vector3.left *speed * Time.deltaTime);     
        }
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {      
            transform.Translate(Vector3.right * speed * Time.deltaTime);     
        }
        if(!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) &&
           !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            ruedas.GetComponent<Animator>().SetBool("Running",false);
        }
        else
        {        
            ruedas.GetComponent<Animator>().SetBool("Running",true);
        }
        //Input teclas de disparo
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && canShoot)
        {
            audioManager.Play("PlayerShot",0.5f,0.12f);
            GameObject balaInstanciada = Instantiate(bala, transform);
            balaInstanciada.transform.parent=null; //Desenganchamos el gameobject de su parent para que pueda moverse independiente.     
            cadenciaActual=cadenciaTiro;     
            canShoot=false;
        }
        StartCoroutine(CanShoot());      

        if(vidasActuales==0) //Si se acaban las vidas se llama al final del juego.
        {
            game.AliensInvaden();
            game.RestarPuntosMuertePlayer();
        }
    }

    IEnumerator CanShoot ()
    {
        cadenciaActual-=Time.deltaTime;
        if(cadenciaActual<=0)
        {
            canShoot=true;               
        }
        yield return canShoot;
    }

    public void PlayerDead()
    {
        audioManager.Play("PlayerDead",0.5f,0.15f);
        Destroy(gameObject);
    }
}
