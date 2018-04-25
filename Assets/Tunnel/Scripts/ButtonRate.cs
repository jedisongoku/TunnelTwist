
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using System.Collections;


namespace AppAdvisory.TunnelAndTwist
{
	public class ButtonRate : ButtonTransitionAnimation
	{
		public string iOSURL = "itms://itunes.apple.com/us/app/apple-store/id1081376318?mt=8";
		public string ANDROIDURL = "http://app-advisory.com";
		public string AMAZONURL = "http://app-advisory.com";

		public void OnClickedRateUs()
		{
			#if UNITY_IPHONE || UNITY_IOS 
			Application.OpenURL("https://itunes.apple.com/fr/app/tunnel-twist-high-speed-rhythmic/id1081376318?mt=8");
			#endif

			#if UNITY_ANDROID
			Application.OpenURL(ANDROIDURL);
			#endif
		}
	}
}