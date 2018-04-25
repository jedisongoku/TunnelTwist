
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/





using UnityEngine;
using System.Collections;

namespace AppAdvisory.VSUI
{
	/// <summary>
	/// Attached to like button
	/// </summary>
	public class ButtonLike : MonoBehaviour 
	{
		/// <summary>
		/// URL use if the Facebook app is present in the mobile/tablet.
		/// </summary>
		public string facebookApp = "fb://profile/515431001924232";
		/// <summary>
		/// URL use if the Facebook app is not present or if we failed to call it quickly.
		/// </summary>
		public string facebookAddress = "https://www.facebook.com/appadvisory";

		/// <summary>
		/// If player clicks on the Facebook button, we call this method.
		/// </summary>
		public void OnClickedFacebookLikeButton()
		{
			float startTime;
			startTime = Time.timeSinceLevelLoad;

			//open the facebook app
			Application.OpenURL(facebookApp);

			if (Time.timeSinceLevelLoad - startTime <= 1f)
			{
				//fail. Open safari.
				Application.OpenURL(facebookAddress);
			}
		}
	}
}