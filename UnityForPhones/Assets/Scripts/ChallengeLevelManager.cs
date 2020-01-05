using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private InputManagerChallege inputManager;
    private bool finNivel = false;
    // Start is called before the first frame update
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

    public void endGame()
    {
        finNivel = true;
        EndGameCanvas.gameObject.SetActive(true);
    }

    public void backButton()
    {
        if (!finNivel)
        {
            gameManager.resetTimerChallenge();
            adController.stopListening();
            SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
        }
    }
    public void okayButton()
    {
        gameManager.resetTimerChallenge();
        gameManager.addCoins(50);
        gameManager.addArchievement();
        gameManager.savePersistance();
        adController.stopListening();
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }

    public void dobleBoostWithAdd()
    {
        adController.showAd();
        gameManager.addArchievement();
    }

    public void timeFinish()
    {
        Destroy(inputManager.gameObject);
        ChallengeFailedCanvas.gameObject.SetActive(true);
    }

    public void okayBottonFailed()
    {
        gameManager.resetTimerChallenge();
        adController.stopListening();
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }

    public void adSeen()
    {
        gameManager.resetTimerChallenge();
        gameManager.addCoins(100);
        gameManager.savePersistance();
        adController.stopListening();
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }

    public bool checkEndGame()
    {
        return finNivel;
    }
}
