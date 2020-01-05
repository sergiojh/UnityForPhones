using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputManagerLevelSelect : MonoBehaviour
{
    [SerializeField]
    private LayoutGroup layoutGroup;
    [SerializeField]
    private SelectLevelManager selectLevelManager;

    // Update is called once per frame
    void Update()
    {
#if  !UNITY_EDITOR && UNITY_ANDROID

        if(Input.touchCount > 0){
        Touch t = Input.GetTouch(0);
        if (t.phase == TouchPhase.Began || t.phase == TouchPhase.Moved)
        {
            var v = Camera.main.ScreenToWorldPoint(t.position);

            v = layoutGroup.transform.InverseTransformPoint(v);

            int x = ((int)v.x - 100) / 100;
            int y = (-1 * (int)v.y) / 100;

            int levelPress = x + y * 5 + 1;
            selectLevelManager.click(levelPress);
        }
        }
#else
        if (Input.GetMouseButton(0))
        {

            var v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            v.z = -1;
            v = layoutGroup.transform.InverseTransformPoint(v);
            v.z = -1;

            int x = ((int)v.x - 100) / 100;
            int y = (-1 * (int)v.y) / 100;

            int levelPress = x + y * 5 + 1;
            selectLevelManager.click(levelPress);
        }
#endif
    }
}
