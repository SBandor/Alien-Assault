using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighlightOnHoverButton : MonoBehaviour, IPointerEnterHandler
{
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }
    public void OnPointerEnter(PointerEventData p) //Recibimos información del objeto al que apunta el mouse a través de la interfaz IPointerEnterHandler
    {
        if(p.pointerEnter.transform.parent.GetComponent<Button>().interactable) // Si el botón del objeto padre del objeto apuntado es interactivo, suena.
        {
            audioManager.Play("HoverButton",1f,0.15f);
        }     
    }
}
