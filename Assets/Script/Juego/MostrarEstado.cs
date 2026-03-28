using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class MostrarEstado : MonoBehaviour
{
    public TextMeshProUGUI textoNivel;
    public TextMeshProUGUI textoTiempo;
    private float tiempoRestante = 180f; // 3 minutos en segundos

    void Start()
    {
        textoNivel.text = "" + GameManager.Instance.levelActual;

        ActualizarReloj(tiempoRestante);
    }

    void Update()
    {
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime; // Resta el tiempo que paso desde el utima vez 
        }
        else
        {
            tiempoRestante = 0;
            SceneManager.LoadScene(4);// Game Over 
        }

        ActualizarReloj(tiempoRestante);
    }

    void ActualizarReloj(float segundos)
    {
        if (segundos < 0) segundos = 0;

        // Calculamos minutos y segundos
        int min= Mathf.FloorToInt(segundos / 60);
        int sec = Mathf.FloorToInt(segundos % 60);

        // Dar el formato deseado 00:00
        textoTiempo.text = string.Format("{0:00}:{1:00}", min, sec);
    }


}