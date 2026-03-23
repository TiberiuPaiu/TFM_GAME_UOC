using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PantallaDeCarga : MonoBehaviour
{
    public TextMeshProUGUI textoCuentaAtras;
    public TextMeshProUGUI textoNivel;


    void Start()
    {
        Debug.Log("Nivel actual escena 2 - " + GameManager.Instance.levelActual); 
        StartCoroutine(CuentaAtras());
    }

    IEnumerator CuentaAtras()
    {
        textoNivel.text = "Level  - " + GameManager.Instance.levelActual;
        for (int i = 3; i > 0; i--)
        {
            textoCuentaAtras.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        textoCuentaAtras.text = "GO!";
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(3);
    }
}