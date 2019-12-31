using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectLevelManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private Text categoryText;

    private string actualCategory;
    private int levelsCompleted;

    [SerializeField]
    private GridLayoutGroup content;
    public Button activeLevel;
    public Button blockedLevel;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        actualCategory = gameManager.getNameCategory();
        categoryText.text = actualCategory;

        if(actualCategory == "BEGINNER")
        {
            levelsCompleted = gameManager.GetPersistance().progress[0];
        }
        else if (actualCategory == "REGULAR")
        {
            levelsCompleted = gameManager.GetPersistance().progress[1];
        }
        else if (actualCategory == "ADVANCED")
        {
            levelsCompleted = gameManager.GetPersistance().progress[2];
        }
        else if (actualCategory == "EXPERT")
        {
            levelsCompleted = gameManager.GetPersistance().progress[3];
        }
        else if (actualCategory == "MASTER")
        {
            levelsCompleted = gameManager.GetPersistance().progress[4];
        }

        createBoard(gameManager.getTotalLevelOfCategory(gameManager.getActualLevel()),levelsCompleted);

    }

    public void back()
    {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }


    private void createBoard(int totalLevels,int passedLevels)
    {
        Button b;
        for(int i = 0; i < totalLevels; i++)
        {
            if (i <= passedLevels)
            {
                b = Instantiate(activeLevel, content.transform);
                b.GetComponentInChildren<Text>().text = ""+(i+1);
            }
            else
            {
                b = Instantiate(blockedLevel, content.transform);
            }
        }
    }
}
