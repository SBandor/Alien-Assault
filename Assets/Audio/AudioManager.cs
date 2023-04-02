using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        //Reviso que no existe más de un AudioManager, cuando haya solo uno, ese no se destruye al cambiar de escena.
        AudioManager[] audioManagersClone = GameObject.FindObjectsOfType<AudioManager>();
        if(audioManagersClone.Length>1)
        {
            foreach(AudioManager audioM in audioManagersClone)
            {
                if(audioM != audioManagersClone[0])
                {
                    Destroy(audioM.gameObject);
                }     
            }  
        }
        else
        {
            DontDestroyOnLoad(this);
        }
        //Del array sounds, por cada sonido le añado un audioSource. Guardo sus valores en las variables del objeto sound.
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name, float newPitch, float newVolume)
    {
       Sound s =  System.Array.Find(sounds, sound=> sound.name ==name);
        s.source.pitch = newPitch;
        s.source.volume = newVolume;
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s =  System.Array.Find(sounds, sound=> sound.name ==name);
        s.source.Stop();
    }

    public void Loop(string name, bool b)
    {
        Sound s =  System.Array.Find(sounds, sound=> sound.name ==name);
        s.source.loop=b;
    }
    public void ChangeVolume( Sound s ,float newVolume)
    {
        s.source.volume = newVolume;
    }
    public Sound GetSoundByName(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        return s;
    }
}
