using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class GameManager : MonoBehaviour
{
    private int coins;

    [SerializeField]
    private string[] nameCategories;
    [SerializeField]
    private int[] levelsPerCategory;
    [SerializeField]
    private int[] levelsPerCategoryCompleted;

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
        coins += value;
        persistance.coins = coins;
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

    public void loadPersistance(string path)
    {
        if (File.Exists(Application.dataPath + path))
        {
            Debug.Log("EXISTE");
            string json = File.ReadAllText(Application.dataPath + path);
            persistance = JsonConvert.DeserializeObject<Persistance>(json);
        }

        else
        {
            Debug.Log("NO EXISTE");
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
        levelsPerCategoryCompleted[category] = levelCompleted;
    }

    public void savePersistance()
    {
        string json = JsonUtility.ToJson(persistance);
        File.WriteAllText(Application.dataPath + "/Resources/Maps/save.json", json);

        
    }
}
