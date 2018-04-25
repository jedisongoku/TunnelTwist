
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
	public class CurvedManager : MonoBehaviorHelper 
	{
		float _x;
		public float x
		{
			get
			{
				return _x;
			}

			set
			{
				_x = value;
				SetCurve(value,y);
			}
		}

		float _y;
		public float y
		{
			get
			{
				return _y;
			}

			set
			{
				_y = value;
				SetCurve(x,value);
			}
		}

		void SetCurve(float x_, float y_)
		{
			TCurve c = new TCurve(x_,y_);
			EventManager.DOCurve(c);
		}

		float rangeMin = -500;
		float rangeMax = 500;


		float durationmin = 5f;
		float durationmax = 10f;

		float waitmin = 2f;
		float waitmax = 10f;

		#if AADOTWEEN
		Tweener x1;
		Tweener y1;


		void Awake()
		{
			Reset();
		}

		void OnEnable()
		{
			DOTween.Kill(x);
			DOTween.Kill(y);
			x = 0;
			y = 0;
			EventManager.OnPlayerStartEvent += OnPlayerStartEvent;
			EventManager.OnAnimFailEvent += OnAnimFailEvent;
		}

		void OnDisable()
		{
			DOTween.Kill(x);
			DOTween.Kill(y);
			x = 0;
			y = 0;
			EventManager.OnPlayerStartEvent -= OnPlayerStartEvent;
			EventManager.OnAnimFailEvent -= OnAnimFailEvent;
		}

		void Reset()
		{
			DOTween.Kill(x);
			DOTween.Kill(y);

			x = 0;
			y = 0;
		}

		void OnDestroy()
		{
			DOTween.Kill(x);
			DOTween.Kill(y);
			x = 0;
			y = 0;
		}

		void OnPlayerStartEvent()
		{
			EventManager.OnPlayerStartEvent -= OnPlayerStartEvent;

			StopAllCoroutines();

			Reset();

			StartCurveAnim();
		}

		void OnAnimFailEvent(AnimFail g)
		{
			if(g == AnimFail.start)
			{
				EventManager.OnAnimFailEvent -= OnAnimFailEvent;

				DOTween.Kill(x,false);
				DOTween.Kill(y,false);
			}
		}

		IEnumerator Start()
		{
			Reset();

			x = 0;
			y = 0; 

			while(true)
			{
				Reset();

				x = 0;
				y = 0; 

				yield return new WaitForSeconds (0.1f);
			}
		}

		void StartCurveAnim()
		{
			ChangeCurveX();

			ChangeCurveY();
		}
		#endif

		float duration
		{
			get
			{

				return Utils.RandomRange (durationmin, durationmax);
			}
		}


		float wait
		{
			get
			{
				return Utils.RandomRange (waitmin, waitmax);
			}
		}

		float range
		{
			get
			{
				return Utils.RandomRange (rangeMin, rangeMax);
			}
		}

		#if AADOTWEEN
		void ChangeCurveX()
		{
			if (x1 != null)
				x1.Kill(false);

			float _range = range;
			float _duration = duration * Mathf.Abs(_range - x) / (rangeMax - rangeMin); 

			x1 = DOTween.To(()=> x, f => x = f, _range, _duration)
				.SetUpdate(true)
				.SetDelay(wait)
				.OnComplete(ChangeCurveX);


		}



		void ChangeCurveY()
		{
			if (y1 != null)
				y1.Kill(false);

			float _range = range;
			float _duration = duration * Mathf.Abs(_range - y) / (rangeMax - rangeMin); 


			y1 = DOTween.To(()=> y, f => y = f, _range, _duration)
				.SetUpdate(true)
				.SetDelay(wait)
				.OnComplete(ChangeCurveY);

		}
		#endif
	}
}