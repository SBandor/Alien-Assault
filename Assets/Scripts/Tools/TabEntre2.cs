using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class TabEntre2 : MonoBehaviour
{
    public InputField nextField;
    InputField myField;
    
    void Start() //Se llama una sola vez. (De Unity)
    {
        if(nextField==null) //Si no se ha añadido un campo, se destruye.
        {
            Destroy(this);
            return;
        }
        myField= GetComponent<InputField>();     
    }

    void Update() //Se llama una vez por frame. (De Unity)
    {
        if(myField.isFocused && Input.GetKeyDown(KeyCode.Tab)) // Si el campo está en focus y presiono tab, activo el focus del siguiente campo.
        {
            nextField.ActivateInputField();           
        }     
    }
}
