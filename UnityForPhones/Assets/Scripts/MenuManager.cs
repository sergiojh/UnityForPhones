using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameManager prefab;
    [SerializeField]
    private AdControllerMenu adController;

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
    [SerializeField]
    private Text archievementText;
    [SerializeField]
    private CanvasRenderer challengeFrame;
    [SerializeField]
    private CanvasRenderer challengeDoneFrame;
    [SerializeField]
    private TimeMenuChallege timeMenuChallege;

    private bool frameActive = false;
    private int coins;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        challengeFrame.gameObject.SetActive(false);
        if (gameManager == null)
        {
            gameManager = Instantiate(prefab, this.transform);
            gameManager.starRuning();
        }
        
        gameManager.loadPersistance("save.json");

        coins = gameManager.GetPersistance().coins;
        coinsText.text = "" + coins;
        archievementText.text = "" + gameManager.getArchievement();
        numLevelBeginnerText.text = "" + gameManager.GetPersistance().progress + "/" + gameManager.getTotalLevelOfCategory(0);
        numLevelRegularText.text = "" + gameManager.GetPersistance().progress1 + "/" + gameManager.getTotalLevelOfCategory(1);
        numLevelAdvancedText.text = "" + gameManager.GetPersistance().progress2 + "/" + gameManager.getTotalLevelOfCategory(2);
        numLevelExpertText.text = "" + gameManager.GetPersistance().progress3 + "/" + gameManager.getTotalLevelOfCategory(3);
        numLevelMasterText.text = "" + gameManager.GetPersistance().progress4 + "/" + gameManager.getTotalLevelOfCategory(4);

        gameManager.setLevelsCompleted(0, gameManager.GetPersistance().progress);
        gameManager.setLevelsCompleted(1, gameManager.GetPersistance().progress1);
        gameManager.setLevelsCompleted(2, gameManager.GetPersistance().progress2);
        gameManager.setLevelsCompleted(3, gameManager.GetPersistance().progress3);
        gameManager.setLevelsCompleted(4, gameManager.GetPersistance().progress4);

        if (!gameManager.checkChallenge())
        {
            challengeDoneFrame.gameObject.SetActive(true);
            timeMenuChallege.init(gameManager.getTimer());
        }
        else
        {
            challengeDoneFrame.gameObject.SetActive(false);
            timeMenuChallege.notNow();
        }

    }

    public void exitApp()
    {
        Application.Quit();
    }

    public void goToBeginner()
    {
        if (!frameActive && gameManager.setActualCategory(0))
        {
            adController.stopListening();
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
        }
    }

    public void goToRegular()
    {
        if (!frameActive && gameManager.setActualCategory(1))
        {
            adController.stopListening();
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
        }
    }

    public void goToAdvanced()
    {
        if (!frameActive && gameManager.setActualCategory(2))
        {
            adController.stopListening();
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
        }
    }

    public void goToExpert()
    {
        if (!frameActive && gameManager.setActualCategory(3))
        {
            adController.stopListening();
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
        }
    }

    public void goToMaster()
    {
        if (!frameActive && gameManager.setActualCategory(4))
        {
            adController.stopListening();
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
        }
    }

    public void updateChallenge()
    {
        gameManager.checkChallenge();
        challengeDoneFrame.gameObject.SetActive(false);
        timeMenuChallege.notNow();
    }

    public void goToChallenge()
    {
        if (gameManager.checkChallenge())
        {
            frameActive = true;
            challengeFrame.gameObject.SetActive(true);
        }
    }

    public void DesactiveChallengeFrame()
    {
        frameActive = false;
        challengeFrame.gameObject.SetActive(false);
    }
    public void challengeAd()
    {
        adController.showAd();
    }

    public void adSeen()
    {
        if (gameManager.setActualCategory(5))
        {
            adController.stopListening();
            SceneManager.LoadScene("Challenge", LoadSceneMode.Single);
        }
    }

    public void challengeCost()
    {
        if (gameManager.subtractCoins(25) && gameManager.setActualCategory(5))
        {
            adController.stopListening();
            gameManager.savePersistance();
            SceneManager.LoadScene("Challenge", LoadSceneMode.Single);
        }
    }
}
