
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
#if AADOTWEEN
using DG.Tweening;
#endif

namespace AppAdvisory.TunnelAndTwist
{
	public class CameraLogic : MonoBehaviorHelper 
	{
		public bool doAnimationStart = true;

		public Canvas canvasBackground;

		public Vector3 originalPosition1;
		public Vector3 originalRotation1;

		public float defaultVelocityScale = 0.375f; 

		public bool AnimationIsEnded;

		void Awake()
		{
			AnimationIsEnded = false;
		}

		void OnEnable()
		{
			InputTouch.OnTouched += OnTouched;
		}

		void OnDisable()
		{
			InputTouch.OnTouched -= OnTouched;

			#if AADOTWEEN
			DOTween.KillAll (false);
			#endif
		}

		void OnTouched (TouchDirection td)
		{
			InputTouch.OnTouched -= OnTouched;
			Time.timeScale = 100;
		}

		IEnumerator Start()
		{
			yield return 0;

			SetAnimationIsStarted();

			Vector3[] waypoints = new[] { new Vector3(26.89613f,-6.749004f,94.42464f), new Vector3(-21.26232f,-39.58519f,1.87349f), transform.position };

			#if AADOTWEEN
			transform.DOMove(waypoints[0],3f)
				.SetEase(Ease.InQuad)
				.OnUpdate(DOLookAt)
				.OnComplete(() => {
					transform.DOMove(waypoints[1],1f)
						.SetEase(Ease.Linear)
						.OnUpdate(DOLookAt)
						.OnComplete(() => {
							transform.DOMove(waypoints[2],0.5f)
								.OnUpdate(DOLookAt)
								.SetEase(Ease.OutQuad)
								.OnComplete(() => {
									DOLookAt();
									SetAnimationIsEnded();
								});

						});

				});
			#endif
		}

		void DOLookAt()
		{

			Vector3 v = new Vector3(0,0,-0.96f);
			//		transform.LookAt(Vector3.zero);
			transform.LookAt(v);
		}

		public void SetAnimationIsStarted()
		{
			AnimationIsEnded = false;

			EventManager.DOAnimIntroEvent(AnimIntro.start);
		}

		public void SetAnimationIsEnded()
		{

			Time.timeScale = 1;

			InputTouch.OnTouched -= OnTouched;

			AnimationIsEnded = true;

			transform.localPosition = originalPosition1;

			transform.eulerAngles = originalRotation1;

			EventManager.DOAnimIntroEvent(AnimIntro.end);

			FindObjectOfType<InputTouch>().enabled = true;
		}
	}
}