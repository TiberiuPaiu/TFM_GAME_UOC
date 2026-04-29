using System.Collections;          
using System.Collections.Generic;
using UnityEngine;
public class ControladorCamara : MonoBehaviour
{
    public Transform jugador;

    public float anchoMapa;
    public float altoMapa;

    public float camSize = 7f; // Orthographic Size

    void LateUpdate()
    {
        if (jugador == null) return;

        var mapa = GameManager.Instance.GetLevel(GameManager.Instance.levelActual - 1);
        float anchoMapa = mapa.ancho;
        float altoMapa  = mapa.alto;

        Vector3 posicionJugadorr = jugador.position;


        if (anchoMapa == 24f && altoMapa == 14f)
        {
            return;
        }

        if (anchoMapa == 72f && altoMapa == 42f)
        {
            float halfHeight = camSize;
            float halfWidth = camSize * Camera.main.aspect;

            float minX = -anchoMapa / 2 + halfWidth;
            float maxX = anchoMapa / 2 - halfWidth;

            float minY = -altoMapa / 2 + halfHeight;
            float maxY = altoMapa / 2 - halfHeight;

            float x = Mathf.Clamp(posicionJugadorr.x, minX, maxX);
            float y = Mathf.Clamp(posicionJugadorr.y, minY, maxY);

            transform.position = new Vector3(x, y, -10f);
            return;
        }
    }
}