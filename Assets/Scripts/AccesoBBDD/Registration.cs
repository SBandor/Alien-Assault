using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class Registration : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;
    public InputField emailField;

    public Button submitButton;
   
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }
    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", emailField.text);
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
       
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/alienassault-sqlconnect/register.php", form);
       
        yield return www.SendWebRequest();
        
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);               
        } else 
        {
            Debug.Log(www.downloadHandler.text);
        }
        if (www.downloadHandler.text == "0")
        {
            Debug.Log("User created succesfully!");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
    
    public void VerifyInputs()
    {
        submitButton.interactable= (nameField.text.Length>=5 && passwordField.text.Length>=5&& emailField.text.Length>=5);
    }

    public void ToMainMenu()
    {
        audioManager.Play("Back",0.5f,0.15f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0); 
    }

    //Funciones de sonido llamadas a través del mouse.
    public void ChangeOnPointerEnter()
    {
        audioManager.Play("HoverButton",1f,0.15f);
    }

    public void ChangeOnPointerClick()
    {
        audioManager.Play("Back",0.5f,0.15f);  
    }

    public void OnPointerClickOnRegister()
    {
        if(submitButton.interactable)
        {
            audioManager.Play("Register",0.5f,0.15f);
        } 
    }
}
