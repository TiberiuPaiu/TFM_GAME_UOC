using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    // Este método se ejecutará al pulsar el botón "Play"
    public void BotonPlay()
    {
        // Carga la escena 1. 
        SceneManager.LoadScene(1); 
    }

    // Este método se ejecutará al pulsar el botón "Exit"
    public void BotonExit()
    {
        Debug.Log("Saliendo del juego..."); 
        Application.Quit();
    }
}