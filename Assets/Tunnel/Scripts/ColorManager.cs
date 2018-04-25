
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if AADOTWEEN
using DG.Tweening;
#endif

namespace AppAdvisory.TunnelAndTwist
{
	public class ColorManager : MonoBehaviorHelper 
	{
		public Color defaultColor;

		public List<Material> materialColorChange;

		Color _currentPlatformColor;
		Color currentPlatformColor
		{
			get
			{
				return _currentPlatformColor;
			}

			set
			{
				_currentPlatformColor = value;
				EventManager.DOPlatformColor(value);

				foreach(var m in materialColorChange)
				{
					m.color = value;
				}
			}
		}

		void OnEnable()
		{
			EventManager.OnPlayerStartEvent += OnPlayerStartEvent;
		}

		void OnDisable()
		{
			EventManager.OnPlayerStartEvent -= OnPlayerStartEvent;
			PlayerPrefs.Save ();
		}

		void OnPlayerStartEvent()
		{
			EventManager.OnPlayerStartEvent -= OnPlayerStartEvent;
			ChangeColor();
		}

		void Start ()
		{

			if (!PlayerPrefs.HasKey ("LAST_COLOR_USED")) 
			{
				currentPlatformColor = defaultColor;

				PlayerPrefsX.SetColor ("LAST_COLOR_USED", defaultColor);
			}
			else 
			{
				Color c = PlayerPrefsX.GetColor ("LAST_COLOR_USED");
				currentPlatformColor = c;
			}
		}

		public void ChangeColor()
		{
			Color colorTemp = Utils.GetRandomColor ();
			#if AADOTWEEN
			DOTween.To(()=> currentPlatformColor, x => currentPlatformColor = x, colorTemp, 3f)
				.SetDelay(Utils.RandomRange(3f,20f))
				.SetUpdate(true)
				.OnComplete(() => {
					PlayerPrefsX.SetColor ("LAST_COLOR_USED", currentPlatformColor);

					ChangeColor();
				});
			#endif
		}
	}
}