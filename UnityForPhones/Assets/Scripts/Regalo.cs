using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Regalo : MonoBehaviour
{
    [SerializeField]
    private Texture2D regaloActivo;
    [SerializeField]
    private Texture2D regaloInactivo;
    [SerializeField]
    private Image imagenComponent;
    [SerializeField]
    private MenuManager menuManager;

    private bool isActive;
    /// <summary>
    /// Inicializa el regalo
    /// </summary>
    public void Init()
    {
        if (menuManager.CheckGiftActive())
        {
            isActive = true;
            imagenComponent.sprite = Sprite.Create(regaloActivo, new Rect(0, 0, regaloActivo.width, regaloActivo.height), new Vector2(0, 0));
        }
        else
        {
            isActive = false;
            imagenComponent.sprite = Sprite.Create(regaloInactivo, new Rect(0, 0, regaloInactivo.width, regaloInactivo.height), new Vector2(0, 0));
        }
    }
    /// <summary>
    /// Si esta activo, añade las monedas al jugador
    /// </summary>
    public void GetCoins()
    {
        if (isActive)
        {
            isActive = false;
            imagenComponent.sprite = Sprite.Create(regaloInactivo, new Rect(0, 0, regaloInactivo.width, regaloInactivo.height), new Vector2(0, 0));
            menuManager.GiftClicked();
        }
    }

}
