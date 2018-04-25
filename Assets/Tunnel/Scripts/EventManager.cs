
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using System.Collections;
using System;
#if UNITY_5_3
using UnityEngine.SceneManagement;
#endif
#if AADOTWEEN
using DG.Tweening;
#endif


namespace AppAdvisory.TunnelAndTwist
{
	public class EventManager : MonoBehaviour 
	{

		public delegate void AnimIntroEvent(AnimIntro g);
		public static event AnimIntroEvent OnAnimIntroEvent;
		public static void DOAnimIntroEvent(AnimIntro g)
		{
			if(OnAnimIntroEvent!=null)
				OnAnimIntroEvent(g);	
		}

		public delegate void AnimFailEvent(AnimFail g);
		public static event AnimFailEvent OnAnimFailEvent;
		public static void DOAnimFailEvent(AnimFail g)
		{
			if(OnAnimFailEvent!=null)
				OnAnimFailEvent(g);	
		}


		public delegate void PlayerStartEvent();
		public static event PlayerStartEvent OnPlayerStartEvent;
		public static void DOPlayerStartEvent()
		{
			if(OnPlayerStartEvent!=null)
				OnPlayerStartEvent();	
		}

		public delegate void PlayerJumpEvent();
		public static event PlayerJumpEvent OnPlayerJumpEvent;
		public static void DOPlayerJumpEvent()
		{
			if(OnPlayerJumpEvent!=null)
				OnPlayerJumpEvent();	
		}

		public delegate void GetPointEvent();
		public static event GetPointEvent OnGetPointEvent;
		public static void DOGetPointEvent()
		{
			if(OnGetPointEvent!=null)
				OnGetPointEvent();	
		}

		public delegate void ReloadSceneEvent();
		public static event ReloadSceneEvent OnReloadSceneEvent;
		public static void DOReloadSceneEvent()
		{
			if(OnReloadSceneEvent!=null)
				OnReloadSceneEvent();	
			#if AADOTWEEN
			DOVirtual.DelayedCall(0.1f,() => {
				#if UNITY_5_3
				DOTween.KillAll();
				GC.Collect();
				Resources.UnloadUnusedAssets();
				SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name,LoadSceneMode.Single);
				Resources.UnloadUnusedAssets();
				DOTween.KillAll();
				GC.Collect();
				#else
				Application.LoadLevel (Application.loadedLevel);
				#endif
			});
			#endif
		}

		public delegate void DespawnPlatformEvent();
		public static event DespawnPlatformEvent OnDespawnPlatformEvent;
		public static void DODespawnPlatformEvent()
		{
			if(OnDespawnPlatformEvent!=null)
				OnDespawnPlatformEvent();
		}

		public delegate void OnCurve(TCurve c);
		public static event OnCurve OnChangeCurve;
		public static void DOCurve(TCurve c)
		{
			if(OnChangeCurve!=null)
				OnChangeCurve(c);	
		}

		public delegate void PlatformColor(Color c);
		public static event PlatformColor OnChangePlatformColor;
		public static void DOPlatformColor(Color c)
		{
			if(OnChangePlatformColor!=null)
				OnChangePlatformColor(c);	
		}

		public delegate void PointColor(Color c);
		public static event PointColor OnChangePointColor;
		public static void DOPointColor(Color c)
		{
			if(OnChangePointColor!=null)
				OnChangePointColor(c);	
		}

		public delegate void PointScale(float scale);
		public static event PointScale OnPointScale;
		public static void DOPointScale(float scale)
		{
			if(OnPointScale!=null)
				OnPointScale(scale);	
		}

		public delegate void MotionBlur(float f, bool enabled);
		public static event MotionBlur OnMotionBlur;
		public static void DOMotionBlur(float f, bool enabled)
		{
			if(OnMotionBlur!=null)
				OnMotionBlur(f, enabled);	
		}

		public delegate void SetSpeed(float speed);
		public static event SetSpeed OnSetSpeed;
		public static void DOSetSpeed(float speed)
		{
			if(OnSetSpeed!=null)
				OnSetSpeed(speed);	
		}

		public delegate void SetAddSpeed(AddSpeed addSpeed);
		public static event SetAddSpeed OnSetAddSpeed;
		public static void DOSetAddSpeed(AddSpeed addSpeed)
		{
			if(OnSetAddSpeed!=null)
				OnSetAddSpeed(addSpeed);	
		}


		public delegate void SetPoint(int point);
		public static event SetPoint OnSetPoint;
		public static void DOSetPoint(int point)
		{
			if(OnSetPoint!=null)
				OnSetPoint(point);	
		}

		public delegate void AddOnePoint(GameObject pointTrigger);
		public static event AddOnePoint OnAddOnePoint;
		public static void DOAddOnePoint(GameObject pointTrigger)
		{
			if(OnAddOnePoint!=null)
				OnAddOnePoint(pointTrigger);	
		}

		public delegate void DesactivatePointTrigger(GameObject pointTrigger);
		public static event DesactivatePointTrigger OnDesactivatePointTrigger;
		public static void DODesactivatePointTrigger(GameObject pointTrigger)
		{
			if(OnDesactivatePointTrigger!=null)
				OnDesactivatePointTrigger(pointTrigger);	
		}

		public delegate void SetComboCount(int comboCount);
		public static event SetComboCount OnSetComboCount;
		public static void DOSetComboCount(int comboCount)
		{
			if(OnSetComboCount!=null)
				OnSetComboCount(comboCount);	
		}
	}

	public enum AnimIntro
	{
		start,
		end
	}

	public enum AnimFail
	{
		start,
		end
	}

	public enum TempoType
	{
		none = 0,
		bass = 1,
		mid = 2,
		high = 4
	}
}