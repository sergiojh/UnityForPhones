using UnityEngine;
using UnityEngine.Advertisements;

/// <summary>
/// Clase que controla todo lo que ocurre con los anuncios usando UnityAds.
/// </summary>
public class AdControllerGame : AdController
{
    [SerializeField]
    private LevelManager levelManager;

    // Implement IUnityAdsListener interface methods:

    /// <summary>
    /// Funcionalidad que será ejecutada por Unity cuando el anuncio termina.
    /// </summary>
    /// <param name="showResult">Enumerado para saber si el anuncio se ha visto, se ha pasado o ha dado error.</param>
    public override void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            levelManager.addCoins(25);
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
            Debug.Log("Skipped");

        }
        else if (showResult == ShowResult.Failed)
        { 
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }
}
