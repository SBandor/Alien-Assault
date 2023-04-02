using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class Login : MonoBehaviour
{   
    //Referencias a botones, inputs, e imagenes
    public InputField nameField;
    public InputField passwordField;

    public Button submitButton;
    public Button mostrarPassButton;
    public GameObject ojo;
    public Sprite ojoCerrado;
    public Sprite ojoAbierto;
    private bool showingPassword; //Determina la visibilidad del password.

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    public void CallLogin()
    {
        StartCoroutine(LoginPlayer());
    }

    IEnumerator LoginPlayer()
    {
        //Creación formulario con información para consulta.
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/alienassault-sqlconnect/login.php", form);
       
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
                                                         
        if(www.downloadHandler.text[0] == '0') //Si 0 en el primer char, entonces todo ha ido bien. 
        {       
            //Copia de datos a DBManager
            DBManager.username = nameField.text;  
            DBManager.logros = int.Parse(www.downloadHandler.text.Split('\t')[16]);

            DBManager.partidaIDA= int.Parse(www.downloadHandler.text.Split('\t')[1]);
            DBManager.rangoA= int.Parse(www.downloadHandler.text.Split('\t')[2]);
            DBManager.killScoreA= int.Parse(www.downloadHandler.text.Split('\t')[3]);
            DBManager.maxScoreA= int.Parse(www.downloadHandler.text.Split('\t')[4]);
            DBManager.oleadaA= int.Parse(www.downloadHandler.text.Split('\t')[5]);

            DBManager.partidaIDB= int.Parse(www.downloadHandler.text.Split('\t')[6]);
            DBManager.rangoB= int.Parse(www.downloadHandler.text.Split('\t')[7]);
            DBManager.killScoreB= int.Parse(www.downloadHandler.text.Split('\t')[8]);
            DBManager.maxScoreB= int.Parse(www.downloadHandler.text.Split('\t')[9]);
            DBManager.oleadaB= int.Parse(www.downloadHandler.text.Split('\t')[10]);

            DBManager.partidaIDC= int.Parse(www.downloadHandler.text.Split('\t')[11]);
            DBManager.rangoC= int.Parse(www.downloadHandler.text.Split('\t')[12]);
            DBManager.killScoreC= int.Parse(www.downloadHandler.text.Split('\t')[13]);
            DBManager.maxScoreC= int.Parse(www.downloadHandler.text.Split('\t')[14]);
            DBManager.oleadaC= int.Parse(www.downloadHandler.text.Split('\t')[15]);

            //Carga de la siguiente escena (Cargar partida)
            audioManager.Play("Login",0.5f,0.15f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
            Debug.Log(DBManager.username + " logged in.");
        }
        else
        {
            Debug.Log("User login failed! Error #"+ www.downloadHandler.text);
        }
    }

    public void VerifyInputs() //Verificamos que hay contenido en los campos a rellenar
    {
        submitButton.interactable= (nameField.text.Length>=5 && passwordField.text.Length>=5);
    }

    public void ToMainMenu()
    {
        audioManager.Play("Back",0.5f,0.15f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);  
    }
    
    public void MostrarOcultarPassword()
    {
        showingPassword= !showingPassword;
        passwordField.ActivateInputField();  
    }
    void Update()//Se llama una vez por frame. (De Unity)
    {
        // Mostramos u Ocultamos el contenido del password.
        mostrarPassButton.interactable= (passwordField.text.Length>=1);
        if(showingPassword)
        {
            passwordField.contentType=InputField.ContentType.Standard;
            ojo.GetComponent<Image>().sprite= ojoAbierto;
        }
        else
        {
            passwordField.contentType=InputField.ContentType.Password;
            ojo.GetComponent<Image>().sprite=ojoCerrado;
        }
    }

    // Funciones de sonido llamadas a traves del mouse.
    public void ChangeOnPointerEnter()
    {
        audioManager.Play("HoverButton",1f,0.15f);
    }

    public void ChangeOnPointerClick()
    {
        audioManager.Play("Back",0.5f,0.15f);  
    }

    public void OnPointerClickPassword()
    {
        if(mostrarPassButton.interactable)
        {
            audioManager.Play("Password",0.5f,0.15f);
        }
    }
}
