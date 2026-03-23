using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using TMPro;           

public class SelectorPersonajes : MonoBehaviour
{
    [Header("Referencias UI")]
    public Button botonPlay;       // El botón de Play que empieza desactivado

    public Image pj;

    public GameObject Image_text_de;
   

    public TMP_Text textoDificultad;

    public Sprite[] pjSprite;

    private string[] dificultad = { "Dificultad: Easy", "Dificultad: Normal", "Dificultad: Hard", "Dificultad: Expert" };

    void Start()
    {
        
        pj.sprite = pjSprite[0];
        textoDificultad.text = dificultad[0];
        textoDificultad.text = dificultad[0];
        Image_text_de.gameObject.SetActive(false);        
        
    }

    // Este método lo llamarán los 4 botones
    public void SeleccionarPJ(int id)
    {
        Image_text_de.SetActive(false);        
        pj.sprite = pjSprite[id];
        textoDificultad.text = dificultad[id];

        if (id == 0)
        {
            botonPlay.gameObject.SetActive(true);
        }
        else
        {
            botonPlay.gameObject.SetActive(false);
            Image_text_de.SetActive(true);
        }
    }

    public void IrAJugar()
    {
        // Carga la escena de carga
        SceneManager.LoadScene(2);
    }

    public void IrATras()
    {
        // Carga la escena 1. 
         SceneManager.LoadScene(0);
    }
}