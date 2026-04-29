using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class GameOverController : MonoBehaviour
{
    public TextMeshProUGUI textoNivel;
    public TextMeshProUGUI textoTiempo;

    public float tiempoEspera = 3f;

    void Start()
    {
        
        textoNivel.text = "Nivel alcanzado: " + GameManager.Instance.levelActual;

        //float tiempo = GameManager.Instance.tiempoTotal;
        //textoTiempo.text = "Tiempo: " + FormatearTiempo(tiempo);

        // Esperar y cambiar escena
        StartCoroutine(VolverAlMenu());
    }

    System.Collections.IEnumerator VolverAlMenu()
    {
        yield return new WaitForSeconds(tiempoEspera);

        SceneManager.LoadScene(1); // escena de carga
    }

    string FormatearTiempo(float tiempo)
    {
        int minutos = Mathf.FloorToInt(tiempo / 60f);
        int segundos = Mathf.FloorToInt(tiempo % 60f);

        return minutos.ToString("00") + ":" + segundos.ToString("00");
    }

    public void Exit()
    {
        SceneManager.LoadScene(1);
    }


}