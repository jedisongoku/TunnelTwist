
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


namespace AppAdvisory.TunnelAndTwist
{
	public class LastScoreText : UIHelper
	{
		int point = 0;

		new void Awake()
		{
			base.Awake();
			Init();
		}

		public void OnEnable()
		{
			EventManager.OnAnimFailEvent += OnAnimFailEvent;
			EventManager.OnSetPoint += OnSetPoint;

			Init();
		}

		void OnDisable()
		{
			EventManager.OnAnimFailEvent -= OnAnimFailEvent;
			EventManager.OnSetPoint -= OnSetPoint;
		}

		void Start () 
		{
			Init();
		}

		void Init()
		{
			ActivateText(true);

			text.text = "Last: " + Utils.lastScore.ToString();
		}

		public void OnSetPoint(int p)
		{
			point = p;
		}

		public void OnAnimFailEvent(AnimFail g)
		{
			if(g == AnimFail.end)
			{
				ActivateText(true);

				if(text)
					text.text = "Last: " + point.ToString();
			}
		}
	}
}