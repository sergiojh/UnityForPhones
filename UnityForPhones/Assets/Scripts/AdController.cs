using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class AdController : MonoBehaviour, IUnityAdsListener
{

    protected string store_id = "3419076";
    protected string rewarded_video_ad = "rewardedVideo";
    [SerializeField]
    private LevelManager levelManager;
    // Start is called before the first frame update
    protected void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(store_id,true);
    }


    public void showAd()
    {
        if (Advertisement.IsReady(rewarded_video_ad))
        {
            Advertisement.Show(rewarded_video_ad);
            Debug.Log("viendo video");
        }
    }

    // Update is called once per frame

    // Implement IUnityAdsListener interface methods:
    public virtual void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            Debug.Log("añado coins");
            levelManager.addCoins(25);
            Debug.Log("vuelvo de añadir");
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

    public void OnUnityAdsReady(string placementId)
    {
        
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    public void stopListening()
    {
        Advertisement.RemoveListener(this);
    }
}
