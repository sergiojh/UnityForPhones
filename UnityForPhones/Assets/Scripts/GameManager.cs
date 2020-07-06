using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Clase persistente entre escenas ya que controla los aspectos generales que se involucran en toda la ejecución del juego como las monedas
/// o el cambio entre niveles y el guardado del objeto persistencia para su posterior volcado en un archivo.
/// </summary>
public class GameManager : MonoBehaviour
{

    [SerializeField]
    private string[] nameCategories;
    [SerializeField]
    private int[] levelsPerCategory;


    private static GameManager gameManager;
    private int actualLevel;
    private int actualCategory;

    public bool challengeReady;
    private bool giftReady = true;
    private float timer;
    private Persistance persistance;

    /// <summary>
    /// Al incio de la ejecución.
    /// </summary>
    public void Start()
    {
        DontDestroyOnLoad(this);
        //Si es nulo, le doy valor a mi variable estatica
        if (gameManager == null)
        {
            gameManager = this;
        }
        //Relleno con valores por defecto
        gameManager.challengeReady = true;
        gameManager.giftReady = true;
        gameManager.timer = 1800.0f;
    }
    /// <summary>
    /// Permite conseguir la instancia del GameManager
    /// </summary>
    /// <returns></returns>
    public static GameManager GetGameManager()
    {
        if (gameManager == null)
        {
            gameManager = new GameManager();
            gameManager.challengeReady = true;
            gameManager.giftReady = true;
            gameManager.timer = 1800.0f;
        }

        return gameManager;
    }

    void Update()
    {
        timer -= Time.deltaTime;
    }
    /// <summary>
    /// Añade el número de monedas a las actuales.
    /// </summary>
    /// <param name="value">Monedas que añadir.</param>
    public void AddCoins(int value)
    {
        persistance.coins += value;
    }
    /// <summary>
    /// Resta un número de monedas a las actuales.
    /// </summary>
    /// <param name="value">Monedas que restar.</param>
    /// <returns></returns>
    public bool SubtractCoins(int value)
    {
        if (persistance.coins - value >= 0)
        {
            persistance.coins -= value;
            return true;
        }
        return false;
    }
    /// <summary>
    /// Devuelve las monedas que tiene el usuario.
    /// </summary>
    /// <returns>Número de monedas que tiene el usuario.</returns>
    public int GetCoins()
    {
        return persistance.coins;
    }
    /// <summary>
    /// Setea el nivel actual al nivel dado.
    /// </summary>
    /// <param name="levelNumber">Nivel que pasará a ser el actual.</param>
    /// <returns></returns>
    public bool SetActualLevel(int levelNumber)
    {
        if (levelNumber <= persistance.progress[actualCategory] + 1)
        {
            actualLevel = levelNumber;
            return true;
        }
        return false;
    }
    /// <summary>
    /// Setea la categoría actual por una categoría dada.
    /// </summary>
    /// <param name="numberCategory">Categoría a la que queremos cambiar.</param>
    /// <returns>True enc aso de que pueda cambiarse a esa categoría dada, false en caso contrario.</returns>
    public bool SetActualCategory(int numberCategory)
    {
        if (nameCategories.Length > numberCategory)
        {
            actualCategory = numberCategory;
            return true;
        }
        return false;
    }
    /// <summary>
    /// Devuelve el nombre de la categoría actual.
    /// </summary>
    /// <returns>Nombre de la categoría actual.</returns>
    public string GetNameCategory()
    {
        return nameCategories[actualCategory];
    }
    /// <summary>
    /// Devuelve el nombre de la categoria por index
    /// </summary>
    /// <param name="index">Index de la categoria</param>
    /// <returns>Nombre de la categoría si no devuelvo vacio como error</returns>
    public string GetNameCategoryByIndex(int index)
    {
        if (index >= 0 && index < nameCategories.Length)
            return nameCategories[index];
        else
            return "";
    }

    /// <summary>
    /// Devuelve la categoría actual.
    /// </summary>
    /// <returns>Categoría actual.</returns>
    public int GetActualCategory()
    {
        return actualCategory;
    }
    /// <summary>
    /// Devuelve el nivel actual.
    /// </summary>
    /// <returns>Nivel actual.</returns>
    public int GetActualLevel()
    {
        return actualLevel;
    }
    /// <summary>
    /// </summary>
    /// <param name="category">Categoría para la que queremos los niveles.</param>
    /// <returns>Niveles para una categoría dada.</returns>
    public int GetTotalLevelOfCategory(int category)
    {
        return levelsPerCategory[category];
    }
    /// <summary>
    /// Devuelve los niveles completados para una categoría dada.
    /// </summary>
    /// <param name="category">Categoría para la que queremos los niveles completados.</param>
    /// <returns>Niveles completados para una categoría dada.</returns>
    public int GetTotalLevelCompletedOfCategory(int category)
    {
        return persistance.progress[category];
    }
    /// <summary>
    /// </summary>
    /// <returns>Niveles totales del juego.</returns>
    public int GetTotalLevels()
    {
        int levelN = 0;
        foreach (int i in levelsPerCategory)
        {
            levelN += i;
        }
        return levelN;
    }
    /// <summary>
    /// Método llamado al completarse un nivel. Si es un nivel nuevo aumenta tu progreso.
    /// </summary>
    public void LevelCompleted()
    {
        if (actualLevel > persistance.progress[actualCategory])
        {
            persistance.progress[actualCategory]++;
        }
    }
    /// <summary>
    /// Añade una medalla a los challenges completados.
    /// </summary>
    public void AddArchievement()
    {
        persistance.archievement++;
    }
    /// <summary>
    /// </summary>
    /// <returns>Número de medallas conseguidas.</returns>
    public int GetArchievement()
    {
        return persistance.archievement;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns>Número de niveles completados de la categoría actual.</returns>
    public int GetLevelsCompletedFromActualLevel()
    {
        return persistance.progress[actualCategory];
    }
    /// <summary>
    /// Carga el archivo que contiene la persistencia, en caso de que este no exista, carga una persistencia por defecto.
    /// </summary>
    public void LoadPersistance()
    {
        PersistanceManager p = new PersistanceManager();
        persistance = p.LoadPersistance("BEGINNER", nameCategories.Length);
    }

    /// <summary>
    /// Setea los niveles que están completados de una categoría dada.
    /// </summary>
    /// <param name="category">Categoría del juego</param>
    /// <param name="levelCompleted">Número de niveles completados para la categoría.</param>
    public void SetLevelsCompleted(int category, int levelCompleted)
    {
        persistance.progress[category] = levelCompleted;
    }
    /// <summary>
    /// Comprobación si el modo Challenge está listo para usarse. En caso de que el tiempo de espera llegue a 0, activa el modo para que el usuario pueda usarlo.
    /// </summary>
    /// <returns>True en caso de que pueda usarse y false en caso contrario.</returns>
    public bool CheckChallenge()
    {

        if (challengeReady)
            return true;
        else if (timer <= 0.0f)//30 min
        {
            challengeReady = true;
            timer = 1800.0f;
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// Devuelve la referencia al objeto que maneja el tiempo.
    /// </summary>
    /// <returns>Objeto Timer</returns>
    public float GetTimer()
    {
        return timer;
    }

    /// <summary>
    /// Reset del tiempo restante para ejecutar el modo challenge de nuevo.
    /// </summary>
    public void ResetTimerChallenge()
    {
        timer = 1800.0f;
        challengeReady = false;
    }

    /// <summary>
    /// Guardado de la persistencia en un archivo.
    /// </summary>
    public void SavePersistance()
    {
        PersistanceManager p = new PersistanceManager();
        p.SavePersistance(persistance, nameCategories[0]);
    }
    /// <summary>
    /// Activamos que hemos cogido el regalo de la sesión
    /// </summary>
    public void GiftTaken()
    {
        giftReady = false;
    }
    /// <summary>
    /// Miramos si el regalo está cogido en la sesión
    /// </summary>
    /// <returns>Retornamos true si aun no se ha cogido y falso si se ha cogido el regalo</returns>
    public bool CheckGiftActive()
    {
        return giftReady;
    }
}
