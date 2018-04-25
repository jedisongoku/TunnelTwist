
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
	public class TriggerHelper : MonoBehaviorHelper
	{
		public virtual void OnCollisionWithPlayer(){}

		public virtual void IsOutOfScreen(){}

		public virtual void CheckCustom(){}


		public Transform camTransform;

		void Awake()
		{
			camTransform = Camera.main.transform;
		}

		protected void OnEnable()
		{
			StartCoroutine(CoUpdate());
			//		EventManager.OnGameEvent += OnGameEvent;
		}

		protected void OnDisable()
		{
			StopAllCoroutines();
			//		EventManager.OnGameEvent -= OnGameEvent;
		}

		IEnumerator CoUpdate()
		{
			while(true)
			{
				CheckCustom ();
				Check ();
				yield return 0;
			}
		}

		void Check()
		{
			float dist = Vector3.Distance (transform.position, playerTransform.position);

			if (dist < 1) 
			{
				OnCollisionWithPlayer ();
			}

			if (IsBehind ()) 
			{
				IsOutOfScreen ();
			}
		}

		bool IsBehind()
		{
			Vector3 forward = transform.TransformDirection(Vector3.forward);
			Vector3 toOther = camTransform.position - transform.position;
			if (Vector3.Dot (forward, toOther) > 2*transform.localScale.z)
				return true;

			return false;
		}

		//
		//	void OnGameEvent (GameState g)
		//	{
		//		if(g == GameState.playerFailAnimationStart)
		//		{
		//			EventManager.OnGameEvent -= OnGameEvent;
		//			StopAllCoroutines();
		//		}
		//	}
	}
}