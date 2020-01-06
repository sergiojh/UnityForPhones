using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerGameplay : MonoBehaviour
{
    [SerializeField]
    protected List<SpriteRenderer> TypeClickTrackers;
    [SerializeField]
    protected BoardManager boardContainer;

    protected SpriteRenderer clickTracker;
    /// <summary>
    /// Inicializa el Tracker con la misma piel que tengan los Tiles del Board.
    /// </summary>
    /// <param name="piel">Piel a usar por el Tracker.</param>
    public void Init(int piel)
    {
        clickTracker = Instantiate(TypeClickTrackers[piel], this.transform);
        clickTracker.enabled = false;
    }
}
