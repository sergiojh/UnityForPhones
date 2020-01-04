using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private BoardManager boardContainer;
    [SerializeField]
    private LevelManager levelManager;
    [SerializeField]
    private List<SpriteRenderer> TypeClickTrackers;

    private SpriteRenderer clickTracker;
    public void Init(int piel)
    {
        clickTracker = Instantiate(TypeClickTrackers[piel],this.transform);
        clickTracker.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
#if !UNITY_EDITOR && UNITY_ANDROID

        Touch t = Input.GetTouch(0);
        if (t.phase == TouchPhase.Began || t.phase == TouchPhase.Moved)
        {
            clickTracker.enabled = true;
            var v = Camera.main.ScreenToWorldPoint(t.position);

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

#else
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
