using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{

    [SerializeField]
    private BoardManager boardManager;
    [SerializeField]
    private SpriteRenderer clickTraker;

    private const int _defaultAncho = 720;
    private const int _defaultAlto = 1280;
    private const int _blueTopPart = 270;
    private const int _blueBottomPart = 230;

    private int _alto;

    [SerializeField]
    private SpriteRenderer topPart;
    [SerializeField]
    private SpriteRenderer bottomPart;

    // Start is called before the first frame update
    public void startScaling()
    {
        if(topPart != null && bottomPart != null) //game scene
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

                
                if (boardManager.getWMatrix() > 5)
                {
                    increment = 0.6f;
                }
                else if (boardManager.getWMatrix() == 5)
                {
                    increment = 0.05f;
                }
                if (boardManager.getHMatrix() >= 5)
                    increment += 0.02f;
                //Debug.Log(factorScaladoX);
                if (factorScaladoX != 0 && Screen.height > Screen.width)
                    factorScaladoX -= increment;
                if (factorScaladoX < 0)
                    factorScaladoX = 0.17f;
                Debug.Log(factorScaladoX);
                Vector3 boardScale = new Vector3(boardManager.transform.localScale.x * factorScaladoX, boardManager.transform.localScale.y * factorScaladoX, 1);
                Vector3 clickScale = new Vector3(factorScaladoY + 0.2f, factorScaladoY + 0.2f, 1);
                boardManager.transform.localScale = boardScale;
                clickTraker.transform.localScale = clickScale;
            }
            else
            {
                // MIRAR EL IF DE ARRIBA
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

            //move position
            if (boardManager.getHMatrix() < 6)
            {

                float moveY = 0;
                switch (boardManager.getHMatrix())
                {
                    case 1:
                        moveY = 2.5f;
                        break;
                    case 2:
                        moveY = 2.0f;
                        break;
                    case 3:
                        moveY = 1.5f;
                        break;
                    case 4:
                        moveY = 1.0f;
                        break;
                    case 5:
                        moveY = 0.5f;
                        break;
                }
                if (factorScaladoY > 0 && boardManager.getHMatrix() == 6 && Screen.height > Screen.width)
                    moveY = 2.0f;
                boardManager.transform.position = new Vector3(boardManager.transform.position.x, boardManager.transform.position.y + moveY, 0);
            }
            else
            {
                float moveY = 0;
                if (factorScaladoY > 0 && boardManager.getHMatrix() == 6 && Screen.height > Screen.width)
                    moveY = 1.0f;
                boardManager.transform.position = new Vector3(boardManager.transform.position.x, boardManager.transform.position.y + moveY, 0);

            }
            if (boardManager.getWMatrix() < 5)
            {

                float moveX = 0;
                switch (boardManager.getWMatrix())
                {
                    case 1:
                        moveX = 2.0f;
                        break;
                    case 2:
                        moveX = 1.5f;
                        break;
                    case 3:
                        moveX = 1.0f;
                        break;
                    case 4:
                        moveX = 0.5f;
                        break;
                }
                boardManager.transform.position = new Vector3(boardManager.transform.position.x + moveX, boardManager.transform.position.y, 0);
            }
        }
    }
}

/*       if (_alto < 6 || _ancho < 5)
       {
           float offsetY = transform.position.y;
           float offsetX = transform.position.x;

           if (_alto < 6)
               offsetY -= _alto/ 2.0f;


               offsetX -= _ancho / 2.0f - 1;


           transform.position = new Vector3(offsetX, offsetY, transform.position.z);
       }

       if(_ancho > 5)
       {
           switch (_ancho)
           {
               case 6:
                   transform.localScale = new Vector3(transform.localScale.x * 0.8f, transform.localScale.y * 0.8f, 1);
                   break;
               case 7:
                   transform.localScale = new Vector3(transform.localScale.x * 0.65f, transform.localScale.y * 0.65f, 1);
                   break;
               case 8:
                   transform.localScale = new Vector3(transform.localScale.x * 0.5f, transform.localScale.y * 0.5f, 1);
                   break;
           }
       }*/
