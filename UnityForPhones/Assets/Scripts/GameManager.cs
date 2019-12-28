using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int actualCategory;
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
        return coins;
    }

    public bool setActualLevel(int levelNumber)
    {
        if (levelNumber < levelsPerCategory[actualCategory] && levelNumber + 1 <= levelsPerCategoryCompleted[actualCategory])
        {
            actualLevel = levelNumber;
            return true;
        }
        return false;
    }

    public bool setActualCategory(int numberCategory)
    {
        if(nameCategories.Length > numberCategory)
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
        if(actualLevel > levelsPerCategoryCompleted[actualCategory])
        {
            levelsPerCategoryCompleted[actualCategory]++;
        }
    }


    public int getLevelsCompletedFromActualLevel()
    {
        return levelsPerCategoryCompleted[actualCategory];
    }

}
