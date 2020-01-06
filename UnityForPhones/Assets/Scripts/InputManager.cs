using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase que controla el Input en la escena juego.
/// </summary>
public class InputManager : MonoBehaviour
{
    [SerializeField]
    private BoardManager boardContainer;
    [SerializeField]
    private LevelManager levelManager;
    [SerializeField]
    private List<SpriteRenderer> TypeClickTrackers;

    private SpriteRenderer clickTracker;
    /// <summary>
    /// Inicializa el Tracker con la misma piel que tengan los Tiles del Board.
    /// </summary>
    /// <param name="piel">Piel a usar por el Tracker.</param>
    public void Init(int piel)
    {
        clickTracker = Instantiate(TypeClickTrackers[piel],this.transform);
        clickTracker.enabled = false;
    }
    /// <summary>
    /// Devuelve el Tracker.
    /// </summary>
    /// <returns>Tracker de la pulsación del usuario.</returns>
    public SpriteRenderer getClickTracker()
    {
        return clickTracker;
    }

    /// <summary>
    /// Comprueba las pulsaciones del usuario en cada tick.
    /// </summary>
    void Update()
    {
        //Código específico de Android
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
                bool fin = boardContainer.checkFinJuego();

                if (fin)
                {
                    levelManager.finLevel();
                    Destroy(this.gameObject);
                }
            }
            else
            {
                clickTracker.enabled = false;
            }
        }

#else
        //Código específico de Windows y Editor.
        if (Input.GetMouseButton(0))
        {
            clickTracker.enabled = true;
            var v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            v.z = -1;
            clickTracker.transform.position = v;
            v = boardContainer.transform.InverseTransformPoint(v);
            boardContainer.Clicked(v);
            bool fin = boardContainer.checkFinJuego();
            if (fin)
            {
                levelManager.finLevel();
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
