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
        if (Input.GetMouseButton(0))
        {

            var v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            v.z = -1;
            v = layoutGroup.transform.InverseTransformPoint(v);
            v.z = -1;

            int x = ((int)v.x - 100)/100;
            int y = (-1*(int)v.y)/100;

            int levelPress = x + y * 5 + 1;
            selectLevelManager.click(levelPress);
        }
    }
}
