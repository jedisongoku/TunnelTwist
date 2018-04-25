
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
	public class SpeedAddText : UIHelper 
	{
		public void OnEnable()
		{
			EventManager.OnPlayerStartEvent += OnPlayerStartEvent;
			EventManager.OnAnimFailEvent += AnimFailEvent;
			EventManager.OnSetAddSpeed += OnSetAddSpeed;
		}

		void OnDisable()
		{
			EventManager.OnPlayerStartEvent -= OnPlayerStartEvent;
			EventManager.OnAnimFailEvent -= AnimFailEvent;
			EventManager.OnSetAddSpeed -= OnSetAddSpeed;
		}

		void Start()
		{
			Reset();
		}

		public void OnSetAddSpeed(AddSpeed speed)
		{
			#if AADOTWEEN
			string sin = "+";

			if(speed.sign == -1)
				sin = "-";

			text.text = sin + speed.decrease.ToString ("F1");


			Reset();

			text.DOFade(1,0.05f)
				.OnComplete(()=>{
					text.rectTransform.DOScale(Vector2.one * 5,0.05f)
						.OnComplete(() => {
							text.rectTransform.DOScale(Vector2.one,0.3f).SetDelay(0.1f);
						});

					text.rectTransform.DOLocalMoveZ(-20,0.05f)
						.OnComplete(()=>{

							text.rectTransform.DOLocalMoveZ(0,0.3f)
								.SetDelay(0.1f)
								.OnComplete(()=>{
									text.DOFade(0,0.1f)
										.SetDelay(0.3f)
										.OnComplete(Reset);
								});
						});
				});
			#endif
		}

		void Reset()
		{
			#if AADOTWEEN
			text.DOKill();
			text.rectTransform.DOKill();
			text.rectTransform.localScale = Vector3.one;

			var r = text.rectTransform;
			var p = r.localPosition;
			p.z = 0;
			text.rectTransform.localPosition = p;

			Color c = text.color;
			c.a = 0;
			text.color = c;
			#endif
		}

		public void OnPlayerStartEvent()
		{
			EventManager.OnPlayerStartEvent -= OnPlayerStartEvent;

			ActivateText(true);

			Color c = text.color;
			c.a = 0;
			text.color = c;
		}

		public void AnimFailEvent(AnimFail g)
		{
			if(g == AnimFail.end)
			{
				EventManager.OnAnimFailEvent -= AnimFailEvent;
				ActivateText(false);
			}
		}
	}

	public class AddSpeed
	{
		public float decrease;
		public int sign;

		public AddSpeed(float decrease, int sign)
		{
			this.decrease = decrease;
			this.sign = sign;
		}
	}
}