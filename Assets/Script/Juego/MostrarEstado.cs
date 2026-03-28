using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using System.Collections;

public class MostrarEstado : MonoBehaviour
{
    public TextMeshProUGUI textoNivel;

    void Start()
    {
        textoNivel.text = "" + GameManager.Instance.levelActual;


    }



}