using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Clase que controla el nivel de Challenge al completo.
/// </summary>
public class ChallengeLevelManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField]
    private AdControllerChallenge adController;
    [SerializeField]
    private Canvas EndGameCanvas;
    [SerializeField]
    private Canvas ChallengeFailedCanvas;
    [SerializeField]
    private BoardManager boardManager;
    [SerializeField]
    private InputManagerChallenge inputManager;
    private bool finNivel = false;
    /// <summary>
    /// Secarga el nivel al azar, se añade una piel  y se carga el tablero.
    /// </summary>
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        EndGameCanvas.gameObject.SetActive(false);
        ChallengeFailedCanvas.gameObject.SetActive(false);
        int levelLoad = Random.Range(0,gameManager.getTotalLevels());

        int numOfTiles = boardManager.getTotalTypeTiles();
        int piel = Random.Range(0, numOfTiles); // es exclusivo en el valor max
        inputManager.Init(piel);
        boardManager.initBoard("maps", levelLoad, piel, inputManager.getClickTracker());
    }
    /// <summary>
    /// Método ejecutado al terminarse el mapa correctamentre o se acaba el tiempo.
    /// </summary>
    public void endGame()
    {
        finNivel = true;
        EndGameCanvas.gameObject.SetActive(true);
    }
    /// <summary>
    /// Método ejecutado al usar el botón Back.
    /// </summary>
    public void backButton()
    {
        if (!finNivel)
        {
            gameManager.resetTimerChallenge();
            adController.stopListening();
            SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
        }
    }
    /// <summary>
    /// Método ejecutado al usar el botón OK una vez se ha superado el nivel Challenge.
    /// </summary>
    public void okayButton()
    {
        gameManager.resetTimerChallenge();
        gameManager.addCoins(50);
        gameManager.addArchievement();
        gameManager.savePersistance();
        adController.stopListening();
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }
    /// <summary>
    /// Se muestra el anuncio al dar al botón para ganr doble de monedas.
    /// </summary>
    public void dobleBoostWithAdd()
    {
        adController.showAd();
        gameManager.addArchievement();
    }
    /// <summary>
    /// Método que se ejecuta una vez el tiempo ha llegado a 0 y el nivel no se ha completado.
    /// </summary>
    public void timeFinish()
    {
        Destroy(inputManager.gameObject);
        ChallengeFailedCanvas.gameObject.SetActive(true);
    }
    /// <summary>
    /// Pulsación del botón Okay en la pantalla que aparece al fallar el nivel.
    /// </summary>
    public void okayBottonFailed()
    {
        gameManager.resetTimerChallenge();
        adController.stopListening();
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }
    /// <summary>
    /// En el caso de haberse visto el anuncio para ganar el doble de monedas al completarse el nivel.
    /// </summary>
    public void adSeen()
    {
        gameManager.resetTimerChallenge();
        gameManager.addCoins(100);
        gameManager.savePersistance();
        adController.stopListening();
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }
    /// <summary>
    /// Comprueba el final del nivel
    /// </summary>
    /// <returns>True en caso de finalización del nivel, falso en caso contrario.</returns>
    public bool checkEndGame()
    {
        return finNivel;
    }
}
