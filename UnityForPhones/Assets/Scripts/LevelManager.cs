using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.IO;

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

    private bool finNivel = false;


    private JSONMapReader jsonReader = new JSONMapReader();

    // Start is called before the first frame update
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
        boardManager.initBoard("/maps.json",levelLoad - 1, r);

        inputManager.Init(r);

        container.gameObject.SetActive(false);
    }

    public void back()
    {
        if (!finNivel)
        {
            gameManager.setActualLevel(0);
            adController.stopListening();
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
        }
    }

    public void loadNextLevel()
    {
        adController.stopListening();
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

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

    public void resetBoard()
    {
        if(!finNivel)
            boardManager.resetBoard();
    }

    public void viewAdd()
    {
        if (!finNivel)
        {
            adController.showAd();
        }
    }

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

    public void goHome()
    {
        if (finNivel)
        {
            gameManager.setActualLevel(0);
            adController.stopListening();
            SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
        }
    }

    public int getCoins()
    {
        return coins;
    }
    public void addCoins(int value)
    {
        gameManager.addCoins(25);
        coins = gameManager.getCoins();
        coinsText.text = "" + coins;
    }
}
