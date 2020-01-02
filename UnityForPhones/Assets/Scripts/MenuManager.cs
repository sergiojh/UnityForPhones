using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameManager prefab;

    private GameManager gameManager;
    [SerializeField]
    private Text coinsText;
    [SerializeField]
    private Text numLevelBeginnerText;
    [SerializeField]
    private Text numLevelRegularText;
    [SerializeField]
    private Text numLevelAdvancedText;
    [SerializeField]
    private Text numLevelExpertText;
    [SerializeField]
    private Text numLevelMasterText;

    


    private int coins;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            gameManager = Instantiate(prefab, this.transform);
            gameManager.starRuning();
        }
        
        gameManager.loadPersistance("/Resources/Maps/save.json");

        coins = gameManager.GetPersistance().coins;
        coinsText.text = "" + coins;

        numLevelBeginnerText.text = "" + gameManager.GetPersistance().progress[0] + "/" + gameManager.getTotalLevelOfCategory(0);
        numLevelRegularText.text = "" + gameManager.GetPersistance().progress[1] + "/" + gameManager.getTotalLevelOfCategory(1);
        numLevelAdvancedText.text = "" + gameManager.GetPersistance().progress[2] + "/" + gameManager.getTotalLevelOfCategory(2);
        numLevelExpertText.text = "" + gameManager.GetPersistance().progress[3] + "/" + gameManager.getTotalLevelOfCategory(3);
        numLevelMasterText.text = "" + gameManager.GetPersistance().progress[4] + "/" + gameManager.getTotalLevelOfCategory(4);

        gameManager.setLevelsCompleted(0, gameManager.GetPersistance().progress[0]);
        gameManager.setLevelsCompleted(1, gameManager.GetPersistance().progress[1]);
        gameManager.setLevelsCompleted(2, gameManager.GetPersistance().progress[2]);
        gameManager.setLevelsCompleted(3, gameManager.GetPersistance().progress[3]);
        gameManager.setLevelsCompleted(4, gameManager.GetPersistance().progress[4]);
    }

    public void exitApp()
    {
        Application.Quit();
    }

    public void goToBeginner()
    {
        if(gameManager.setActualCategory(0))
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
    }

    public void goToRegular()
    {
        if (gameManager.setActualCategory(1))
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
    }

    public void goToAdvanced()
    {
        if (gameManager.setActualCategory(2))
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
    }

    public void goToExpert()
    {
        if (gameManager.setActualCategory(3))
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
    }

    public void goToMaster()
    {
        if (gameManager.setActualCategory(4))
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
    }
}
