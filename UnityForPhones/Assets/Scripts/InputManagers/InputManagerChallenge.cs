using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que controla el Input en la escena Challenge.
/// </summary>
public class InputManagerChallenge : InputManagerGameplay
{
    [SerializeField]
    private ChallengeLevelManager challengeLevelManager;

    /// <summary>
    /// Devuelve el Tracker.
    /// </summary>
    /// <returns>Tracker de la pulsación del usuario.</returns>
    public SpriteRenderer GetClickTracker()
    {
        return clickTracker;
    }
    /// <summary>
    /// Comprueba las pulsaciones del usuario en cada tick.
    /// </summary>
    void Update()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        if(Input.touchCount > 0){
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began || t.phase == TouchPhase.Moved)
            {
                clickTracker.enabled = true;
                var v = Camera.main.ScreenToWorldPoint(t.position);
                v.z = -1;
                clickTracker.transform.position = v;
                v = boardContainer.transform.InverseTransformPoint(v);
                boardContainer.Clicked(v);
                bool fin = boardContainer.CheckFinJuego();

                if (fin)
                {
                    challengeLevelManager.EndGame();
                    Destroy(this.gameObject);
                }
            }
            else
            {
                clickTracker.enabled = false;
            }
        }

#else
        if (Input.GetMouseButton(0))
        {
            clickTracker.enabled = true;

            var v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            v.z = -1;
            clickTracker.transform.position = v;
            v = boardContainer.transform.InverseTransformPoint(v);
            boardContainer.Clicked(v);
            bool fin = boardContainer.CheckFinJuego();
            if (fin)
            {
                challengeLevelManager.EndGame();
                Destroy(this.gameObject);
            }
        }
        else
        {
            clickTracker.enabled = false;
        }
#endif
    }
}
