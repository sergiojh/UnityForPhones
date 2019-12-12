using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BoardManager : MonoBehaviour
{
    JSONMapReader jsonReader = new JSONMapReader();
    [SerializeField]
    private List<Tile> typeofTile;

    private Tile[,] matrix;
    private int _yTileActivo;
    private int _xTileActivo;
    private int _ancho;
    private int _alto;
    // Start is called before the first frame update
    void Start()
    {
        Board b = jsonReader.deserializarJSON("/Resources/Maps/map1.json");
        createBoard(b);
    }

    public void Clicked(Vector3 v)
    {
        
        v.x += 0.5f;
        v.y += 0.5f;
        Debug.Log(v);
        if (v.y > 0 && v.y < _alto && v.x > 0 && v.x < _ancho && matrix[(int)v.y, (int)v.x] != null)
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
            if (val != -1 && !matrix[(int)v.y, (int)v.x].CheckPulsado())
            {
                matrix[(int)v.y, (int)v.x].SetPulsado(true);
                matrix[(int)v.y, (int)v.x].setActivePath(val);
                _xTileActivo = (int)v.x;
                _yTileActivo = (int)v.y;
            }
            else if(matrix[(int)v.y, (int)v.x].CheckPulsado() && ((int)v.y != _yTileActivo || (int)v.x != _xTileActivo))
            {
                while ((int)v.y != _yTileActivo && (int)v.x != _xTileActivo)
                {
                    int i = matrix[_yTileActivo, _xTileActivo].DisableActivedPath();
                    if(i == 0)
                    {
                        _yTileActivo -= 1;
                    }
                    else if(i == 1)
                    {
                        _yTileActivo += 1;
                    }
                    else if (i == 2)
                    {
                        _xTileActivo += 1;
                    }
                    else if (i == 3)
                    {
                        _xTileActivo -= 1;
                    }
                }
                /*
                while(!llegueAlObjetivo)
                    //desapilar usando norte sur este y oeste para moverme
             
             */
            }

        }
    }



    private bool createBoard(Board b)
    {

        _alto = b.layout.Count;
        _ancho = b.layout[0].Length;

        matrix = new Tile[_alto, _ancho];

        //x es alto, y es ancho
        for(int x = 0; x < b.layout.Count; x++)//Cada fila del tablero
        {
            for(int y = 0; y < b.layout[x].Length; y++)//Cada casilla en el string de cada fila
            {
                switch(b.layout[x][y])
                {
                    case '0':
                        matrix[x, y] = null;
                        break;
                    case '1':
                        matrix[x, y] = Instantiate(typeofTile[0], new Vector3(y + transform.position.x, x + transform.position.y, 0), Quaternion.identity, gameObject.transform);
                        break;
                    case '2':
                        matrix[x,y] = Instantiate(typeofTile[0],new Vector3(y + transform.position.x, x + transform.position.y, 0),Quaternion.identity,gameObject.transform);
                        matrix[x, y].SetPulsado(true);
                        _xTileActivo = y;
                        _yTileActivo = x;
                        break;
                }
            }
        }
        return true;
    }
}
