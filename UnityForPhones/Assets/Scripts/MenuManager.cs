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
    private Text archievementText;
    [SerializeField]
    private CanvasRenderer challengeFrame;
    [SerializeField]
    private CanvasRenderer challengeDoneFrame;
    [SerializeField]
    private TimeMenuChallege timeMenuChallege;

    private bool frameActive = false;
    private int coins;
    /// <summary>
    /// Carga la persistencia para mostrar el progreso del usuario en cada categoría, 
    /// las medallas conseguidas en el modo Challenge y las monedas de las que dispone el usuario.
    /// </summary>
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        challengeFrame.gameObject.SetActive(false);
        if (gameManager == null)
        {
            gameManager = Instantiate(prefab, this.transform);
            gameManager.starRuning();
        }
        
        gameManager.loadPersistance();

        coins = gameManager.getCoins();
        coinsText.text = "" + coins;
        archievementText.text = "" + gameManager.getArchievement();
        numLevelBeginnerText.text = "" + gameManager.getTotalLevelCompletedOfCategory(0) + "/" + gameManager.getTotalLevelOfCategory(0);
        numLevelRegularText.text = "" + gameManager.getTotalLevelCompletedOfCategory(1) + "/" + gameManager.getTotalLevelOfCategory(1);
        numLevelAdvancedText.text = "" + gameManager.getTotalLevelCompletedOfCategory(2) + "/" + gameManager.getTotalLevelOfCategory(2);
        numLevelExpertText.text = "" + gameManager.getTotalLevelCompletedOfCategory(3) + "/" + gameManager.getTotalLevelOfCategory(3);
        numLevelMasterText.text = "" + gameManager.getTotalLevelCompletedOfCategory(4) + "/" + gameManager.getTotalLevelOfCategory(4);

        gameManager.setLevelsCompleted(0, gameManager.getTotalLevelCompletedOfCategory(0));
        gameManager.setLevelsCompleted(1, gameManager.getTotalLevelCompletedOfCategory(1));
        gameManager.setLevelsCompleted(2, gameManager.getTotalLevelCompletedOfCategory(2));
        gameManager.setLevelsCompleted(3, gameManager.getTotalLevelCompletedOfCategory(3));
        gameManager.setLevelsCompleted(4, gameManager.getTotalLevelCompletedOfCategory(4));

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
    /// <summary>
    /// Cierra la aplicación.
    /// </summary>
    public void exitApp()
    {
        Application.Quit();
    }
    /// <summary>
    /// Carga la selección de niveles de la categoría Beginner.
    /// </summary>
    public void goToBeginner()
    {
        if (!frameActive && gameManager.setActualCategory(0))
        {
            adController.stopListening();
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
        }
    }
    /// <summary>
    /// Carga la selección de niveles de la categoría Regular.
    /// </summary>
    public void goToRegular()
    {
        if (!frameActive && gameManager.setActualCategory(1))
        {
            adController.stopListening();
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
        }
    }
    /// <summary>
    /// Carga la selección de niveles de la categoría Advanced.
    /// </summary>
    public void goToAdvanced()
    {
        if (!frameActive && gameManager.setActualCategory(2))
        {
            adController.stopListening();
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
        }
    }
    /// <summary>
    /// Carga la selección de niveles de la categoría Expert.
    /// </summary>
    public void goToExpert()
    {
        if (!frameActive && gameManager.setActualCategory(3))
        {
            adController.stopListening();
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
        }
    }
    /// <summary>
    /// Carga la selección de niveles de la categoría Master.
    /// </summary>
    public void goToMaster()
    {
        if (!frameActive && gameManager.setActualCategory(4))
        {
            adController.stopListening();
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
        }
    }
    /// <summary>
    /// Cambia el botón de Challenge para que este no tenga el tiempo de espera.
    /// </summary>
    public void updateChallenge()
    {
        gameManager.checkChallenge();
        challengeDoneFrame.gameObject.SetActive(false);
        timeMenuChallege.notNow();
    }
    /// <summary>
    /// Carga la pantalla del modo challenge.
    /// </summary>
    public void goToChallenge()
    {
        if (gameManager.checkChallenge())
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
    public void challengeAd()
    {
        adController.showAd();
    }
    /// <summary>
    /// Añade la visualización del anuncio y carga la escena challenge.
    /// </summary>
    public void adSeen()
    {
        if (gameManager.setActualCategory(5))
        {
            adController.stopListening();
            SceneManager.LoadScene("Challenge", LoadSceneMode.Single);
        }
    }
    /// <summary>
    /// Añade 25 monedas al darle al regalo diario
    /// </summary>
    public void GiftClicked()
    {
        gameManager.addCoins(25);
        coins = gameManager.getCoins();
        coinsText.text = "" + coins;
    }
    /// <summary>
    /// Carga el modo challenge pagando monedas.
    /// </summary>
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
