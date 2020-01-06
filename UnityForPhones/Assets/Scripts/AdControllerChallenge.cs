using UnityEngine;
using UnityEngine.Advertisements;
/// <summary>
/// Clase que controla todo lo que ocurre con los anuncios usando UnityAds. Hereda de AdController ya que únicamente cambia la funcionalidad el método OnUnityAdsDidFinish.
/// </summary>
public class AdControllerChallenge : AdController
{
    [SerializeField]
    private ChallengeLevelManager challengeLevelManager;

    public override void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            if (challengeLevelManager == null)
                challengeLevelManager = FindObjectOfType<ChallengeLevelManager>();
            challengeLevelManager.adSeen();
            
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
