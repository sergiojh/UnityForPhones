using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private string[] nameCategories;
    [SerializeField]
    private int[] levelsPerCategory;


    private static GameManager gameManager;
    private int actualLevel;
    private int actualCategory;


    private Persistance persistance;

    public void starRuning()
    {
        DontDestroyOnLoad(this);

        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    void Awake()
    {
        DontDestroyOnLoad(this);

        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void addCoins(int value)
    {
        persistance.coins += value;
    }

    public bool subtractCoins(int value)
    {
        if (persistance.coins - value >= 0)
        {
            persistance.coins -= value;
            return true;
        }
        return false;
    }

    public int getCoins()
    {
        return GetPersistance().coins;
    }

    public bool setActualLevel(int levelNumber)
    {
        if (levelNumber <= persistance.progress[actualCategory] + 1)
        {
            actualLevel = levelNumber;
            return true;
        }
        return false;
    }

    public bool setActualCategory(int numberCategory)
    {
        if (nameCategories.Length > numberCategory)
        {
            actualCategory = numberCategory;
            return true;
        }
        return false;
    }

    public string getNameCategory()
    {
        return nameCategories[actualCategory];
    }

    public int getActualCategory()
    {
        return actualCategory;
    }

    public int getActualLevel()
    {
        return actualLevel;
    }

    public int getTotalLevelOfCategory(int category)
    {
        return levelsPerCategory[category];
    }

    public int getTotalLevelCompletedOfCategory(int category)
    {
        return persistance.progress[category];
    }

    public void levelCompleted()
    {
        if (actualLevel > persistance.progress[actualCategory])
        {
            persistance.progress[actualCategory]++;
        }
    }


    public int getLevelsCompletedFromActualLevel()
    {
        return persistance.progress[actualCategory];
    }

    public void loadPersistance(string path)
    {
        if (File.Exists(Application.persistentDataPath + path))
        {
            string json;
            json = File.ReadAllText(Application.persistentDataPath + path);
            persistance = JsonConvert.DeserializeObject<Persistance>(json);
        }
        else
        {
            persistance = new Persistance();
            persistance.coins = 0;
            persistance.categories = new List<string>() { "BEGINNER", "REGULAR", "ADVANCED", "EXPERT", "MASTER" };
            persistance.progress = new List<int>() { 0, 0, 0, 0, 0 };

        }
    }
    public Persistance GetPersistance()
    {
        return persistance;
    }

    
    public void setLevelsCompleted(int category, int levelCompleted)
    {
        persistance.progress[category] = levelCompleted;
    }

    public void savePersistance()
    {
        string json = JsonUtility.ToJson(persistance);
        File.WriteAllText(Application.persistentDataPath + "save.json", json);        
    }
}
