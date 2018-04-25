
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace AppAdvisory.TunnelAndTwist
{
	public class BestScoreText : UIHelper
	{
		new void Awake()
		{
			base.Awake();
			Init();
		}

		void OnEnable()
		{
			EventManager.OnAnimFailEvent += OnAnimFailEvent;

			Init();
		}

		void OnDisable()
		{
			EventManager.OnAnimFailEvent -= OnAnimFailEvent;
		}

		void Start () 
		{
			Init();
		}

		void Init()
		{
			ActivateText(true);

			text.text = "Best: " + Utils.bestScore.ToString();

			EventManager.OnAnimFailEvent -= OnAnimFailEvent;
		}

		public void OnAnimFailEvent(AnimFail g)
		{
			if(g == AnimFail.end)
			{
				EventManager.OnAnimFailEvent -= OnAnimFailEvent;

				ActivateText(true);

				if(text)
					text.text = "Best: " + Utils.bestScore.ToString();
			}

		}
	}
}