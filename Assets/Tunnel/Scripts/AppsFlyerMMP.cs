using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppsFlyerMMP : MonoBehaviour {

    void Start()
    {
        // For detailed logging
        //AppsFlyer.setIsDebug (true);
        AppsFlyer.setAppsFlyerKey("aTYJZVwsYCTz8BbnbrDbxL");
#if UNITY_IOS
        //Mandatory - set your AppsFlyer’s Developer key.
        
        //Mandatory - set your apple app ID
        //NOTE: You should enter the number only and not the "ID" prefix
        AppsFlyer.setAppID ("YOUR_APP_ID_HERE");
        AppsFlyer.trackAppLaunch ();
#elif UNITY_ANDROID
        //Mandatory - set your Android package name
        AppsFlyer.setAppID("com.belizard.tunneltwist");
        //Mandatory - set your AppsFlyer’s Developer key.
        AppsFlyer.init("aTYJZVwsYCTz8BbnbrDbxL", "AppsFlyerTrackerCallbacks");

        AppsFlyer.setCustomerUserID("659231");

        //For getting the conversion data in Android, you need to this listener.
        //AppsFlyer.loadConversionData("AppsFlyerTrackerCallbacks");

#endif
    }


    public static void ElementsCollected(int batch)
    {
        Dictionary<string, string> elementsCollected = new Dictionary<string, string>();
        elementsCollected.Add("quantity", batch.ToString());
        AppsFlyer.trackRichEvent("elements_collected", elementsCollected);
        //AppsFlyer.loadConversionData("AppsFlyerTrackerCallbacks");

        //To get the callbacks
        //AppsFlyer.createValidateInAppListener ("AppsFlyerTrackerCallbacks", "onInAppBillingSuccess", "onInAppBillingFailure");

    }

    public static void LevelCompleted()
    {

        Dictionary<string, string> levelCompleted = new Dictionary<string, string>();
        levelCompleted.Add("quantity", "1");
        AppsFlyer.trackRichEvent("level_completed", levelCompleted);
        //AppsFlyer.loadConversionData("AppsFlyerTrackerCallbacks");

        //To get the callbacks
        //AppsFlyer.createValidateInAppListener ("AppsFlyerTrackerCallbacks", "onInAppBillingSuccess", "onInAppBillingFailure");

    }
    
    public static void Combo3()
    {
        Debug.Log("AppsFlyerMMP: Combo 3");
        Dictionary<string, string> combo3 = new Dictionary<string, string>();
        combo3.Add("combo3", "1");
        AppsFlyer.trackRichEvent("combo3", combo3);
    }
    public static void Combo5()
    {
        Debug.Log("AppsFlyerMMP: Combo 5");
        Dictionary<string, string> combo5 = new Dictionary<string, string>();
        combo5.Add("combo5", "1");
        AppsFlyer.trackRichEvent("combo5", combo5);
    }
    public static void Score(int playerScore)
    {
        Debug.Log("AppsFlyerMMP: Score of " + playerScore);
        Dictionary<string, string> score = new Dictionary<string, string>();
        score.Add("score", playerScore.ToString());
        AppsFlyer.trackRichEvent("score", score);
    }
}
