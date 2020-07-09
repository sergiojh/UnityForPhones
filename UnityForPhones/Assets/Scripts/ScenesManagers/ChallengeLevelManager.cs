using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// Clase que controla el nivel de Challenge al completo.
/// </summary>
public class ChallengeLevelManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField]
    private Text challengeText;
    [SerializeField]
    private AdControllerChallenge adController;
    [SerializeField]
    private Canvas EndGameCanvas;
    [SerializeField]
    private Canvas ChallengeFailedCanvas;
    [SerializeField]
    private BoardManager boardManager;
    [SerializeField]
    private InputManagerGameplay inputManager;
    private bool finNivel = false;
    /// <summary>
    /// Secarga el nivel al azar, se añade una piel  y se carga el tablero.
    /// </summary>
    void Start()
    {
        gameManager = GameManager.GetInstance();

        challengeText.text = "" + gameManager.GetNameCategoryByIndex(5);

        EndGameCanvas.gameObject.SetActive(false);
        ChallengeFailedCanvas.gameObject.SetActive(false);
    
        int numberCategory = Random.Range(0, gameManager.GetCategoriesLength());
        int levelLoad = Random.Range(0, gameManager.GetTotalLevelOfCategory(numberCategory));
        int numOfTiles = boardManager.GetTotalTypeTiles();
        int piel = Random.Range(0, numOfTiles); // es exclusivo en el valor max
        inputManager.Init(piel);
        boardManager.InitBoard(numberCategory, levelLoad, piel, inputManager.GetClickTracker());
    }
    /// <summary>
    /// Método ejecutado al terminarse el mapa correctamentre o se acaba el tiempo.
    /// </summary>
    public void EndGame()
    {
        finNivel = true;
        EndGameCanvas.gameObject.SetActive(true);
    }
    /// <summary>
    /// Método ejecutado al usar el botón Back.
    /// </summary>
    public void BackButton()
    {
        if (!finNivel)
        {
            gameManager.ResetTimerChallenge();
            adController.StopListening();
            SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
        }
    }
    /// <summary>
    /// Método ejecutado al usar el botón OK una vez se ha superado el nivel Challenge.
    /// </summary>
    public void OkayButton()
    {
        gameManager.ResetTimerChallenge();
        gameManager.AddCoins(50);
        gameManager.AddArchievement();
        gameManager.SavePersistance();
        adController.StopListening();
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }
    /// <summary>
    /// Se muestra el anuncio al dar al botón para ganr doble de monedas.
    /// </summary>
    public void DobleBoostWithAdd()
    {
        adController.ShowAd();
        gameManager.AddArchievement();
    }
    /// <summary>
    /// Método que se ejecuta una vez el tiempo ha llegado a 0 y el nivel no se ha completado.
    /// </summary>
    public void TimeFinish()
    {
        Destroy(inputManager.gameObject);
        ChallengeFailedCanvas.gameObject.SetActive(true);
    }
    /// <summary>
    /// Pulsación del botón Okay en la pantalla que aparece al fallar el nivel.
    /// </summary>
    public void OkayBottonFailed()
    {
        gameManager.ResetTimerChallenge();
        adController.StopListening();
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }
    /// <summary>
    /// En el caso de haberse visto el anuncio para ganar el doble de monedas al completarse el nivel.
    /// </summary>
    public void AdSeen()
    {
        gameManager.ResetTimerChallenge();
        gameManager.AddCoins(100);
        gameManager.SavePersistance();
        adController.StopListening();
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }
    /// <summary>
    /// Comprueba el final del nivel
    /// </summary>
    /// <returns>True en caso de finalización del nivel, falso en caso contrario.</returns>
    public bool CheckEndGame()
    {
        return finNivel;
    }
}
