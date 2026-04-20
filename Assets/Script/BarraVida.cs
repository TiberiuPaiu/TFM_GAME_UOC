using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image baraDeVida;

    private float vidaActual;
    private float vidaMaxima = 100f;

    void Start()
    {
        // Configuramos el máximo del slider según la vida inicial
        if (GameManager.Instance != null)
        {
            vidaActual = GameManager.Instance.vidaJugador;
            ActualizarBarra();
        }
    }

    void Update()
    {
        // Mantenemos la barra sincronizada
        ActualizarBarra();
    }

    void ActualizarBarra()
    {
        if (GameManager.Instance != null)
        {
            vidaActual = GameManager.Instance.vidaJugador;
            baraDeVida.fillAmount = vidaActual / vidaMaxima;

        }
    }
}