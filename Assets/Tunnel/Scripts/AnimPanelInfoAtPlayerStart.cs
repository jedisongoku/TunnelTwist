
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using System.Collections;
#if AADOTWEEN
using DG.Tweening;
#endif


namespace AppAdvisory.TunnelAndTwist
{
	public class AnimPanelInfoAtPlayerStart : MonoBehaviour 
	{
		public bool isRight;

		void Awake()
		{
			float f = -90f;

			if(isRight)
				f = 90f;

			GetComponent<RectTransform>().localEulerAngles = Vector2.up * f;
		}

		void OnEnable()
		{
			EventManager.OnPlayerStartEvent += OnPlayerStartEvent;
			EventManager.OnAnimFailEvent += OnAnimFailEvent;
		}

		void OnDisable()
		{
			EventManager.OnPlayerStartEvent -= OnPlayerStartEvent;
			EventManager.OnAnimFailEvent -= OnAnimFailEvent;
		}

		void OnPlayerStartEvent()
		{
			EventManager.OnPlayerStartEvent -= OnPlayerStartEvent;
			#if AADOTWEEN
			GetComponent<RectTransform>().DOLocalRotate(Vector2.zero,1)
				.SetEase(Ease.OutBack);
			#endif
		}

		void OnAnimFailEvent (AnimFail g)
		{
			if(g == AnimFail.start)
			{
				EventManager.OnAnimFailEvent -= OnAnimFailEvent;

				float f = -90f;

				if(isRight)
					f = 90f;
				#if AADOTWEEN
				GetComponent<RectTransform>().DOLocalRotate(Vector2.up * f,1)
					.SetEase(Ease.OutBack);
				#endif
			}
		}
	}
}