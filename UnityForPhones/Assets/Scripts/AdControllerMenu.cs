using UnityEngine;
using UnityEngine.Advertisements;

/// <summary>
/// Clase que controla todo lo que ocurre con los anuncios usando UnityAds. Hereda de AdController ya que únicamente cambia la funcionalidad el método OnUnityAdsDidFinish.
/// Esta clase es usada para que el usuario no pueda ver más de un anuncio por sesión. Es utilizado en el caso de ver un anuncio para ejecutar el modo Challenge.
/// </summary>
public class AdControllerMenu : AdController
{
    [SerializeField]
    private MenuManager menuManager;

    public override void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            menuManager.adSeen();
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
            Debug.Log("SALTADO");

        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }
}
