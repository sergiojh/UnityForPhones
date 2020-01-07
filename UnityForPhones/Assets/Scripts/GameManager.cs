﻿using System.Collections;
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
    private float timer;
    private Persistance persistance;

    /// <summary>
    /// Al incio de la ejecución.
    /// </summary>
    public void starRuning()
    {
        DontDestroyOnLoad(this);

        if (gameManager == null)
        {
            gameManager = this;
            challengeReady = true;
            timer = 1800.0f;
}
        else
        {
            Destroy(this.gameObject);
        }
    }


    void Awake()
    {
        DontDestroyOnLoad(this);

        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Update()
    {
        timer -= Time.deltaTime;
    }
    /// <summary>
    /// Añade el número de monedas a las actuales.
    /// </summary>
    /// <param name="value">Monedas que añadir.</param>
    public void addCoins(int value)
    {
        persistance.coins += value;
    }
    /// <summary>
    /// Resta un número de monedas a las actuales.
    /// </summary>
    /// <param name="value">Monedas que restar.</param>
    /// <returns></returns>
    public bool subtractCoins(int value)
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
    public int getCoins()
    {
        return persistance.coins;
    }
    /// <summary>
    /// Setea el nivel actual al nivel dado.
    /// </summary>
    /// <param name="levelNumber">Nivel que pasará a ser el actual.</param>
    /// <returns></returns>
    public bool setActualLevel(int levelNumber)
    {
        int valor = 0;
        switch (actualCategory)
        {
            case 0:
                valor = persistance.progress;
                break;
            case 1:
                valor = persistance.progress1;
                break;
            case 2:
                valor = persistance.progress2;
                break;
            case 3:
                valor = persistance.progress3;
                break;
            case 4:
                valor = persistance.progress4;
                break;
        }

        if (levelNumber <= valor + 1)
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
    public bool setActualCategory(int numberCategory)
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
    public string getNameCategory()
    {
        return nameCategories[actualCategory];
    }
    /// <summary>
    /// Devuelve la categoría actual.
    /// </summary>
    /// <returns>Categoría actual.</returns>
    public int getActualCategory()
    {
        return actualCategory;
    }
    /// <summary>
    /// Devuelve el nivel actual.
    /// </summary>
    /// <returns>Nivel actual.</returns>
    public int getActualLevel()
    {
        return actualLevel;
    }
    /// <summary>
    /// </summary>
    /// <param name="category">Categoría para la que queremos los niveles.</param>
    /// <returns>Niveles para una categoría dada.</returns>
    public int getTotalLevelOfCategory(int category)
    {
        return levelsPerCategory[category];
    }
    /// <summary>
    /// Devuelve los niveles completados para una categoría dada.
    /// </summary>
    /// <param name="category">Categoría para la que queremos los niveles completados.</param>
    /// <returns>Niveles completados para una categoría dada.</returns>
    public int getTotalLevelCompletedOfCategory(int category)
    {
        switch (category)
        {
            case 0:
                return persistance.progress;
            case 1:
                return persistance.progress1;
            case 2:
                return persistance.progress2;
            case 3:
                return persistance.progress3;
            case 4:
                return persistance.progress4;
        }
        return -1;
    }
    /// <summary>
    /// </summary>
    /// <returns>Niveles totales del juego.</returns>
    public int getTotalLevels()
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
    public void levelCompleted()
    {
        switch (actualCategory)
        {
            case 0:
                if (actualLevel > persistance.progress)
                    persistance.progress++;
                break;
            case 1:
                if (actualLevel > persistance.progress1)
                    persistance.progress1++;
                break;
            case 2:
                if (actualLevel > persistance.progress2)
                    persistance.progress2++;
                break;
            case 3:
                if (actualLevel > persistance.progress3)
                    persistance.progress3++;
                break;
            case 4:
                if (actualLevel > persistance.progress4)
                    persistance.progress4++;
                break;
        }
    }
    /// <summary>
    /// Añade una medalla a los challenges completados.
    /// </summary>
    public void addArchievement()
    {
        persistance.archievement++;
    }
    /// <summary>
    /// </summary>
    /// <returns>Número de medallas conseguidas.</returns>
    public int getArchievement()
    {
        return persistance.archievement;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns>Número de niveles completados de la categoría actual.</returns>
    public int getLevelsCompletedFromActualLevel()
    {
        switch (actualCategory)
        {
            case 0:
                return persistance.progress;
            case 1:
                return persistance.progress1;
            case 2:
                return persistance.progress2;
            case 3:
                return persistance.progress3;
            case 4:
                return persistance.progress4;
        }
        return -1;
    }
    /// <summary>
    /// Carga el archivo que contiene la persistencia, en caso de que este no exista, carga una persistencia por defecto.
    /// </summary>
    public void loadPersistance()
    {
        PersistanceManager p = new PersistanceManager();
        persistance = p.loadPersistance(nameCategories[0]);
    }

    /// <summary>
    /// Setea los niveles que están completados de una categoría dada.
    /// </summary>
    /// <param name="category">Categoría del juego</param>
    /// <param name="levelCompleted">Número de niveles completados para la categoría.</param>
    public void setLevelsCompleted(int category, int levelCompleted)
    {
        switch (category)
        {
            case 0:
                persistance.progress = levelCompleted;
                break;
            case 1:
                persistance.progress1 = levelCompleted;
                break;
            case 2:
                persistance.progress2 = levelCompleted;
                break;
            case 3:
                persistance.progress3 = levelCompleted;
                break;
        }
    }
    /// <summary>
    /// Comprobación si el modo Challenge está listo para usarse. En caso de que el tiempo de espera llegue a 0, activa el modo para que el usuario pueda usarlo.
    /// </summary>
    /// <returns>True en caso de que pueda usarse y false en caso contrario.</returns>
    public bool checkChallenge()
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
    public float getTimer()
    {
        return timer;
    }

    /// <summary>
    /// Reset del tiempo restante para ejecutar el modo challenge de nuevo.
    /// </summary>
    public void resetTimerChallenge()
    {
        timer = 1800.0f;
        challengeReady = false;
    }

    /// <summary>
    /// Guardado de la persistencia en un archivo.
    /// </summary>
    public void savePersistance()
    {
        PersistanceManager p = new PersistanceManager();
        p.savePersistance(persistance, nameCategories[0]);
    }
}
