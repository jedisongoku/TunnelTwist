
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if AADOTWEEN
using DG.Tweening;
#endif


namespace AppAdvisory.TunnelAndTwist
{
	public class MaterialManager : MonoBehaviour 
	{
		public List<Material> materialMainColorChange;
		public List<Material> materialPointColorChange;
		public List<Material> materialCurveChange;

		void Awake()
		{
			float dist = 300f;

			foreach(var m in materialCurveChange)
			{
				m.SetFloat("_Dist", dist);
				m.SetVector("_QOffset", Vector4.zero);
			}
		}

		void OnEnable()
		{
			EventManager.OnChangeCurve += OnChangeCurve;
			EventManager.OnChangePlatformColor += OnChangePlatformColor;
			EventManager.OnChangePointColor += OnChangePointColor;
		}


		void OnDisable()
		{
			EventManager.OnChangeCurve -= OnChangeCurve;
			EventManager.OnChangePlatformColor -= OnChangePlatformColor;
			EventManager.OnChangePointColor -= OnChangePointColor;
		}

		void OnChangeCurve (TCurve c)
		{
			for(int i = 0; i < materialCurveChange.Count; i++)
			{
				var m = materialCurveChange[i];
				m.SetVector("_QOffset", c.GetVector());
			}
		}

		void OnChangePlatformColor(Color c)
		{
			foreach(var m in materialMainColorChange)
			{
				m.color = c;
			}
		}

		void OnChangePointColor(Color c)
		{
			foreach(var m in materialPointColorChange)
			{
				m.SetColor("_Color",c);
			}
		}
	}
}