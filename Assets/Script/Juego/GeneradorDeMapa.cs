using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;


public class GeneradorDeMapa : MonoBehaviour
{
   
    public Tilemap tilemap;
    [Header("Lista de suelo a utilizar para cada nivel")]
    public Sprite[] terreno; // Sprite que vamos a pitar para el suelo 
    [Header("Lista de trampa a utilizar para cada nivel")]
    public GameObject[] trampa;    // Tu GameObject trampa con la animación y el  script de daño 
    [Header("Lista de obstaculo a utilizar para cada nivel")]
    public GameObject[] obstaculo; // Tu GameObject de muro
    [Header("Objecto de enemigos")]
    public GameObject enemigos_mele;

    public GameObject enemigos_rango;

    public GameObject boss;
    public Transform player;




    [Header("Dimensiones y Ruido")]
    public int ancho = 40;
    public int alto = 40;
    public float escala = 0.1f; 

    private Transform contenedorDeObjetos;
    private List<GameObject> trampasInstanciadas = new List<GameObject>();

    void Start() {
        // Creamos un objeto para organizar los GameObjects en la jerarquía
        contenedorDeObjetos = new GameObject("ContenedorDeMapa").transform;
        contenedorDeObjetos.parent = this.transform;

        ConfigurarTilemap();
        GenerarMapa();

    }

    void ConfigurarTilemap() {
        if (tilemap != null) {
            TilemapRenderer renderer = tilemap.GetComponent<TilemapRenderer>();
            if (renderer != null) renderer.sortingOrder = -2; // Más atrás que los GameObjects
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            GenerarMapa();
        }
    }

    public void GenerarMapa() {
        //  Limpiamos todo lo anterior
        lipiarMapa();
        int level = GameManager.Instance.levelActual - 1;
        var configActual = GameManager.Instance.GetLevel(level);

        // Segun el nivel actual, se genera el mapa con los sprites y prefabs correspondientes
        crearMapa(terreno[level], obstaculo[level], trampa[level]);

        switch (level)
        {
            case 0:
                GenerarEnemigos(enemigos_mele, configActual.melee);
                break;
            case 1:
                GenerarEnemigos(enemigos_mele, configActual.melee);
                GenerarEnemigos(enemigos_rango, configActual.rango);
                break;
            case 2:
                GenerarEnemigos(enemigos_mele, configActual.melee);
                GenerarEnemigos(enemigos_rango, configActual.rango);
                break;
            case 3:
                GenerarJefe();
                break;

        }


    }

    void lipiarMapa() {
        tilemap.ClearAllTiles();
        foreach (Transform hijo in contenedorDeObjetos) {
            Destroy(hijo.gameObject);
        }
    }

    void crearMapa(Sprite terreno, GameObject obstaculo, GameObject trampa)
    {
        float seedX = Random.Range(-99999f, 99999f);
        float seedY = Random.Range(-99999f, 99999f);

        int inicioX = -ancho / 2;
        int inicioY = -alto / 2;

        // Crear el Tile de terreno una sola vez en memoria
        Tile tileSuelo = ScriptableObject.CreateInstance<Tile>();
        tileSuelo.sprite = terreno;

        for (int x = 0; x < ancho; x++)
        {
            for (int y = 0; y < alto; y++)
            {
                int posWorldX = inicioX + x;
                int posWorldY = inicioY + y;

                // Posición central para el GameObject (Tilemap usa esquinas, GameObject usa centros)
                Vector3 posCentro = new Vector3(posWorldX + 0.5f, posWorldY + 0.5f, 0);


                // Lógica 
                bool esBorde = (x == 0 || x == ancho - 1 || y == 0 || y == alto - 1);
                float ruido = Mathf.PerlinNoise((posWorldX + seedX) * escala, (posWorldY + seedY) * escala);

                if (esBorde)
                {
                    // Pintar el tile del borde en el Tilemap en la posición del mundo
                    Instanciar(obstaculo, posCentro);
                }
                else
                {
                    if (ruido < 0.65f)
                    {
                        // Pintar el tile de suelo en el Tilemap en la posición del mundo
                        tilemap.SetTile(new Vector3Int(posWorldX, posWorldY, 0), tileSuelo);
                    }
                    else
                    {
                        //  Evitar que trapas salgan cerca del jugador
                        if (CercaDelPlayer(posCentro))
                        {
                            tilemap.SetTile(new Vector3Int(posWorldX, posWorldY, 0), tileSuelo);

                        }

                        // Pintar el tile de trapa en el Tilemap en la posición del mundo
                        Instanciar(trampa, posCentro);
                        GameObject t = Instantiate(trampa, posCentro, Quaternion.identity, contenedorDeObjetos);
                        trampasInstanciadas.Add(t);
                    }
                }
            }
        }
    }

    void Instanciar(GameObject prefab, Vector3 posicion)
    {
        if (prefab != null)
        {
            GameObject nuevoObj = Instantiate(prefab, posicion, Quaternion.identity);
            nuevoObj.transform.parent = contenedorDeObjetos;
        }
    }


    void GenerarEnemigos(GameObject enemigo, int cantidad)
    {
       
        int generados = 0;
        int intentos = 0;

        while (generados < cantidad && intentos < cantidad * 10)
        {
            intentos++;

            int x = Random.Range(-ancho / 2, ancho / 2);
            int y = Random.Range(-alto / 2, alto / 2);

            Vector3 pos = new Vector3(x + 0.5f, y + 0.5f, 0);

            // evitar bordes
            bool esBorde = (x == -ancho / 2 || x == ancho / 2 - 1 ||
                            y == -alto / 2 || y == alto / 2 - 1);

            if (esBorde) continue;

            //  Evitar que enemigos salgan cerca del jugador
            if (CercaDelPlayer(pos)) continue;
            //  Evitar que enemigos salgan encima de una trapa o cerca de ella  
            if (EstaOcupado(pos) ) continue;

            GameObject enemigoPrefab = enemigo;

            Instantiate(enemigoPrefab, pos, Quaternion.identity, contenedorDeObjetos);

            generados++;
        }
    }

    bool CercaDelPlayer(Vector3 pos)
    {
        return Vector3.Distance(pos, player.position) < 3f;
    }


    bool EstaOcupado(Vector3 pos)
    {
        foreach (GameObject t in trampasInstanciadas)
        {
            if (Vector3.Distance(pos, t.transform.position) < 3f)
                return true;
        }
        return false;
    }


    void GenerarJefe()
    {
        Vector3 centro = Vector3.zero;
        Instantiate(boss, centro, Quaternion.identity, contenedorDeObjetos);


    }



}