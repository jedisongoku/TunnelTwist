
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
	public class MoveForward : MonoBehaviorHelper 
	{
		float pointSpeed = 0;

		void OnEnable()
		{
			EventManager.OnSetSpeed += OnSetSpeed;
			EventManager.OnPlayerStartEvent += OnPlayerStartEvent;
			EventManager.OnAnimFailEvent += OnAnimFailEvent;
		}

		void OnDisable()
		{
			EventManager.OnSetSpeed -= OnSetSpeed;
			EventManager.OnPlayerStartEvent -= OnPlayerStartEvent;
			EventManager.OnAnimFailEvent -= OnAnimFailEvent;
		}

		void OnPlayerStartEvent()
		{
			EventManager.OnPlayerStartEvent -= OnPlayerStartEvent;

			StartCoroutine("CoUpdate");
		}

		void OnAnimFailEvent(AnimFail g)
		{
			if(g == AnimFail.start)
			{
				StopCoroutine("CoUpdate");
				EventManager.OnSetSpeed -= OnSetSpeed;
				EventManager.OnAnimFailEvent -= OnAnimFailEvent;
			}
		}

		void OnSetSpeed(float speed)
		{
			pointSpeed = speed;
		}

		IEnumerator CoUpdate()
		{
			while(true)
			{

				transform.Translate (Vector3.forward * Utils.GetMovePlayerSpeed(pointSpeed) * Time.deltaTime);
				yield return 0;
			}
		}
	}
}