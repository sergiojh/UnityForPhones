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

    public bool challengeReady;
    private float timer;
    private Persistance persistance;

    public void starRuning()
    {
        DontDestroyOnLoad(this);

        if (gameManager == null)
        {
            gameManager = this;
            challengeReady = true;
            timer = 1800.0f;
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
    void Update()
    {
        timer -= Time.deltaTime;
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
        int valor = 0;
        switch (actualCategory)
        {
            case 0:
                valor = persistance.progress;
                break;
            case 1:
                valor = persistance.progress1;
                break;
            case 2:
                valor = persistance.progress2;
                break;
            case 3:
                valor = persistance.progress3;
                break;
            case 4:
                valor = persistance.progress4;
                break;
        }

        if (levelNumber <= valor + 1)
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
        switch (category)
        {
            case 0:
                return persistance.progress;
            case 1:
                return persistance.progress1;
            case 2:
                return persistance.progress2;
            case 3:
                return persistance.progress3;
            case 4:
                return persistance.progress4;
        }
        return -1;
    }

    public int getTotalLevels()
    {
        int levelN = 0;
        foreach (int i in levelsPerCategory)
        {
            levelN += i;
        }
        return levelN;
    }

    public void levelCompleted()
    {
        switch (actualCategory)
        {
            case 0:
                if (actualLevel > persistance.progress)
                    persistance.progress++;
                break;
            case 1:
                if (actualLevel > persistance.progress1)
                    persistance.progress1++;
                break;
            case 2:
                if (actualLevel > persistance.progress2)
                    persistance.progress2++;
                break;
            case 3:
                if (actualLevel > persistance.progress3)
                    persistance.progress3++;
                break;
            case 4:
                if (actualLevel > persistance.progress4)
                    persistance.progress4++;
                break;
        }
    }

    public void addArchievement()
    {
        persistance.archievement++;
    }

    public int getArchievement()
    {
        return persistance.archievement;
    }

    public int getLevelsCompletedFromActualLevel()
    {
        switch (actualCategory)
        {
            case 0:
                return persistance.progress;
            case 1:
                return persistance.progress1;
            case 2:
                return persistance.progress2;
            case 3:
                return persistance.progress3;
            case 4:
                return persistance.progress4;
        }
        return -1;
    }

    public void loadPersistance(string path)
    {
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
                string salted = s.encript(nameCategories[0] + jsonCode);
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
    }

    public Persistance GetPersistance()
    {
        return persistance;
    }

    
    public void setLevelsCompleted(int category, int levelCompleted)
    {
        switch (category)
        {
            case 0:
                persistance.progress = levelCompleted;
                break;
            case 1:
                persistance.progress1 = levelCompleted;
                break;
            case 2:
                persistance.progress2 = levelCompleted;
                break;
            case 3:
                persistance.progress3 = levelCompleted;
                break;
        }
    }

    public bool checkChallenge()
    {

        if (challengeReady)
            return true;
        else if (timer <= 0.0f)//30 min
        {
            challengeReady = true;
            timer = 1800.0f;
            return true;
        }
        else
        {
            return false;
        }
    }

    public float getTimer()
    {
        return timer;
    }

    public void resetTimerChallenge()
    {
        timer = 1800.0f;
        challengeReady = false;
    }

    public void savePersistance()
    {
        string json = JsonUtility.ToJson(persistance);
        SecurityHelper s = new SecurityHelper();
        string code = s.encript(json);
        string jsonCode = s.encript(json + code);
        string salted = s.encript(nameCategories[0] + jsonCode);
        PersistanceSecurity persistenceSec =  new PersistanceSecurity();
        persistenceSec.coins = persistance.coins;
        persistenceSec.archievement = persistance.archievement;
        persistenceSec.progress = persistance.progress;
        persistenceSec.progress1 = persistance.progress1;
        persistenceSec.progress2 = persistance.progress2;
        persistenceSec.progress3 = persistance.progress3;
        persistenceSec.progress4 = persistance.progress4;
        persistenceSec.hash = salted;
        string finalJson = JsonUtility.ToJson(persistenceSec);
        File.WriteAllText(Application.persistentDataPath + "save.json", finalJson);        
    }
}
