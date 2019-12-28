using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectLevelManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private Text categoryText;

    private string actualCategory;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        actualCategory = gameManager.getNameCategory();
        categoryText.text = actualCategory;
    }

    public void back()
    {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }
}
