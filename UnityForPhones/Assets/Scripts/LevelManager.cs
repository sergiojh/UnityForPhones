using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    [SerializeField]
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

    private int coins;
    private int actualLevel;
    private string actualCategory;
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

        boardManager.initBoard("/Resources/Maps/maps.json",1);
    }

    public void back()
    {
        SceneManager.LoadScene("LevelScene",LoadSceneMode.Single);
    }

    public void buyHints()
    {
        if (gameManager.subtractCoins(price))
        {
            coins = gameManager.getCoins();
            coinsText.text = "" + coins;
        }
        else
            Debug.Log("No hay suficiente dinero");
    }

    public void resetBoard()
    {
        boardManager.resetBoard();
    }

    public void viewAdd()
    {
        //AQUI VER ANUNCIO Y SUMAR COINS
        gameManager.addCoins(1);
        coins = gameManager.getCoins();
        coinsText.text = "" + coins;
    }

    public void finLevel()
    {
        Debug.Log("FIN DEL JUEGO");
        //llamar gameManger.completedLevel
        //aumentar los niveles completados por categoria actual
        //RECUARDA CREAR LOS BOTONES PARA AVANZAR A "LEVELSCENE" CON UN NIVEL MAS O VOLVER A selectLevel(ES UNA ESCENA)
        //animacion de pulsado de botones con los putos sprites truñer esos chinos que ahora se pone en gris mendrugo (from: Pablo to: Pablo)
    }

}
