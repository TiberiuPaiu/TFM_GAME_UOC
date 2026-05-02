using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BarraVida : MonoBehaviour
{
    public Image baraDeVida;

    private float vidaActual;
    private float vidaMaxima = 100f;
    public TextMeshProUGUI textoVida;

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

            textoVida.text = vidaActual.ToString() + " /" + vidaMaxima.ToString();

            // Rojo si la vida llega a 25
            if (vidaActual <= 25f)
            {
                textoVida.color = Color.red;
            }
            else
            {
                textoVida.color = Color.white;
            }

        }
    }
}