using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// Clase que controla la escena del juego.
/// </summary>
public class LevelManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField]
    private BoardManager boardManager;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Text coinsText;
    [SerializeField]
    private Text categoryText;
    [SerializeField]
    private Text priceText;
    [SerializeField]
    private int price;
    [SerializeField]
    private Canvas container;

    [SerializeField]
    private Text gameOverCategory;
    [SerializeField]
    private Text gameOverLevel;

    [SerializeField]
    private AdController adController;

    private int coins;
    private int actualLevel;
    private string actualCategory;
    private bool adSeen = false;

    private bool finNivel = false;
    /// <summary>
    /// Se encarga de cargar el tablero, la piel que va a usarse y de cargar las monedas de las que dispone el usuario por si quiere comprar pistas.
    /// </summary>
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        coins = gameManager.getCoins();
        actualLevel = gameManager.getActualLevel();
        actualCategory = gameManager.getNameCategory();

        coinsText.text = "" + coins;
        categoryText.text = actualCategory+ " " + actualLevel;
        priceText.text = "" + price;

        int levelLoad = gameManager.getActualLevel() + gameManager.getActualCategory() * 100;

        int numOfTiles = boardManager.getTotalTypeTiles();
        int r = Random.Range(0,numOfTiles); // es exclusivo en el valor max
        inputManager.Init(r);
        boardManager.initBoard("maps",levelLoad - 1, r, inputManager.getClickTracker());
        container.gameObject.SetActive(false);
    }
    /// <summary>
    /// Método ejecutado al usar el botón Back. Carga la escena de selección de niveles.
    /// </summary>
    public void back()
    {
        if (!finNivel)
        {
            gameManager.setActualLevel(0);
            adController.stopListening();
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
        }
    }
    /// <summary>
    /// Carga el siguiente nivel.
    /// </summary>
    public void loadNextLevel()
    {
        adController.stopListening();
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    /// <summary>
    /// Compra de pistas. Resta monedas y activa 5 caminos. En caso de no haber más pistas disponibles, no gasta monedas.
    /// </summary>
    public void buyHints()
    {
        if (!finNivel)
        {
            if (gameManager.subtractCoins(price))
            {
                if (boardManager.ActiveMoreHints())
                {
                    coins = gameManager.getCoins();
                    coinsText.text = "" + coins;
                }
                else
                    gameManager.addCoins(price);
            }
            else
                Debug.Log("No hay suficiente dinero");
        }
    }
    /// <summary>
    /// Devuelve el tablero a su estado inicial.
    /// </summary>
    public void resetBoard()
    {
        if(!finNivel)
            boardManager.resetBoard();
    }
    /// <summary>
    /// Se visualiza un anuncio para ganar monedas. Solo puede verse 1 anuncio por nivel.
    /// </summary>
    public void viewAdd()
    {
        if (!finNivel && !adSeen)
        {
            adSeen = true;
            adController.showAd();
        }
    }
    /// <summary>
    /// Se ejecuta al finalizarse el nivel lo que guarda en la persistencia que se ha completado.
    /// </summary>
    public void finLevel()
    {
        gameOverCategory.text = "" + actualCategory;
        gameOverLevel.text = "" + actualLevel;
        finNivel = true;
        container.gameObject.SetActive(true);
        gameManager.levelCompleted();
        gameManager.setActualLevel(gameManager.getActualLevel() + 1);
        gameManager.savePersistance();
    }
    /// <summary>
    /// Devuelve al usuario al menú principal.
    /// </summary>
    public void goHome()
    {
        if (finNivel)
        {
            gameManager.setActualLevel(0);
            adController.stopListening();
            SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
        }
    }
    /// <summary>
    /// Devuelve las monedas que tiene el ususario.
    /// </summary>
    /// <returns>Número de monedas del usuario.</returns>
    public int getCoins()
    {
        return coins;
    }
    /// <summary>
    /// Suma monedas a las actuales.
    /// </summary>
    /// <param name="value">Monedas a sumar.</param>
    public void addCoins(int value)
    {
        gameManager.addCoins(25);
        gameManager.savePersistance();
        coins = gameManager.getCoins();
        coinsText.text = "" + coins;
    }
}
