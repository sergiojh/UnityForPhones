using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class JSONMapReader: MonoBehaviour
{
    //TODO: CONTROL DE ERRORES. 
    // path = "/Resources/Maps/map1.json"

    public List<Board> deserializarJSON(string path)
    {
        //string json = File.ReadAllText(Application.dataPath + path);




        string filePath = Application.streamingAssetsPath + "/maps.json";
        string jsonString;

        if (Application.platform == RuntimePlatform.Android) //Need to extract file from apk first
        {
            WWW reader = new WWW(filePath);
            while (!reader.isDone) { }

            jsonString = reader.text;
        }
        else
        {
            jsonString = File.ReadAllText(filePath);
        }

    




         List<Board> board = JsonConvert.DeserializeObject<List<Board>>(jsonString);


        //List<Board> board = JsonUtility.FromJson<List<Board>>(Application.dataPath + path);
        return board;
    }
    
}
