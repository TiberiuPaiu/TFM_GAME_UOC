using UnityEngine;
using UnityEngine.Tilemaps;

public class GeneradorDeMapa : MonoBehaviour
{
    [Header("Referencias")]
    public Tilemap tilemap;
    public Sprite terreno; 

    [Header("Prefabs (Capa de Objetos)")]
    public GameObject trampa;    // Tu GameObject con script de daño o animación
    public GameObject obstaculo; // Tu GameObject de muro o piedra

    [Header("Dimensiones y Ruido")]
    public int ancho = 40;
    public int alto = 40;
    public float escala = 0.1f; 

    private Transform contenedorDeObjetos;

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
        // 1. Limpiamos todo lo anterior
        tilemap.ClearAllTiles();
        foreach (Transform hijo in contenedorDeObjetos) {
            Destroy(hijo.gameObject);
        }

        float seedX = Random.Range(-99999f, 99999f);
        float seedY = Random.Range(-99999f, 99999f);

        int inicioX = -ancho / 2;
        int inicioY = -alto / 2;

        // Crear el Tile de terreno una sola vez en memoria
        Tile tileSuelo = ScriptableObject.CreateInstance<Tile>();
        tileSuelo.sprite = terreno;

        for (int x = 0; x < ancho; x++) {
            for (int y = 0; y < alto; y++) {
                int posWorldX = inicioX + x;
                int posWorldY = inicioY + y;

                // Posición central para el GameObject (Tilemap usa esquinas, GameObject usa centros)
                Vector3 posCentro = new Vector3(posWorldX + 0.5f, posWorldY + 0.5f, 0);

                // 2. Pintar siempre suelo debajo
                tilemap.SetTile(new Vector3Int(posWorldX, posWorldY, 0), tileSuelo);

                // 3. Lógica de Bordes y Ruido
                bool esBorde = (x == 0 || x == ancho - 1 || y == 0 || y == alto - 1);
                float ruido = Mathf.PerlinNoise((posWorldX + seedX) * escala, (posWorldY + seedY) * escala);

                if (esBorde) {
                    Instanciar(obstaculo, posCentro);
                } else {
                    if (ruido > 0.75f) {
                        Instanciar(obstaculo, posCentro);
                    } else if (ruido > 0.6f) {
                        Instanciar(trampa, posCentro);
                    }
                }
            }
        }
    }

    void Instanciar(GameObject prefab, Vector3 posicion) {
        if (prefab != null) {
            GameObject nuevoObj = Instantiate(prefab, posicion, Quaternion.identity);
            nuevoObj.transform.parent = contenedorDeObjetos;
        }
    }
}