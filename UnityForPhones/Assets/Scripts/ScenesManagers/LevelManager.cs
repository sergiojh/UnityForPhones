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
    private InputManagerGameplay inputManager;
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
    private AdControllerGame adController;

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
        gameManager = GameManager.GetInstance();
        coins = gameManager.GetCoins();
        actualLevel = gameManager.GetActualLevel();
        actualCategory = gameManager.GetNameCategory();

        coinsText.text = "" + coins;
        categoryText.text = actualCategory+ " " + actualLevel;
        priceText.text = "" + price;

        int levelLoad = gameManager.GetActualLevel();
        int numberCategory = gameManager.GetActualCategory();
        int numOfTiles = boardManager.GetTotalTypeTiles();
        int r = Random.Range(0,numOfTiles); // es exclusivo en el valor max
        inputManager.Init(r);
        boardManager.InitBoard(numberCategory,levelLoad - 1, r, inputManager.GetClickTracker());
        container.gameObject.SetActive(false);
    }
    /// <summary>
    /// Método ejecutado al usar el botón Back. Carga la escena de selección de niveles.
    /// </summary>
    public void Back()
    {
        if (!finNivel)
        {
            adController.StopListening();
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
        }
    }
    /// <summary>
    /// Carga el siguiente nivel.
    /// </summary>
    public void LoadNextLevel()
    {
        adController.StopListening();
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    /// <summary>
    /// Compra de pistas. Resta monedas y activa 5 caminos. En caso de no haber más pistas disponibles, no gasta monedas.
    /// </summary>
    public void BuyHints()
    {
        if (!finNivel)
        {
            if (gameManager.SubtractCoins(price))
            {
                if (boardManager.ActiveMoreHints())
                {
                    coins = gameManager.GetCoins();
                    coinsText.text = "" + coins;
                }
                else
                    gameManager.AddCoins(price);
            }
            else
                Debug.Log("No hay suficiente dinero");
        }
    }
    /// <summary>
    /// Devuelve el tablero a su estado inicial.
    /// </summary>
    public void ResetBoard()
    {
        if(!finNivel)
            boardManager.ResetBoard();
    }
    /// <summary>
    /// Se visualiza un anuncio para ganar monedas. Solo puede verse 1 anuncio por nivel.
    /// </summary>
    public void ViewAdd()
    {
        if (!finNivel && !adSeen)
        {
            adSeen = true;
            adController.ShowAd();
        }
    }
    /// <summary>
    /// Se ejecuta al finalizarse el nivel lo que guarda en la persistencia que se ha completado.
    /// </summary>
    public void FinLevel()
    {
        gameOverCategory.text = "" + actualCategory;
        gameOverLevel.text = "" + actualLevel;
        finNivel = true;
        adController.ShowAd();
        //quitamos puntos ya que el ad da 25 y este ad es de no recompensa
        gameManager.SubtractCoins(25);
        container.gameObject.SetActive(true);
        gameManager.LevelCompleted();
        gameManager.SetActualLevel(gameManager.GetActualLevel() + 1);
        gameManager.SavePersistance();
    }
    /// <summary>
    /// Devuelve al usuario al menú principal.
    /// </summary>
    public void GoHome()
    {
        if (finNivel)
        {
            adController.StopListening();
            SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
        }
    }
    /// <summary>
    /// Devuelve las monedas que tiene el ususario.
    /// </summary>
    /// <returns>Número de monedas del usuario.</returns>
    public int GetCoins()
    {
        return coins;
    }
    /// <summary>
    /// Suma monedas a las actuales.
    /// </summary>
    /// <param name="value">Monedas a sumar.</param>
    public void AddCoins(int value)
    {
        gameManager.AddCoins(25);
        gameManager.SavePersistance();
        coins = gameManager.GetCoins();
        coinsText.text = "" + coins;
    }
}
