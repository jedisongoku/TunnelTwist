
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
using System.Collections.Generic;
#if AADOTWEEN
using DG.Tweening;
#endif


namespace AppAdvisory.TunnelAndTwist
{
	public class MenuManager : MonoBehaviorHelper 
	{
		[SerializeField] private List<CanvasGroup> canvasGroupStart;
		[SerializeField] private List<CanvasGroup> canvasGroupGameOver;

		void OnEnable()
		{
			EventManager.OnAnimIntroEvent += OnAnimIntroEvent;
			EventManager.OnPlayerStartEvent += OnPlayerStartEvent;
			EventManager.OnAnimFailEvent += OnAnimFailEvent;
		}

		void OnDisable()
		{
			EventManager.OnAnimIntroEvent -= OnAnimIntroEvent;
			EventManager.OnPlayerStartEvent -= OnPlayerStartEvent;
			EventManager.OnAnimFailEvent -= OnAnimFailEvent;
		}

		void Awake()
		{
			SceneJustStartedLogic();
		}

		void OnAnimIntroEvent(AnimIntro g)
		{
			if(g == AnimIntro.end)
			{
				EventManager.OnAnimIntroEvent -= OnAnimIntroEvent;
				IntroANimEndedLogic();
			}
		}

		void SceneJustStartedLogic()
		{
			canvasGroupStart.ActivateCanavsGroup(0,0,0);
			canvasGroupGameOver.ActivateCanavsGroup(0,0,0);
		}

		void IntroANimEndedLogic()
		{
			canvasGroupStart.ActivateCanavsGroup(0,1,1);
			canvasGroupGameOver.ActivateCanavsGroup(0,0,0);
		}

		void OnPlayerStartEvent()
		{
			EventManager.OnPlayerStartEvent -= OnPlayerStartEvent;
			canvasGroupStart.ActivateCanavsGroup(1,0,1);
			canvasGroupGameOver.ActivateCanavsGroup(0,0,0);
		}

		void OnAnimFailEvent(AnimFail g)
		{
			if(g == AnimFail.end)
			{
				EventManager.OnAnimFailEvent -= OnAnimFailEvent;
				canvasGroupStart.ActivateCanavsGroup(0,0,0);
				canvasGroupGameOver.ActivateCanavsGroup(0,1,1);
			}
		}
	}
}