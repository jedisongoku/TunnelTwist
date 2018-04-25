
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
	/// <summary>
	/// Attached to the button more games. 
	/// </summary>
	public class ButtonMoreGames: ButtonTransitionAnimation
	{
		/// URL for iOS device, change the URL
		/// </summary>
		public string moreGamesIosURL = "http://barouch.fr/moregames.php";
		/// <summary>
		/// URL for Android device, change the URL
		/// </summary>
		public string moreGamesAndroidURL = "http://barouch.fr/moregames.php";
		/// <summary>
		/// URL for other platforms, change the URL
		/// </summary>
		public string moreGamesOtherPlatformsURL = "http://barouch.fr/moregames.php";
		/// <summary>
		/// Call when the button is pressed
		/// </summary>
		public void OnClickedMoreGames()
		{
			if(Application.platform == RuntimePlatform.IPhonePlayer )
			{
				Application.OpenURL(moreGamesIosURL);
			}
			else if(Application.platform == RuntimePlatform.Android)
			{
				Application.OpenURL(moreGamesAndroidURL);
			}
			else
			{
				Application.OpenURL(moreGamesOtherPlatformsURL);
			}
		}
	}
}