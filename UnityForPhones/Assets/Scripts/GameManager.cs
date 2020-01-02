using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class GameManager : MonoBehaviour
{
    private int coins = 0;

    [SerializeField]
    private string[] nameCategories;
    [SerializeField]
    private int[] levelsPerCategory;
    [SerializeField]
    private int[] levelsPerCategoryCompleted;

    private static GameManager gameManager;
    private int actualLevel;
    private int actualCategory;


    private static Persistance persistance;

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
        coins += value;
    }

    public bool subtractCoins(int value)
    {
        if (coins - value >= 0)
        {
            coins -= value;
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
        Debug.Log(levelsPerCategoryCompleted[actualCategory]);
        if (levelNumber <= levelsPerCategoryCompleted[actualCategory] + 1)
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
        return levelsPerCategoryCompleted[category];
    }

    public void levelCompleted()
    {
        if (actualLevel > levelsPerCategoryCompleted[actualCategory])
        {
            levelsPerCategoryCompleted[actualCategory]++;
            persistance.progress[actualCategory]++;
        }
    }


    public int getLevelsCompletedFromActualLevel()
    {
        return levelsPerCategoryCompleted[actualCategory];
    }

    public void SavePersistance(string path)
    {
        string json = File.ReadAllText(Application.dataPath + path);
        persistance = JsonConvert.DeserializeObject<Persistance>(json);
    }
    public Persistance GetPersistance()
    {
        return persistance;
    }

    
    public void setLevelsCompleted(int category, int levelCompleted)
    {
        levelsPerCategoryCompleted[category] = levelCompleted;
    }

    public void savePersistance()
    {
        Debug.Log(persistance);
        string json = JsonUtility.ToJson(persistance);
        Debug.Log(json);
        File.WriteAllText(Application.dataPath + "/Resources/Maps/save.json", json);

        
    }
}
