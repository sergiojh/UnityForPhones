using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BoardManager : MonoBehaviour
{
    JSONMapReader jsonReader = new JSONMapReader();
    [SerializeField]
    private List<Tile> typeofTile;

    [SerializeField]
    private Scaler scaler;

    private List<Board> mapas = new List<Board>();

    private Tile[,] matrix;
    private int _yTileActivo;
    private int _xTileActivo;
    private int _ancho;
    private int _alto;
    // Start is called before the first frame update
    void Start()
    {
        mapas = jsonReader.deserializarJSON("/Resources/Maps/map2.json");
        createBoard(mapas,1);
        scaler.startScaling();
    }

    public int getWMatrix()
    {
        return _ancho;
    }

    public int getHMatrix()
    {
        return _alto;
    }

    public void Clicked(Vector3 v)
    {
        
        v.x += 0.5f;
        v.y += 0.5f;
        
        if (v.y > 0 && v.y < _alto && v.x > 0 && v.x < _ancho && matrix[(int)v.x, (int)v.y] != null)
        {
            int val = -1;
            if (_xTileActivo == (int)v.x && _yTileActivo == (int)v.y + 1)
            {
                val = 0;
            }
            else if (_xTileActivo == (int)v.x && _yTileActivo == (int)v.y - 1)
            {
                val = 1;
            }
            else if (_xTileActivo == (int)v.x + 1 && _yTileActivo == (int)v.y)
            {
                val = 3;
            }
            else if (_xTileActivo == (int)v.x - 1 && _yTileActivo == (int)v.y)//esta dentro del cuadrado +-1 del tile
            {
                val = 2;
            }
            if (val != -1 && !matrix[(int)v.x, (int)v.y].CheckPulsado())
            {
                matrix[(int)v.x, (int)v.y].SetPulsado(true);
                matrix[(int)v.x, (int)v.y].setActivePath(val);
                _xTileActivo = (int)v.x;
                _yTileActivo = (int)v.y;
            }
            else if(matrix[(int)v.x, (int)v.y].CheckPulsado() && ((int)v.y != _yTileActivo || (int)v.x != _xTileActivo)) //vuelta atras
            {
                while ((int)v.y != _yTileActivo || (int)v.x != _xTileActivo)
                {
                    int i = matrix[_xTileActivo, _yTileActivo].DisableActivedPath();
                    if (i == 0)
                    {
                        _yTileActivo += 1;
                    }
                    else if(i == 1)
                    {
                        _yTileActivo -= 1;
                    }
                    else if (i == 2)
                    {
                        _xTileActivo -= 1;
                    }
                    else if (i == 3)
                    {
                        _xTileActivo += 1;
                    }
                }           
            }
        }
    }



    private bool createBoard(List<Board> b,int nivel)
    {
        _alto = b[nivel].layout.Count;
        _ancho = b[nivel].layout[0].Length;
        matrix = new Tile[_ancho, _alto];
        //x es alto, y es ancho
        int xLogic = _alto - 1;
        for (int x = 0; x < b[nivel].layout.Count ; x++)//Cada fila del tablero
        {
            int yLogic = 0;
            for (int y = 0; y < b[nivel].layout[x].Length; y++)//Cada casilla en el string de cada fila
            {
                switch (b[nivel].layout[x][y])
                {
                    case '0':
                        matrix[yLogic, xLogic] = null;
                        break;
                    case '1':
                        matrix[yLogic, xLogic] = Instantiate(typeofTile[0], new Vector3(y + transform.position.x, (b[nivel].layout.Count -1) + (transform.position.y - x), 0), Quaternion.identity, gameObject.transform);
                        matrix[yLogic, xLogic].transform.localPosition = new Vector3(yLogic, xLogic, 0);
                        break;
                    case '2':
                        matrix[yLogic, xLogic] = Instantiate(typeofTile[0],new Vector3(y + transform.position.x, (b[nivel].layout.Count -1 ) + (transform.position.y - x), 0),Quaternion.identity,gameObject.transform);
                        matrix[yLogic, xLogic].transform.localPosition = new Vector3(yLogic, xLogic, 0);
                        matrix[yLogic, xLogic].SetPulsado(true);
                        _xTileActivo = yLogic;
                        _yTileActivo = xLogic;
                        break;
                }
                yLogic++;
            }
            xLogic--;
        }
        return true;
    }
}
