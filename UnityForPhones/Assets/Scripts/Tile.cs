using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer _onImage;
    [SerializeField]
    private SpriteRenderer _offImage;

    /// <summary>
    /// La colocacion es Norte, Sur, Oeste y Este
    /// </summary>
    [SerializeField]
    private SpriteRenderer[] _pathDirection;

    /// <summary>
    /// La colocacion es Norte, Sur, Oeste y Este
    /// </summary>
    [SerializeField]
    private SpriteRenderer[] _pathHintDirection;

    private bool _pulsado = false;
    private int _pathActive = -1;

    // Start is called before the first frame update
    void Start()
    {

        foreach (var sprite in _pathDirection)
            sprite.gameObject.SetActive(false);
        foreach (var sprite in _pathHintDirection)
            sprite.gameObject.SetActive(false);

        if (_pulsado)
        {
            _onImage.gameObject.SetActive(true);
            _offImage.gameObject.SetActive(false);
        }
        else
        {
            _onImage.gameObject.SetActive(false);
            _offImage.gameObject.SetActive(true);
        }
    }
    public bool CheckPulsado()
    {
        return _pulsado;
    }
    /// <summary>
    /// 0 ->  Norte
    /// 1 ->  Sur
    /// 2 ->  Oeste
    /// 3 ->  Este
    /// </summary>
    /// <param name="value">debe estar entre 0-3</param>
    public void setActivePath(int value)
    {
        if (value >= 0 && value < 4)
        {
            _pathActive = value;
            _pathDirection[value].gameObject.SetActive(true);
        }
        else
            Debug.LogError("La variable no esta contenida entre 0 y 3");
    }
    /// <summary>
    /// 0 ->  Norte
    /// 1 ->  Sur
    /// 2 ->  Oeste
    /// 3 ->  Este
    /// </summary>
    /// <param name="value">debe estar entre 0-3</param>
    public void setActiveHintPath(int value)
    {
        if (value >= 0 && value < 4)
        {
            _pathHintDirection[value].gameObject.SetActive(true);
        }
        else
            Debug.LogError("La variable no esta contenida entre 0 y 3");
    }
    /// <summary>
    /// Deshabilito mi path si existe, si no lanzo un error por consola
    /// </summary>
    /// <returns>el camino hacia atras si no es valido retorno -1</returns>
    public int DisableActivedPath()
    {
        if(_pathActive != -1)
        {
            _pathDirection[_pathActive].gameObject.SetActive(false);
            SetPulsado(false);
            int value = _pathActive;
            _pathActive = -1;
            return value;
        }
        else
        {
            Debug.Log("No habia path activo");
            return -1;
        }
    }
    /// <summary>
    /// si esta pulsado activo/inactivo tambien el sprite correspondiente
    /// </summary>
    /// <param name="value">Control of flag touched</param>
    public void SetPulsado(bool value)
    {
        _pulsado = value;

        if (_pulsado)
        {
            _onImage.gameObject.SetActive(true);
            _offImage.gameObject.SetActive(false);
        }
        else
        {
            _onImage.gameObject.SetActive(false);
            _offImage.gameObject.SetActive(true);
        }
    }
}
