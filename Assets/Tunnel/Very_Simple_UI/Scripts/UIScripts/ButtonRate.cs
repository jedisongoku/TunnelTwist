
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
	/// Attached to rate button
	/// </summary>
	public class ButtonRate : MonoBehaviour 
	{
				/// <summary>
		/// If player clicks on the rate button, we call this method.
		/// </summary>
		public void OnClickedRate()
		{
			Application.OpenURL(FindObjectOfType<UIController>().URL_STORE);
		}
	}
}