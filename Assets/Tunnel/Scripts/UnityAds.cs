using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class UnityAds : MonoBehaviour {

    public static UnityAds instance;
    public bool rewardZone;
    public bool isAdShowing = false;
    public int replaysBeforeAd = 3;

    void Awake()
    {
        instance = this;

        if(Application.platform == RuntimePlatform.Android)
        {
            Advertisement.Initialize("1771098", false);
        }
        else if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Advertisement.Initialize("1771099", false);
        }
        else
        {
            Advertisement.Initialize("1771099", false);
        }
    }

    public void ShowAd(string zone = "")
    {
#if UNITY_EDITOR
        //StartCoroutine(WaitForAd());
#endif
        isAdShowing = true;
        if (string.Equals(zone, ""))
            zone = null;
        else
            rewardZone = true;

        ShowOptions options = new ShowOptions();
        options.resultCallback = AdCallbackhandler;

        if (Advertisement.IsReady(zone))
        {
            Advertisement.Show(zone, options);
            Debug.Log("Show AD");
        }
            

        
    }

    void AdCallbackhandler(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("Ad Finished. Rewarding player...");
                if (rewardZone)
                {
                    isAdShowing = false;
                }
                    
                    
                break;
            case ShowResult.Skipped:
                Debug.Log("Ad skipped. Son, I am dissapointed in you");
                break;
            case ShowResult.Failed:
                Debug.Log("I swear this has never happened to me before");
                break;
        }
    }

    IEnumerator WaitForAd()
    {
        float currentTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        yield return null;

        while (Advertisement.isShowing)
            yield return null;

        Time.timeScale = currentTimeScale;
    }
}
