using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// Clase que controla la escena del Menú principal.
/// </summary>
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
    private Text numArchievementText;
    [SerializeField]
    private Text levelBeginnerText;
    [SerializeField]
    private Text levelRegularText;
    [SerializeField]
    private Text levelAdvancedText;
    [SerializeField]
    private Text levelExpertText;
    [SerializeField]
    private Text levelMasterText;
    [SerializeField]
    private Text archievementText;
    [SerializeField]
    private Text archievementTextFrame;
    [SerializeField]
    private CanvasRenderer challengeFrame;
    [SerializeField]
    private CanvasRenderer challengeDoneFrame;
    [SerializeField]
    private TimeMenuChallege timeMenuChallege;
    [SerializeField]
    private Regalo regalo;
    private bool frameActive = false;
    private int coins;
    /// <summary>
    /// Carga la persistencia para mostrar el progreso del usuario en cada categoría, 
    /// las medallas conseguidas en el modo Challenge y las monedas de las que dispone el usuario.
    /// </summary>
    void Start()
    {
        gameManager = GameManager.GetGameManager();
        challengeFrame.gameObject.SetActive(false);
        
        gameManager.LoadPersistance();

        coins = gameManager.GetCoins();
        coinsText.text = "" + coins;
        numArchievementText.text = "" + gameManager.GetArchievement();
        numLevelBeginnerText.text = "" + gameManager.GetTotalLevelCompletedOfCategory(0) + "/" + gameManager.GetTotalLevelOfCategory(0);
        numLevelRegularText.text = "" + gameManager.GetTotalLevelCompletedOfCategory(1) + "/" + gameManager.GetTotalLevelOfCategory(1);
        numLevelAdvancedText.text = "" + gameManager.GetTotalLevelCompletedOfCategory(2) + "/" + gameManager.GetTotalLevelOfCategory(2);
        numLevelExpertText.text = "" + gameManager.GetTotalLevelCompletedOfCategory(3) + "/" + gameManager.GetTotalLevelOfCategory(3);
        numLevelMasterText.text = "" + gameManager.GetTotalLevelCompletedOfCategory(4) + "/" + gameManager.GetTotalLevelOfCategory(4);

        archievementTextFrame.text = "" + gameManager.GetNameCategoryByIndex(5);
        archievementText.text = "" + gameManager.GetNameCategoryByIndex(5);
        levelBeginnerText.text = "" + gameManager.GetNameCategoryByIndex(0);
        levelRegularText.text = "" + gameManager.GetNameCategoryByIndex(1);
        levelAdvancedText.text = "" + gameManager.GetNameCategoryByIndex(2);
        levelExpertText.text = "" + gameManager.GetNameCategoryByIndex(3);
        levelMasterText.text = "" + gameManager.GetNameCategoryByIndex(4);


        gameManager.SetLevelsCompleted(0, gameManager.GetTotalLevelCompletedOfCategory(0));
        gameManager.SetLevelsCompleted(1, gameManager.GetTotalLevelCompletedOfCategory(1));
        gameManager.SetLevelsCompleted(2, gameManager.GetTotalLevelCompletedOfCategory(2));
        gameManager.SetLevelsCompleted(3, gameManager.GetTotalLevelCompletedOfCategory(3));
        gameManager.SetLevelsCompleted(4, gameManager.GetTotalLevelCompletedOfCategory(4));
        
        if (!gameManager.CheckChallenge())
        {
            challengeDoneFrame.gameObject.SetActive(true);
            timeMenuChallege.Init(gameManager.GetTimer());
        }
        else
        {
            challengeDoneFrame.gameObject.SetActive(false);
            timeMenuChallege.NotNow();
        }

        regalo.Init();
    }
    /// <summary>
    /// Cierra la aplicación.
    /// </summary>
    public void ExitApp()
    {
        Application.Quit();
    }
    /// <summary>
    /// Carga la selección de niveles de la categoría Beginner.
    /// </summary>
    public void GoToBeginner()
    {
        if (!frameActive && gameManager.SetActualCategory(0))
        {
            adController.StopListening();
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
        }
    }
    /// <summary>
    /// Carga la selección de niveles de la categoría Regular.
    /// </summary>
    public void GoToRegular()
    {
        if (!frameActive && gameManager.SetActualCategory(1))
        {
            adController.StopListening();
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
        }
    }
    /// <summary>
    /// Carga la selección de niveles de la categoría Advanced.
    /// </summary>
    public void GoToAdvanced()
    {
        if (!frameActive && gameManager.SetActualCategory(2))
        {
            adController.StopListening();
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
        }
    }
    /// <summary>
    /// Carga la selección de niveles de la categoría Expert.
    /// </summary>
    public void GoToExpert()
    {
        if (!frameActive && gameManager.SetActualCategory(3))
        {
            adController.StopListening();
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
        }
    }
    /// <summary>
    /// Carga la selección de niveles de la categoría Master.
    /// </summary>
    public void GoToMaster()
    {
        if (!frameActive && gameManager.SetActualCategory(4))
        {
            adController.StopListening();
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
        }
    }
    /// <summary>
    /// Cambia el botón de Challenge para que este no tenga el tiempo de espera.
    /// </summary>
    public void UpdateChallenge()
    {
        gameManager.CheckChallenge();
        challengeDoneFrame.gameObject.SetActive(false);
        timeMenuChallege.NotNow();
    }
    /// <summary>
    /// Carga la pantalla del modo challenge.
    /// </summary>
    public void GoToChallenge()
    {
        if (gameManager.CheckChallenge())
        {
            frameActive = true;
            challengeFrame.gameObject.SetActive(true);
        }
    }
    /// <summary>
    /// Desactiva la pantalla de modo challenge.
    /// </summary>
    public void DesactiveChallengeFrame()
    {
        frameActive = false;
        challengeFrame.gameObject.SetActive(false);
    }
    /// <summary>
    /// Carga el anuncio para jugar el modo challenge gratis.
    /// </summary>
    public void ChallengeAd()
    {
        adController.ShowAd();
    }
    /// <summary>
    /// Añade la visualización del anuncio y carga la escena challenge.
    /// </summary>
    public void AdSeen()
    {
        if (gameManager.SetActualCategory(5))
        {
            adController.StopListening();
            SceneManager.LoadScene("Challenge", LoadSceneMode.Single);
        }
    }
    /// <summary>
    /// Añade 25 monedas al darle al regalo diario
    /// </summary>
    public void GiftClicked()
    {
        gameManager.AddCoins(25);
        coins = gameManager.GetCoins();
        coinsText.text = "" + coins;
        gameManager.GiftTaken();
        gameManager.SavePersistance();
    }

    public bool CheckGiftActive()
    {
        return gameManager.CheckGiftActive();
    }
    /// <summary>
    /// Carga el modo challenge pagando monedas.
    /// </summary>
    public void ChallengeCost()
    {
        if (gameManager.SubtractCoins(25) && gameManager.SetActualCategory(5))
        {
            adController.StopListening();
            gameManager.SavePersistance();
            SceneManager.LoadScene("Challenge", LoadSceneMode.Single);
        }
    }
}
