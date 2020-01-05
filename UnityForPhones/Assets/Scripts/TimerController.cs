using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerController : MonoBehaviour
{
    [SerializeField]
    private Text chrono;
    [SerializeField]
    private ChallengeLevelManager challengeLevelManager;

    private int seconds = 30;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        chrono.text = "00:" + seconds;
    }


    // Update is called once per frame
    void Update()
    {
        if (challengeLevelManager.checkEndGame())
            Destroy(this.gameObject);
        timer += Time.deltaTime;

        if (timer >= 1.0f)
        {
            timer -= 1.0f;
            seconds--;
            if (seconds == 0)
            {
                challengeLevelManager.timeFinish();
                Destroy(this.gameObject);
            }
            if(seconds > 9)
                chrono.text = "00:" + seconds;
            else
                chrono.text = "00:0" + seconds;
        }
    }
}
