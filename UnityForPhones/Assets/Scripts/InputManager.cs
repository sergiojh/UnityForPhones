﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private BoardManager boardContainer;

    private SpriteRenderer clickTracker;
    void Start()
    {
        clickTracker = gameObject.GetComponentInChildren<SpriteRenderer>();
        clickTracker.enabled = false;
    }

    /*Camera.main.ScreenToWorldPoint();
transform.InverseTransformPoint(,);*/


    // Update is called once per frame
    void Update()
    {
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
                Debug.Log("Completado");
        }
        else
        {
            clickTracker.enabled = false;
            

        }
    }
}
