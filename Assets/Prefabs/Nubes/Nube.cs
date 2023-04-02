using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nube : MonoBehaviour
{
    Game game;
    private float speed;
    private float timeToDestroy;
    
    void Start()//Se llama una sola vez. (De Unity)
    {
        game= GameObject.Find("Canvas").GetComponent<Game>();
        speed = Random.Range(0.05f,0.5f);
        timeToDestroy= Random.Range(10f,25f);
    }

    void Update() //Se llama una vez por frame. (De Unity)
    {
        // Movemos la nube hacia la derecha hasta que se autodestruya.
        transform.Translate(Vector3.right*game.SlowGameSpeed()*speed*Time.deltaTime);
        timeToDestroy-=Time.deltaTime;
        if(timeToDestroy<=0)
        {
            Destroy(gameObject);
        }
    }
}
