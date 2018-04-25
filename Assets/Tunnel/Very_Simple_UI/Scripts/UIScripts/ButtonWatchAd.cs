
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/






using UnityEngine;
using System.Collections;

#if APPADVISORY_ADS
using AppAdvisory.Ads;
#endif

namespace AppAdvisory.VSUI
{
	/// <summary>
	/// Class attached to the watch ad button. Works only on mobile (iOS & Android), with Very Simple Ads : http://u3d.as/oWD
	/// </summary>
	public class ButtonWatchAd : MonoBehaviour
	{
		public string VerySimpleAdsURL = "http://u3d.as/oWD";
		/// <summary>
		/// If player clics on the watch ad button, we call this method. Works only on mobile (iOS & Android) if using Very Simple Ads by App Advisory : http://u3d.as/oWD
		/// </summary>
		public void OnClickedWatchAd()
		{
			#if APPADVISORY_ADS
			AdsManager.instance.ShowVideoAds();
			#else
			Debug.LogWarning("To show video ads, please have a look to Very Simple Ad on the Asset Store, or go to this link: " + VerySimpleAdsURL);
			Debug.LogWarning("Very Simple Ad is already implemented in this asset");
			Debug.LogWarning("Just import the package and you are ready to use it and monetize your game!");
			Debug.LogWarning("Very Simple Ad : " + VerySimpleAdsURL);
			#endif
		}
	}
}
