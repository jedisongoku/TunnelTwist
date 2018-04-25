
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
	public class ScoreText : UIHelper 
	{
		int point = 0;

		Vector3 defaultPosition;

		new void Awake()
		{
			base.Awake();
			defaultPosition = text.rectTransform.localPosition;
		}

		void Start()
		{
			OnSetPoint(0);
			ActivateText(true);
		}

		public void OnEnable()
		{
			EventManager.OnPlayerStartEvent += OnPlayerStartEvent;
			EventManager.OnSetPoint += OnSetPoint;
		}

		void OnDisable()
		{
			EventManager.OnPlayerStartEvent -= OnPlayerStartEvent;
			EventManager.OnSetPoint -= OnSetPoint;
		}

		public void OnSetPoint(int p)
		{
			point = p;
			text.text = p.ToString ();

			if(point == 0)
				return;
			#if AADOTWEEN
			text.rectTransform.DOShakePosition(0.2f,Vector3.back*10,100,90).OnComplete(() => {
				text.rectTransform.DOLocalMove(defaultPosition,0.1f);
			});
			#endif
		}

		public void OnPlayerStartEvent()
		{
			ActivateText(true);
		}
	}
}