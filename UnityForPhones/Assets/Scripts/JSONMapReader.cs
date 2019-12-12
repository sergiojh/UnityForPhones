using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
public class JSONMapReader: MonoBehaviour
{
    //TODO: CONTROL DE ERRORES. 
    // path = "/Resources/Maps/map1.json"
    public Board deserializarJSON(string path)
    {
        string json = File.ReadAllText(Application.dataPath + path );
        Board board = JsonConvert.DeserializeObject<Board>(json);
        return board;
    }
    


    
}
