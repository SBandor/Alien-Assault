using UnityEngine.Audio;
using UnityEngine;

[System.Serializable] //Permitimos a Unity trabajar con esta información en el editor.
public class Sound 
{
    public string name;
    public AudioClip clip;
    [Range(0f,1f)] //Estos valores podrán ser editados en el editor de Unity.
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    [HideInInspector]
    public AudioSource source;

    public string GetName() { return name; }
}
