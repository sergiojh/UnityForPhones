using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
/// <summary>
/// Clase que controla todo lo relacionado con el tablero. Tanto las pulsaciones del usuario como el fin del juego y su creación.
/// </summary>
public class BoardManager : MonoBehaviour
{
    [SerializeField]
    private List<Tile> typeofTile;

    [SerializeField]
    private Scaler scaler;

    [SerializeField]
    private Camera camera;

    private Board mapa;
    private Tile[,] matrix;
    private int _yTileActivo;
    private int _xTileActivo;
    private int _ancho;
    private int _alto;
    private int _startPosX;
    private int _startPosY;
    private int _piel;
    private int _hintNumber;
    private int _nivel;
    /// <summary>
    /// Inicialización del tablero, lee el archivo, crea el tablero y lo escala.
    /// </summary>
    /// <param name="mapName">Ruta donde se encuentra el archivo.</param>
    /// <param name="level">Nivel a cargar</param>
    /// <param name="piel">Piel a usar para que el color sea uniforme.</param>
    /// <param name="clickTracker">Tracker de la pulsación del usuario.</param>
    public void InitBoard(int actualCategory,int level, int piel, SpriteRenderer clickTracker)
    {
        _hintNumber = 1;
        _piel = piel;
        string mapName = "maps" + actualCategory;
        mapa = ParseTxtMaps(mapName, level);
        CreateBoard(mapa);
        _nivel = level;
        scaler.StartScaling(clickTracker);
        float x = 0.5f;
        float y = 0.5f;
        if (_alto != 0)
        {
            if (y % 2 == 0)
                y = _alto / 2 - 0.5f;
            else
                y = _alto / 2;
        }
        if (_ancho != 0)
        {
            if (x % 2 == 0)
                x = _ancho / 2 - 0.5f;
            else
                x = _ancho / 2;
        }
        camera.transform.localPosition = new Vector3(x, y,-10);
    }

    public int GetTotalTypeTiles()
    {
        return typeofTile.Count;
    }
    public int GetWMatrix()
    {
        return _ancho;
    }
   
    public int GetHMatrix()
    {
        return _alto;
    }
    /// <summary>
    /// Pulsación del usuario en una casilla.
    /// </summary>
    /// <param name="v"> Posición donde ha pulsado el usuario.</param>
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
                matrix[(int)v.x, (int)v.y].SetActivePath(val);
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
    /// <summary>
    /// Comprueba si se ha terminado la partida.
    /// </summary>
    /// <returns>True en el caso de haberse terminado el juego, false en caso contrario.</returns>
    public bool CheckFinJuego()
    {
        bool clicked = true;
        int x = 0;
        while (x < _ancho && clicked)
        {
            int y = 0;
            while (y < _alto && clicked)
            {
                if (matrix[x, y] != null && !matrix[x, y].CheckPulsado())
                    clicked = false;
                y++;
            }
            x++;
        }
        return clicked;
    }
    /// <summary>
    /// Crea el nivel a partir de un tablero.
    /// </summary>
    /// <param name="b"> Tablero con los datos que hay que cargar.</param>
    /// <returns></returns>
    private bool CreateBoard(Board b)
    {
        _alto = b.layout.Count;
        _ancho = b.layout[0].Length;
        matrix = new Tile[_ancho, _alto];
        //x es alto, y es ancho
        int xLogic = 0;
        for (int x = 0; x < b.layout.Count ; x++)//Cada fila del tablero
        {
            int yLogic = 0;
            for (int y = 0; y < b.layout[x].Length; y++)//Cada casilla en el string de cada fila
            {
                switch (b.layout[x][y])
                {
                    case '0':
                        matrix[yLogic, xLogic] = null;
                        break;
                    case '1':
                        matrix[yLogic, xLogic] = Instantiate(typeofTile[_piel], new Vector3(y + transform.position.x, (b.layout.Count -1) + (transform.position.y - x), 0), Quaternion.identity, gameObject.transform);
                        matrix[yLogic, xLogic].transform.localPosition = new Vector3(yLogic, xLogic, 85);
                        break;
                    case '2':
                        matrix[yLogic, xLogic] = Instantiate(typeofTile[_piel],new Vector3(y + transform.position.x, (b.layout.Count -1 ) + (transform.position.y - x), 0),Quaternion.identity,gameObject.transform);
                        matrix[yLogic, xLogic].transform.localPosition = new Vector3(yLogic, xLogic, 85);
                        matrix[yLogic, xLogic].SetPulsado(true);
                        _xTileActivo = yLogic;
                        _yTileActivo = xLogic;
                        _startPosX = yLogic;
                        _startPosY = xLogic;
                        break;
                }
                yLogic++;
            }
            xLogic++;
        }
        return true;
    }
    /// <summary>
    /// Resetea el tablero al estado inicial.
    /// </summary>
    public void ResetBoard()
    {
        int x = 0;
        while (x < _ancho)
        {
            int y = 0;
            while (y < _alto)
            {
                if (matrix[x, y] != null)
                {
                    matrix[x, y].SetPulsado(false);
                    matrix[x, y].DisableActivedPath();
                }
                y++;
            }
            x++;
        }

        matrix[_startPosX, _startPosY].SetPulsado(true);
        _xTileActivo = _startPosX;
        _yTileActivo = _startPosY;
    }
    /// <summary>
    /// Activador de pistas, activa un máximo de 5.
    /// </summary>
    /// <returns>True en el caso de que se haya activado alguna pista, false si no.</returns>
    public bool ActiveMoreHints()
    {
        ResetBoard();

        int i = 0;

        while(i < 5 && _hintNumber < mapa.path.Count)
        {

            int x = mapa.path[_hintNumber][1];
            int y = mapa.path[_hintNumber][0];

            if (mapa.path[_hintNumber][0] == mapa.path[_hintNumber - 1][0])//se mueve en x
            {
                if (mapa.path[_hintNumber][1] == mapa.path[_hintNumber - 1][1] - 1)//izquierda
                {
                    matrix[x, y].SetActiveHintPath(3);
                }
                else //derecha
                {
                    matrix[x, y].SetActiveHintPath(2);
                }
            }
            else //se mueve en y
            {
                if (mapa.path[_hintNumber][0] == mapa.path[_hintNumber - 1][0] - 1)//abajo
                {
                    matrix[x, y].SetActiveHintPath(0);
                }
                else //arriba
                {
                    matrix[x, y].SetActiveHintPath(1);
                }
            }
            _hintNumber++;
            i++;
        }

        if (i == 0)
            return false;
        else
            return true;

    }
    /// <summary>
    /// Parseador del txt donde se encuentra el mapa a un objeto de tipo Board.
    /// </summary>
    /// <param name="pathToMaps"> Path al directorio donde se encuentra el archivo.</param>
    /// <param name="level"> Nivel que se quiere cargar.</param>
    /// <returns></returns>

    private Board ParseTxtMaps(string pathToMaps, int level)
    {
        TextAsset files = Resources.Load(pathToMaps) as TextAsset;

        string text = files.text;
        //Regular Expressions de Unity permite definir que queremos del txt y que no
        Regex r = new Regex("(?:[^ 0-9 } ]|(?<=['\",]))");
        text = r.Replace(text, string.Empty);
        string[] lines = Regex.Split(text, " ");
        //una vez partido buscamos el nivel correspondiente y lo creamos tipo Board
        Board b = new Board();
        b.layout = new List<string>();
        b.path = new List<List<int>>();

        int i = 0;
        int corchetePast = 0;
        bool hecho = false;
        while (i < lines.Length && !hecho)
        {
            if(corchetePast == level)
            {
                b.index = int.Parse(lines[i + 1]);
                for(int x = i + 2; !hecho; x++)
                {
                    
                    if (lines[x] != " " && lines[x] != "")
                    {
                        if(lines[x] == "}")
                        {
                            hecho = true;
                        }
                        else if (int.Parse(lines[x]) > 9)
                        {//es un nivel  
                            
                            b.layout.Add(lines[x]);
                        }
                        else if(int.Parse(lines[x]) <= 9)//pista
                        {
                            List<int> pistaPos = new List<int>();
                            pistaPos.Add(int.Parse(lines[x]));
                            pistaPos.Add(int.Parse(lines[x + 1]));
                            b.path.Add(pistaPos);
                            x++;
                        }
                    }
                }
            }
            else if (lines[i] == "}")
            {
                corchetePast++;
            }
            i++;
        }
        return b;
    }
}
