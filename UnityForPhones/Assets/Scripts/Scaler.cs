using UnityEngine;
using System;
/// <summary>
/// Clase que se encarga de escalar el tablero dada una resolución base para que se respete esa relación <alto,ancho>.
/// </summary>
public class Scaler : MonoBehaviour
{

    [SerializeField]
    private BoardManager boardManager;
    [SerializeField]
    private RectTransform playSpace;

    /// <summary>
    /// Escala el tablero para que este quepa en el espacio reservado que tiene en la pantalla.
    /// </summary>
    /// <param name="clickTracker">Referencia al Tracker de la pulsaciíon del jugador para que este se escale también.</param>
    public void StartScaling(SpriteRenderer clickTracker)
    {
        //declaro cuanto tamaño de mapa es
        int tamHMap = 5;
        if (boardManager.GetHMatrix() > 5)
            tamHMap = 8;
        float aspectRatioH = tamHMap * 110;
        float aspectRatioW = 660;//este no cambia nunca

        //miro el espacio disponible para colocar los tiles
        float renderWidth = playSpace.rect.width;
        float renderHeight = playSpace.rect.height;
        
        float width = renderWidth / aspectRatioW;
        float height = renderHeight / aspectRatioH;

        if (height < width)
        {
            width = height * aspectRatioW;
            height = height * aspectRatioH;
        }
        else
        {
            height = width * aspectRatioH;
            width = width * aspectRatioW;
        }

        playSpace.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        playSpace.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        //miro que escalado necesito
        float margen = (float)Camera.main.pixelWidth / (float)Camera.main.pixelHeight * 1.4f;

        float escaladoFinal = playSpace.rect.width / renderWidth * margen;

        //redodeo decimales
        escaladoFinal = (float)Math.Round(escaladoFinal, 2);

        //escalamos x e y
        boardManager.transform.localScale = new Vector3(escaladoFinal, escaladoFinal, 1);
        clickTracker.transform.localScale = new Vector3(escaladoFinal, escaladoFinal, 1);
    }
}