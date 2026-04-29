using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishedGame : MonoBehaviour
{
    public TextMeshProUGUI textoNivel;
    public TextMeshProUGUI textoTiempo;

    public float tiempoEspera = 8f;

    void Start()
    {

        //textoNivel.text = "Nivel alcanzado: " + GameManager.Instance.levelActual;


        // Esperar y cambiar escena
        StartCoroutine(VolverAlMenu());
    }

    System.Collections.IEnumerator VolverAlMenu()
    {
        yield return new WaitForSeconds(tiempoEspera);

        SceneManager.LoadScene(1); // escena de carga
    }

    public void Exit()
    {
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene(1);
    }


}