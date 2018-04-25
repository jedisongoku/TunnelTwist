
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#if AADOTWEEN
using DG.Tweening;
#endif


namespace AppAdvisory.TunnelAndTwist
{
	public class SpeedText : UIHelper 
	{
		float newSpeed;

		void Start()
		{
			OnSetSpeed(0);
		}

		public void OnEnable()
		{
			EventManager.OnPlayerStartEvent += OnPlayerStartEvent;
			EventManager.OnSetSpeed += OnSetSpeed;
		}

		void OnDisable()
		{
			EventManager.OnPlayerStartEvent -= OnPlayerStartEvent;
			EventManager.OnSetSpeed -= OnSetSpeed;
		}

		public void OnSetSpeed(float speed)
		{
			#if AADOTWEEN
			DOVirtual.Float(newSpeed, speed, 0.3f, SetSpeedText);
			#endif
			newSpeed = speed;
		}

		void SetSpeedText(float speed)
		{
			text.text = speed.ToString("F1");
		}


		public void OnPlayerStartEvent()
		{
			EventManager.OnPlayerStartEvent -= OnPlayerStartEvent;
			OnSetSpeed(0);
			ActivateText(true);
		}		
	}
}