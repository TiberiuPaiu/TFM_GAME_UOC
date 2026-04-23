using UnityEngine;
using UnityEngine.SceneManagement;
using System; 


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int levelActual;
    public int vidaJugador;
    public int cantidadEnemigos;
    public int cantidadEnemigosEliminados;

    public LevelDatabase baseDeDatosNiveles;

    public static event Action OnEnemyKilled;



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            levelActual = 1;
            vidaJugador = 100;
            cantidadEnemigosEliminados = 0;
            baseDeDatosNiveles = LevelDatabase.LoadFromJson();
            cantidadEnemigos = baseDeDatosNiveles.levels[0].melee + baseDeDatosNiveles.levels[0].rango;
        }
        else
        {
            Destroy(gameObject); 
        }
    }


    public void ComprobarFinNivel()
    {
        int restantes = cantidadEnemigos - cantidadEnemigosEliminados;

        if (restantes == 0)
        {
            cantidadEnemigosEliminados = 0;
            levelActual++;
            cantidadEnemigos = baseDeDatosNiveles.levels[levelActual-1].melee + baseDeDatosNiveles.levels[levelActual - 1].rango;
            SceneManager.LoadScene(2);
        }
    }


    public LevelDataJson GetLevel(int level)
    {
        LevelDataJson data = baseDeDatosNiveles.levels[level];
        return data;
    }

    public void EnemyKilled()
    {
        cantidadEnemigosEliminados= cantidadEnemigosEliminados+1;

        OnEnemyKilled?.Invoke(); //  usar  patron Observer para notificar
    }



}