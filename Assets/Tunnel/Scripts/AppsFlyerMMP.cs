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


    public static void Score25()
    {
        Debug.Log("AppsFlyerMMP: Score 25");
        Dictionary<string, string> score25 = new Dictionary<string, string>();
        score25.Add("score25", "1");
        AppsFlyer.trackRichEvent("score25", score25);
    }
    public static void Score50()
    {
        Debug.Log("AppsFlyerMMP: Score 50");
        Dictionary<string, string> score50 = new Dictionary<string, string>();
        score50.Add("score50", "1");
        AppsFlyer.trackRichEvent("score50", score50);
    }
    public static void Score75()
    {
        Debug.Log("AppsFlyerMMP: Score 75");
        Dictionary<string, string> score75 = new Dictionary<string, string>();
        score75.Add("score75", "1");
        AppsFlyer.trackRichEvent("score75", score75);
    }
    public static void Score100()
    {
        Debug.Log("AppsFlyerMMP: Score 100");
        Dictionary<string, string> score100 = new Dictionary<string, string>();
        score100.Add("score100", "1");
        AppsFlyer.trackRichEvent("score75", score100);
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
}
