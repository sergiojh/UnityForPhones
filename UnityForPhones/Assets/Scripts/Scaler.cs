using UnityEngine;
/// <summary>
/// Clase que se encarga de escalar el tablero dada una resolución base para que se respete esa relación <alto,ancho>.
/// </summary>
public class Scaler : MonoBehaviour
{

    [SerializeField]
    private BoardManager boardManager;

    private const int _defaultAncho = 720;
    private const int _defaultAlto = 1280;
    private const int _blueTopPart = 270;
    private const int _blueBottomPart = 230;

    private int _alto;


/// <summary>
/// Escala el tablero para que este quepa en el espacio reservado que tiene en la pantalla.
/// </summary>
/// <param name="clickTracker">Referencia al Tracker de la pulsaciíon del jugador para que este se escale también.</param>
    public void startScaling(SpriteRenderer clickTracker)
    {
        //sacamos la parte blanca
        _alto = _defaultAlto - _blueBottomPart - _blueTopPart;

        int width = Screen.width;
        int height = Screen.height;
        float factorScaladoY = 0;
        float increment = 0;
        if (width != _defaultAncho || height != _defaultAlto)
        {

            float factorDeEscalado = Screen.width / (float)_defaultAncho;

            int blueTopPartHeight = (int)(_blueTopPart * factorDeEscalado);
            int blueBottomPartHeight = (int)(_blueBottomPart * factorDeEscalado);
            height = height - blueTopPartHeight - blueBottomPartHeight;

            factorScaladoY = height / (float)(_alto);
            float factorScaladoX = width / (float)_defaultAncho;

            if (factorScaladoY < 0)
                factorScaladoY = 0;
            if (factorScaladoX < 0)
                factorScaladoX = 0;

            if (factorScaladoX > factorScaladoY)
                factorScaladoX = factorScaladoY;
            else if(factorScaladoX < factorScaladoY)
                factorScaladoY = factorScaladoX;
            if (factorScaladoX > 1.0f)
            {
                factorScaladoY -= 0.3f;
                factorScaladoX -= 0.3f;
                if (boardManager.getWMatrix() > 5)
                {
                    increment = 0.6f;
                }
                else if (boardManager.getWMatrix() <= 5)
                {
                    increment = 0.3f;
                }
                if (boardManager.getHMatrix() >= 5)
                    increment += 0.02f;
            }
            else
            {
                if (boardManager.getWMatrix() > 5)
                {
                    increment = 0.2f;
                }
                else if (boardManager.getWMatrix() <= 5)
                {
                    increment = 0.05f;
                }
                if (boardManager.getHMatrix() >= 5)
                    increment += 0.02f;
            }
            if (factorScaladoX != 0 && Screen.height > Screen.width)
                factorScaladoX -= increment;
            if (factorScaladoX < 0)
                factorScaladoX = 0.17f;
            Vector3 boardScale = new Vector3(boardManager.transform.localScale.x * factorScaladoX, boardManager.transform.localScale.y * factorScaladoX, 1);
            Vector3 clickScale = new Vector3(factorScaladoY + 0.2f, factorScaladoY + 0.2f, 1);
            boardManager.transform.localScale = boardScale;
            clickTracker.transform.localScale = clickScale;
        }
        else
        {
            if (boardManager.getWMatrix() > 5)
            {
                increment = 0.55f;
            }
            else if (boardManager.getWMatrix() == 5)
            {
                increment = 0.8f;
            }
            else
                increment = 1;

            Vector3 v = new Vector3(boardManager.transform.localScale.x * increment, boardManager.transform.localScale.y * increment, 1);
            boardManager.transform.localScale = v;
        }
    }
    
}