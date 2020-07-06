using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdController : MonoBehaviour, IUnityAdsListener
{
    /// <summary>
    /// La id que tiene nuestra aplicación en Unity. Se mira el Dashboard de Unity para sacarlo.
    /// </summary>
    protected string store_id = "3419076";
    /// <summary>
    /// Tipo de Ad que queremos meter.
    /// </summary>
    protected string rewarded_video_ad = "rewardedVideo";

    /// <summary>
    /// Inicialización de Ads y añadimos este objeto como Listener de Ads.
    /// </summary>
    protected void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(store_id, true);
    }

    /// <summary>
    /// Método para mostrar el anuncio al usuario.
    /// </summary>
    public virtual void ShowAd()
    {
        if (Advertisement.IsReady(rewarded_video_ad))
        {
            Advertisement.Show(rewarded_video_ad);
        }
    }

    // Implement IUnityAdsListener interface methods:

    /// <summary>
    /// Funcionalidad que será ejecutada por Unity cuando el anuncio termina.
    /// </summary>
    /// <param name="showResult">Enumerado para saber si el anuncio se ha visto, se ha pasado o ha dado error.</param>
    public virtual void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            Debug.Log("Completed");
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

    /// <summary>
    /// Métodos heredados que están obligados a ser implementados, en nuestro caso no necesitamos que estos tengan funcionalidad.
    /// </summary>
    public void OnUnityAdsReady(string placementId)
    {

    }
    /// <summary>
    /// Métodos heredados que están obligados a ser implementados, en nuestro caso no necesitamos que estos tengan funcionalidad.
    /// </summary>
    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }
    /// <summary>
    /// Métodos heredados que están obligados a ser implementados, en nuestro caso no necesitamos que estos tengan funcionalidad.
    /// </summary>
    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
    /// <summary>
    /// El objeto deja de ser Listener de Ads.
    /// </summary>
    public void StopListening()
    {
        Advertisement.RemoveListener(this);
    }
}
