﻿using System.Collections;
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
    public Persistance loadPersistance(string salt)
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
                persistance.progress1 = persistanceSec.progress1;
                persistance.progress2 = persistanceSec.progress2;
                persistance.progress3 = persistanceSec.progress3;
                persistance.progress4 = persistanceSec.progress4;

                SecurityHelper s = new SecurityHelper();
                string data = JsonUtility.ToJson(persistance);
                string code = s.encript(data);
                string jsonCode = s.encript(data + code);
                string salted = s.encript(salt + jsonCode);
                if (salted != persistanceSec.hash)
                {
                    persistance = new Persistance();
                    persistance.coins = 0;
                    persistance.progress = 0;
                    persistance.progress1 = 0;
                    persistance.progress2 = 0;
                    persistance.progress3 = 0;
                    persistance.progress4 = 0;
                    persistance.archievement = 0;
                }
            }
            else
            {
                persistance = new Persistance();
                persistance.coins = 0;
                persistance.progress = 0;
                persistance.progress1 = 0;
                persistance.progress2 = 0;
                persistance.progress3 = 0;
                persistance.progress4 = 0;
                persistance.archievement = 0;
            }
        }
        else
        {
            persistance = new Persistance();
            persistance.coins = 0;
            persistance.progress = 0;
            persistance.progress1 = 0;
            persistance.progress2 = 0;
            persistance.progress3 = 0;
            persistance.progress4 = 0;
            persistance.archievement = 0;
        }
        return persistance;
    }

    /// <summary>
    /// Guardado de la persistencia en un archivo.
    /// </summary>
    /// <param name="persistance">Objeto que contiene datos para guardar en el fichero</param>
    /// <param name="salt">sal de hash</param>
    public void savePersistance(Persistance persistance, string salt)
    {
        string json = JsonUtility.ToJson(persistance);
        SecurityHelper s = new SecurityHelper();
        string code = s.encript(json);
        string jsonCode = s.encript(json + code);
        string salted = s.encript(salt + jsonCode);
        PersistanceSecurity persistenceSec = new PersistanceSecurity();
        persistenceSec.coins = persistance.coins;
        persistenceSec.archievement = persistance.archievement;
        persistenceSec.progress = persistance.progress;
        persistenceSec.progress1 = persistance.progress1;
        persistenceSec.progress2 = persistance.progress2;
        persistenceSec.progress3 = persistance.progress3;
        persistenceSec.progress4 = persistance.progress4;
        persistenceSec.hash = salted;
        string finalJson = JsonUtility.ToJson(persistenceSec);
        File.WriteAllText(Application.persistentDataPath + path, finalJson);
    }
}
