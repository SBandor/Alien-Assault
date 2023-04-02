using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackgroundMusic : MonoBehaviour
{
    Game game;
    string sceneName;
    AudioSource backMusicSource;
    Sound song;
    AudioManager audioManager;
    private void Start()
    {
        backMusicSource = gameObject.AddComponent<AudioSource>();
        audioManager = GetComponent<AudioManager>();
    }
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("GameController") && game ==null)
        {
            game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();  //Busca ref. del script Game.  
        }
        //Si la escena ha cambiado y es parte del menú principal entonces pongo música de menú, sino música normal.
        if(sceneName!= "gameMainMenu")
        {       
            song = audioManager.GetSoundByName("MenuMusic");
            sceneName = SceneManager.GetActiveScene().name;            
        }
        else 
        { 
            if(!game.GetIsBoss())
            {
                song = audioManager.GetSoundByName("NormalMusic");
                sceneName = SceneManager.GetActiveScene().name; 
            }
            else
            {
                song = audioManager.GetSoundByName("BossMusic");
                sceneName = SceneManager.GetActiveScene().name;
            }     
        }   
        
        if(backMusicSource.clip != song.clip)
        {
            backMusicSource.clip = song.clip;
            backMusicSource.volume=0.2f;
            backMusicSource.Play();
        }
        backMusicSource.loop = true;
    }
}