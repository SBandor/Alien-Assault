using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class TabEntre3 : MonoBehaviour
{
    public InputField firstField;
    public InputField secondField;
    public InputField thirdField;
    
    void Start() //Se llama una sola vez. (De Unity)
    {
        if(secondField==null && thirdField ==null && firstField==null) //Si no se han añadido campos, se destruye.
        {
            Destroy(this);
            return;
        }     
    }

    void Update() //Se llama una vez por frame. (De Unity)
    {
        // Si el campo está en focus y presiono tab, activo el focus del siguiente campo.
        if(firstField.isFocused && Input.GetKeyDown(KeyCode.Tab) )
        {
            secondField.ActivateInputField();       
        }
        else if(secondField.isFocused && Input.GetKeyDown(KeyCode.Tab) )
        {
            thirdField.ActivateInputField();      
        }
        else if (thirdField.isFocused && Input.GetKeyDown(KeyCode.Tab) )
        {
            firstField.ActivateInputField();    
        }
    }
}
