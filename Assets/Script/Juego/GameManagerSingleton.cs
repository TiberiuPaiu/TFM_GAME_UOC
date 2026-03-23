using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int levelActual;
    public int vidaJugador;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            levelActual = 1;
            vidaJugador = 100;
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}