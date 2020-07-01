using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// Clase que gestiona la escena donde se elige el nivel que se va a jugar de la categoría seleccionada.
/// </summary>
public class SelectLevelManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private Text categoryText;

    private string actualCategory;
    private int levelsCompleted;

    [SerializeField]
    private GridLayoutGroup content;
    public Button activeLevel;
    public Button blockedLevel;
    /// <summary>
    /// Muestra los niveles completos y disponibles para esa categoría.
    /// </summary>
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        actualCategory = gameManager.GetNameCategory();
        categoryText.text = actualCategory;

        if(actualCategory == "BEGINNER")
        {
            levelsCompleted = gameManager.GetTotalLevelCompletedOfCategory(0);
        }
        else if (actualCategory == "REGULAR")
        {
            levelsCompleted = gameManager.GetTotalLevelCompletedOfCategory(1);
        }
        else if (actualCategory == "ADVANCED")
        {
            levelsCompleted = gameManager.GetTotalLevelCompletedOfCategory(2);
        }
        else if (actualCategory == "EXPERT")
        {
            levelsCompleted = gameManager.GetTotalLevelCompletedOfCategory(3);
        }
        else if (actualCategory == "MASTER")
        {
            levelsCompleted = gameManager.GetTotalLevelCompletedOfCategory(4);
        }
        CreateBoard(gameManager.GetTotalLevelOfCategory(gameManager.GetActualCategory()),levelsCompleted);

    }
    /// <summary>
    /// LLamado al pulsar el botón de atrás.
    /// </summary>
    public void Back()
    {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }
    /// <summary>
    /// Click en un nivel. Carga la escena de juego con el nivel seleccionado.
    /// </summary>
    /// <param name="levelPress">Nivel seleccinado</param>
    public void Click(int levelPress)
    {
        if (gameManager.SetActualLevel(levelPress) && levelPress >= 0)
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    /// <summary>
    /// Crea las casillas de los niveles que están activos y los que están bloqueados.
    /// </summary>
    /// <param name="totalLevels">Niveles totales que tiene que mostrar</param>
    /// <param name="passedLevels">Niveles que están activos porque ya han sido desbloqueados.</param>
    private void CreateBoard(int totalLevels,int passedLevels)
    {
        Button b;
        for(int i = 0; i < totalLevels; i++)
        {
            if (i <= passedLevels)
            {
                b = Instantiate(activeLevel, content.transform);
                b.GetComponentInChildren<Text>().text = ""+(i+1);
            }
            else
            {
                b = Instantiate(blockedLevel, content.transform);
            }
        }
    }
}
