using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
public class JSONMapReader: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string json = File.ReadAllText(Application.dataPath + "/Resources/Maps/map1.json");
        var model = JsonConvert.DeserializeObject<Board>(json);
        Debug.Log(model.path[0].x);
        Debug.Log(model.path[0].y);
        Debug.Log(model.path[1].x);
        Debug.Log(model.path[1].y);
        Debug.Log(model.path[2].x);
        Debug.Log(model.path[2].y);

    }
}
