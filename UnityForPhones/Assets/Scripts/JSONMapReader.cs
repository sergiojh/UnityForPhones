using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.Json;



public class JSONMapReader: MonoBehaviour
{
    //TODO: CONTROL DE ERRORES. 
    // path = "/Resources/Maps/map1.json"

    public List<Board> deserializarJSON(string path)
    {
        //string json = File.ReadAllText(Application.dataPath + path);

        Debug.Log("AAA " + Application.streamingAssetsPath + "/maps.json");

        string filePath = Application.streamingAssetsPath + "/maps.json";
        string jsonString;
     
        jsonString = File.ReadAllText(filePath);
        
        List<Board> board;
        board = JsonSerializer.Deserialize<List<Board>>(jsonString);

        return board;

        
        
    }
    
}
