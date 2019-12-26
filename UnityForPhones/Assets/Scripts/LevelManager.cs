using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        coins = gameManager.getCoins();
        actualLevel = gameManager.getActualLevel();
        actualCategory = gameManager.getNameCategory();

        coinsText.text = "" + coins;
        categoryText.text = actualCategory+ " " + actualLevel;
        priceText.text = "" + price;

        boardManager.initBoard("/Resources/Maps/map2.json",1);
    }
}
