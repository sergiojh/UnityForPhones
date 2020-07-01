using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Clase que se encarga de controlar el tiempo que está en el Menú.
/// </summary>
public class TimeMenuChallege : MonoBehaviour
{

    [SerializeField]
    private Text chrono;
    [SerializeField]
    private MenuManager menuManager;

    private float _timer;
    /// <summary>
    /// Inicialización de los atributos del tiempo y el texto de este.
    /// </summary>
    /// <param name="timer">Tiempo.</param>
    public void Init(float timer)
    {
        _timer = timer;
        int min = (int)_timer % 60;

        int second = (int)_timer - ((int)_timer % 60) * 60;
        
        string timeLeft = "";
        if (min > 9)
            timeLeft += min + ":";
        else
            timeLeft += "0" + min + ":";
        if(second > 9)
            timeLeft += second;
        else
            timeLeft += "0" + second;

        chrono.text = timeLeft;
    }

    /// <summary>
    /// Destruye el objeto tiempo ya que el Challenge puede realizarse.
    /// </summary>
    public void NotNow()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        _timer -= Time.deltaTime;
        int second = (int)_timer % 60;

        int min = (int)_timer/60;

        string timeLeft = "";
        if (min > 9)
            timeLeft += min + ":";
        else
            timeLeft += "0" + min + ":";
        if (second > 9)
            timeLeft += second;
        else
            timeLeft += "0" + second;

        chrono.text = timeLeft;

        if (_timer <= 0.0f)
            menuManager.UpdateChallenge();
    }
}
