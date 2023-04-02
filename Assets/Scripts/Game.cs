using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Game : MonoBehaviour
{
    private AudioManager audioManager;
    public GameObject gameContext;
    public Camera cam3D;
    public Camera cam2D;
    private PlayerController playerController;
    public Sprite playerSprite;
    public Sprite mejoraTanque1;
    public Sprite mejoraTanque2;
    public Text vidasActuales;
    public Text playerDisplay;
    public Text rangoDisplay;
    public Text killScoreDisplay;
    public Text maxScoreDisplay;
    public Text oleadaDisplay;
    public Text partidaDisplay;

    public  List<GameObject> alienReadys = new List<GameObject>();
    public  int aliensDead;

    public GameObject menuFinalPartida;
    public GameObject menuSiguienteOleada;
    private float tiempoHastaReset=2f;
    private bool tryAgain;
    
    public GameObject filaEspecial;
    public GameObject naveAlien;
    private float tiempoHastaNaveAlien;
    private float timerNaveAlien;

    public GameObject boss1;
    public GameObject boss2;
    public GameObject boss3;
    public GameObject boss4;
    private GameObject boss;

    private bool estaElBoss;
    public bool GetIsBoss(){return estaElBoss;}
    public void SetIsBoss(bool b){estaElBoss=b;}
   
    public GameObject muroBossIzq;
    public GameObject muroBossDer;

    private  float gameSpeed;
    private int direccion;
    public float GetGameSpeed() { return gameSpeed;}
    public float NormalGameSpeed(){gameSpeed=0.45f; return gameSpeed;}
    public float FastGameSpeed(){gameSpeed=1;return gameSpeed;}
    public float SlowGameSpeed(){gameSpeed=0.2f; return gameSpeed;}

    private int puntosAcumulados;
    private int maxPuntosAcumulados;
    private int puntosActuales; //Usada para mantener los puntos y comparar para subir de rango.
    private int rangoAcumulado=1;
    private bool cambioRango;
    
    private void Awake() //Se llama al cargar la instancia del script.
    {
       
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        Camera.SetupCurrent(cam3D);
        if(DBManager.username == null && DBManager.partidaIDA !=0 && DBManager.partidaIDB !=0 && DBManager.partidaIDC !=0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        //Textos de los displays
        playerDisplay.text= "Player: "+ DBManager.username;
        partidaDisplay.text= "Partida #"+ DBManager.actualpartidaID; 
        
        oleadaDisplay.text="Oleada: "+DBManager.actualpartidaOleada;
        if(DBManager.actualpartidaID == DBManager.partidaIDA)
        {
            rangoDisplay.text= "Rango: "+ DBManager.rangoA;  
            killScoreDisplay.text= "Kill Score: "+ DBManager.killScoreA;  
            maxScoreDisplay.text= "Max. Score: "+ DBManager.maxScoreA; 
        }
        else if (DBManager.actualpartidaID == DBManager.partidaIDB)
        {
            rangoDisplay.text= "Rango: "+ DBManager.rangoB;  
            killScoreDisplay.text= "Kill Score: "+ DBManager.killScoreB; 
            maxScoreDisplay.text= "Max. Score: "+ DBManager.maxScoreB; 
        }
        else if(DBManager.actualpartidaID == DBManager.partidaIDC)
        {
            rangoDisplay.text= "Rango: "+ DBManager.rangoC; 
            killScoreDisplay.text= "Kill Score: "+ DBManager.killScoreC; 
            maxScoreDisplay.text= "Max. Score: "+ DBManager.maxScoreC;  
        }   

        //Se cargan los aliens, se referencia el player instanciado y se resetea el timer de la nave alien.
        muroBossDer.GetComponent<Collider2D>().enabled=false;
        muroBossIzq.GetComponent<Collider2D>().enabled=false;
        CargarNivel();
  
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        tiempoHastaNaveAlien=Random.Range(20f,30f); // Se establece un timer random para la nave alien.
        timerNaveAlien=tiempoHastaNaveAlien;
    }

 void Update()//Se llama una vez por frame. (De Unity)
    {
        CalcularRangoYMaxPts();
        rangoDisplay.text= "Rango: "+rangoAcumulado.ToString();
        killScoreDisplay.text= "Kill Score: "+puntosAcumulados.ToString();
        oleadaDisplay.text="Oleada: "+DBManager.actualpartidaOleada;
        vidasActuales.text= "x "+playerController.GetVidasActuales(); //Display de vidas del player.

        if(playerController.GetVidasActuales()>0) // Mientras el player esté vivo, probabilidad de nave alien
        {
            if(aliensDead<65 || GetIsBoss())
            {
                NaveAlien();
            }
        }
        if(alienReadys.Count>0) // 
        {
            AlienReadyToShoot();
        }
        if(tryAgain) // Si se ha presionado el boton try again se inicia el timer para iniciar el reseteo el nivel.
        {
            tiempoHastaReset-=Time.deltaTime;
            if(tiempoHastaReset<=0)
            {
                puntosAcumulados=0;
                muroBossDer.GetComponent<Collider2D>().enabled=false;
                muroBossIzq.GetComponent<Collider2D>().enabled=false;
                CargarNivel();
                playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                tryAgain=false;
            }
        }
        if(aliensDead==65) //Si se ha destruido a todos los aliens, aparece el boss.
        {
            muroBossDer.GetComponent<Collider2D>().enabled=true;
            muroBossIzq.GetComponent<Collider2D>().enabled=true;
            EnterBoss();
            aliensDead=0;
        }
    }

    public void CallSaveData()
    {
        if(DBManager.actualpartidaID==0) //Si se juega en modo offline se sale al menú principal, sino se sale al de partidas.
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);         
        }
        else
        {
            StartCoroutine(SavePlayerData());
        }    
    }

    IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);
       
        if(DBManager.actualpartidaID == DBManager.partidaIDA)
        {
            form.AddField("partidaID", DBManager.partidaIDA);

            DBManager.rangoA=rangoAcumulado;
            form.AddField("rango", DBManager.rangoA);
           
            form.AddField("killScore", puntosAcumulados);

            if(puntosAcumulados>DBManager.maxScoreA)
            {
                DBManager.maxScoreA=puntosAcumulados;
                form.AddField("maxScore", DBManager.maxScoreA);
            }
            else
            {
                form.AddField("maxScore", DBManager.maxScoreA);
            }

            if(DBManager.actualpartidaOleada>DBManager.oleadaA)
            {
                DBManager.oleadaA=DBManager.actualpartidaOleada;
                form.AddField("oleada",DBManager.oleadaA);
            }
            else
            {
                form.AddField("oleada",DBManager.oleadaA);
            }
           
            
        }
        else if (DBManager.actualpartidaID == DBManager.partidaIDB)
        {
            form.AddField("partidaID", DBManager.partidaIDB);

            DBManager.rangoB=rangoAcumulado;
            form.AddField("rango", DBManager.rangoB);

            form.AddField("killScore", puntosAcumulados);

            if(puntosAcumulados>DBManager.maxScoreB)
            {
                DBManager.maxScoreB=puntosAcumulados;
                form.AddField("maxScore", DBManager.maxScoreB);
            }
            else
            {
                form.AddField("maxScore", DBManager.maxScoreB);
            }

            if(DBManager.actualpartidaOleada>DBManager.oleadaB)
            {
                DBManager.oleadaB=DBManager.actualpartidaOleada;
                form.AddField("oleada",DBManager.oleadaB);
            }
            else
            {
                form.AddField("oleada",DBManager.oleadaB);
            }
        }
        else if(DBManager.actualpartidaID == DBManager.partidaIDC)
        {
            form.AddField("partidaID", DBManager.partidaIDC);

            DBManager.rangoC=rangoAcumulado;
            form.AddField("rango", DBManager.rangoC);

            form.AddField("killScore", puntosAcumulados);

            if(puntosAcumulados>DBManager.maxScoreC)
            {
                DBManager.maxScoreC=puntosAcumulados;
                form.AddField("maxScore", DBManager.maxScoreC);
            }
            else
            {
                form.AddField("maxScore", DBManager.maxScoreC);
            }

            if(DBManager.actualpartidaOleada>DBManager.oleadaC)
            {
                DBManager.oleadaC=DBManager.actualpartidaOleada;
                form.AddField("oleada",DBManager.oleadaC);
            }
            else
            {
                form.AddField("oleada",DBManager.oleadaC);
            }
        }
        DBManager.actualpartidaOleada=1;
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/alienassault-sqlconnect/savedata.php", form);
        yield return www.SendWebRequest();
        if(www.downloadHandler.text == "0")
        {
            Debug.Log("Game successfully saved!");
        }
        else
        {
            Debug.Log("Save process failed. Error # "+www.downloadHandler.text);
        }
        //Se carga la escena del menú de partidas
        DBManager.FueraDePartida();
        Camera.SetupCurrent(cam2D);
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);        
    }

    public void CalcularRangoYMaxPts()
    {
        if(puntosAcumulados>=maxPuntosAcumulados){maxPuntosAcumulados=puntosAcumulados;}

        if(puntosAcumulados>=puntosActuales+2355 && !cambioRango)
        {
            rangoAcumulado++;
            puntosActuales=puntosAcumulados;
            cambioRango=true;
        }
        else
        {
            cambioRango=false;
        }

        if(rangoAcumulado<5)
        {
            playerController.gameObject.GetComponent<SpriteRenderer>().sprite= playerSprite;
        }
        else if(rangoAcumulado<10)
        {
            playerController.gameObject.GetComponent<SpriteRenderer>().sprite= mejoraTanque1;
        }
        else
        {
            playerController.gameObject.GetComponent<SpriteRenderer>().sprite= mejoraTanque2;
        }
    }   

    public void AumentarPuntos(GameObject go)
    {
        switch(go.transform.parent.name)
        {
            case "Fila1": puntosAcumulados+=10; break;
            case "Fila2": puntosAcumulados+=20; break;
            case "Fila3": puntosAcumulados+=30; break;
            case "Fila4": puntosAcumulados+=35; break;
            case "Fila5": puntosAcumulados+=40; break;
            case "FilaEspecial": puntosAcumulados+=50; break;
        }
    }
    public void AumentarPuntosHitBoss()
    {
        puntosAcumulados+=100;
    }
    public void RestarPuntosDisparoAlien()
    {
        puntosAcumulados-=60;
    }
    public void RestarPuntosMuertePlayer()
    {
        puntosAcumulados-=500;
        if(puntosAcumulados<0){puntosAcumulados=0;}
    }
    public void SetDireccion(int i){direccion=i;}
    public int GetDireccion()
    {
        return direccion;
    }
    public void CargarNivel()
    {
        gameContext.GetComponent<Niveles>().CargarNivel1();           
    }

     public void BajarUnaFilaTodos()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
           enemy.transform.Translate(Vector3.down/25);
           enemy.GetComponent<Alien>().SetVelocidadEspera(enemy.GetComponent<Alien>().GetVelocidadEspera()-0.12f);
        }       
        audioManager.Play("AlienDown",1f,2f);
    }

    private void NaveAlien()
    {
        timerNaveAlien-=Time.deltaTime;
        if(timerNaveAlien<=0)
        {
            Instantiate(naveAlien,filaEspecial.transform);
            tiempoHastaNaveAlien=Random.Range(20f,30f);
            timerNaveAlien=tiempoHastaNaveAlien;
        }
    }
 
    public void AliensInvaden()
    {    
        //Se para el juego, se mata al jugador y se activa el menú de game over.
        gameSpeed=0;
    
        if(GameObject.FindGameObjectWithTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().PlayerDead();
        } 
        menuFinalPartida.gameObject.SetActive(true);
    }
    
    public void SumarDeadAlien()
    {
        aliensDead++;
    }

    private void EnterBoss()
    {
        SetIsBoss(true);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
          Destroy(enemy);
        }  
        switch(Random.Range(1,5))
        {
            case 1:boss= boss1; break;
            case 2:boss= boss2; break;
            case 3:boss= boss3; break;
            case 4:boss= boss4; break;
        }
        Instantiate(boss,gameContext.transform);
    }

    public void BossDead()
    {  
        menuSiguienteOleada.gameObject.SetActive(true);
        menuSiguienteOleada.transform.Find("NuevaOleada").GetComponent<Text>().text="La oleada "+(DBManager.actualpartidaOleada+1)+" se acerca..."; 
    }
    public void EmpezarOleada()
    {
        DBManager.actualpartidaOleada++;
        SetIsBoss(false);
        menuSiguienteOleada.SetActive(false);
        muroBossDer.GetComponent<Collider2D>().enabled=false;
        muroBossIzq.GetComponent<Collider2D>().enabled=false;
        CargarNivel();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if(DBManager.actualpartidaID== DBManager.partidaIDA)
        {
            DBManager.maxScoreA=puntosAcumulados;
           
        }
        else if(DBManager.actualpartidaID== DBManager.partidaIDB)
        {
            DBManager.maxScoreB=puntosAcumulados;
            
        }
        else if(DBManager.actualpartidaID== DBManager.partidaIDC)
        {
            DBManager.maxScoreC=puntosAcumulados;
            
        }
    }
    public void TryAgain()
    {           
        SetIsBoss(false);
       
       if(DBManager.actualpartidaID== DBManager.partidaIDA)
        {
            DBManager.maxScoreA=puntosAcumulados;
           
        }
        else if(DBManager.actualpartidaID== DBManager.partidaIDB)
        {
            DBManager.maxScoreB=puntosAcumulados;
           
        }
        else if(DBManager.actualpartidaID== DBManager.partidaIDC)
        {
            DBManager.maxScoreC=puntosAcumulados;
            
        }
        //Se desactiva el menú de game over, se destruyen los aliens restantes y se establece el timer para el reset.
        menuFinalPartida.gameObject.SetActive(false);
        Destroy(GameObject.FindGameObjectWithTag("Boss"));
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
          Destroy(enemy);
        }     
        tryAgain=true;
        tiempoHastaReset=2f;    
    }

    public  void AlienReadyToShoot() // Selección de tirador alien.
    {            
        int alienToShoot =Random.Range(0,12);
        foreach(GameObject al in alienReadys)
        {       
            if(alienReadys.IndexOf(al) == alienToShoot)
            {               
                al.GetComponent<Alien>().Disparar();                  
            }
        }   
        alienReadys.Clear(); 
    }       

}
