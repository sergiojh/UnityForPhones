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

    private int actualLevel;
    private int actualCategory;
    private void Start()
    {
       DontDestroyOnLoad(this.gameObject);
    }

    public void addCoins(int value)
    {
        coins += value;
    }
    
    public void subtractCoins(int value)
    {
        coins -= value;
    }

    public int getCoins()
    {
        return coins;
    }

    public bool setActualLevel(int levelNumber)
    {
        if (levelNumber < levelsPerCategory[actualCategory])
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
