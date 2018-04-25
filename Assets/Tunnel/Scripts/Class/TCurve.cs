
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
	public class TCurve
	{
		public float x;
		public float y;

		public TCurve(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public Vector4 GetVector()
		{
			return new Vector4(this.x,this.y,0,0);
		}
	}
}