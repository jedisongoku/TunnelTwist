
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using System;
using System.Collections;

namespace AppAdvisory.TunnelAndTwist
{
	[Serializable]
	public struct TUNNELColor 
	{
		public float h,s,l,a;
		public TUNNELColor(float h, float s, float l, float a = 1){this.h = h; this.s = s; this.l = l; this.a = a;}
		public TUNNELColor(Color c){TUNNELColor temp = Utils.FromColor(c); h = temp.h; s = temp.s; l = temp.l; a = temp.a;}
		public static implicit operator TUNNELColor(Color src){return Utils.FromColor(src);}
		public static implicit operator Color(TUNNELColor src){return Utils.ToColor(src);}
	}
}