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
    //"/Resources/Maps/map2.json"
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
        boardManager.initBoard("/Resources/Maps/maps.json",levelLoad);

        container.gameObject.SetActive(false);
    }

    public void back()
    {
        if (!finNivel)
        {
            gameManager.setActualLevel(0);
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
        }
    }

    public void loadNextLevel()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void buyHints()
    {
        if (!finNivel)
        {
            if (gameManager.subtractCoins(price))
            {
                coins = gameManager.getCoins();
                coinsText.text = "" + coins;
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
        //animacion de pulsado de botones con los putos sprites truñer esos chinos que ahora se pone en gris mendrugo (from: Pablo to: Pablo)
    }

    public void goHome()
    {
        if(finNivel)
            SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }

    public int getCoins()
    {
        return coins;
    }
    public void addCoins(int value)
    {
        gameManager.addCoins(value);
        coins = gameManager.getCoins();
        coinsText.text = "" + coins;
    }
    

}
