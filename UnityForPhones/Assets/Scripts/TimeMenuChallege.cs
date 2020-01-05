using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeMenuChallege : MonoBehaviour
{

    [SerializeField]
    private Text chrono;
    [SerializeField]
    private MenuManager menuManager;

    private float _timer;
    // Start is called before the first frame update
    public void init(float timer)
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

    public void notNow()
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
            menuManager.updateChallenge();
    }
}
