using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BoardManager : MonoBehaviour
{
    JSONMapReader jsonReader = new JSONMapReader();
    [SerializeField]
    private List<Tile> typeofTile;
    // Start is called before the first frame update
    void Start()
    {
        Board b = jsonReader.deserializarJSON("/Resources/Maps/map1.json");
        createBoard(b);
    }

    private bool createBoard(Board b)
    {
        for(int x = 0; x < b.layout.Count; x++)//Cada fila del tablero
        {
            for(int y = 0; y < b.layout[x].Length; y++)//Cada casilla en el string de cada fila
            {
                switch(b.layout[x][y])
                {
                    case '0':
                        break;
                    case '1':
                    case '2':
                        Instantiate(typeofTile[0],new Vector3(y,x,0),Quaternion.identity,gameObject.transform); 
                        break;
                }
            }

        }


        return true;
    }
}
