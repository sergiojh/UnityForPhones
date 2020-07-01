using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
/// <summary>
/// Clase encargada de guardar y cargar el archivo de la persistencia
/// </summary>
public class PersistanceManager : MonoBehaviour
{
    string path = "save.json";
    /// <summary>
    /// Carga el archivo que contiene la persistencia, en caso de que este no exista, carga una persistencia por defecto.
    /// </summary>
    /// <param name="salt">sal de hash</param>
    public Persistance LoadPersistance(string salt, int tam)
    {
        Persistance persistance;
        if (File.Exists(Application.persistentDataPath + path))
        {
            string json;
            json = File.ReadAllText(Application.persistentDataPath + path);
            PersistanceSecurity persistanceSec = JsonUtility.FromJson<PersistanceSecurity>(json);
            if (persistanceSec != null)
            {
                persistance = new Persistance();
                persistance.archievement = persistanceSec.archievement;
                persistance.coins = persistanceSec.coins;
                persistance.progress = persistanceSec.progress;

                SecurityHelper s = new SecurityHelper();
                string data = JsonUtility.ToJson(persistance);
                string code = s.CreateMD5(data);
                string jsonCode = s.CreateMD5(data + code);
                string salted = s.CreateMD5(salt + jsonCode);
                if (salted != persistanceSec.hash)
                {
                    persistance = new Persistance();
                    persistance.coins = 0;
                    persistance.progress = new int[tam];
                    for (int i = 0; i < tam; i++)
                        persistance.progress[i] = 0;
                    persistance.archievement = 0;
                }
            }
            else
            {
                persistance = new Persistance();
                persistance.coins = 0;
                persistance.progress = new int[tam];
                for (int i = 0; i < tam; i++)
                    persistance.progress[i] = 0;
                persistance.archievement = 0;
            }
        }
        else
        {
            persistance = new Persistance();
            persistance.coins = 0;
            persistance.progress = new int[tam];
            for (int i = 0; i < tam; i++)
                persistance.progress[i] = 0;
            persistance.archievement = 0;
        }
        return persistance;
    }

    /// <summary>
    /// Guardado de la persistencia en un archivo.
    /// </summary>
    /// <param name="persistance">Objeto que contiene datos para guardar en el fichero</param>
    /// <param name="salt">sal de hash</param>
    public void SavePersistance(Persistance persistance, string salt)
    {
        string json = JsonUtility.ToJson(persistance);
        SecurityHelper s = new SecurityHelper();
        string code = s.CreateMD5(json);
        string jsonCode = s.CreateMD5(json + code);
        string salted = s.CreateMD5(salt + jsonCode);
        PersistanceSecurity persistenceSec = new PersistanceSecurity();
        persistenceSec.coins = persistance.coins;
        persistenceSec.archievement = persistance.archievement;
        persistenceSec.progress = persistance.progress;
        persistenceSec.hash = salted;
        string finalJson = JsonUtility.ToJson(persistenceSec);
        File.WriteAllText(Application.persistentDataPath + path, finalJson);
    }
}
