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
        /*string json = File.ReadAllText(Application.streamingAssetsPath + path);
        List<Board> board = JsonConvert.DeserializeObject<List<Board>>(json);
        return board;
        */

        TextAsset asset = Resources.Load<TextAsset>("maps");
        List<Board> board = JsonConvert.DeserializeObject<List<Board>>(asset.ToString());
        


        return board;
    }

}
