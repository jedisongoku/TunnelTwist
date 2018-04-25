
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
	public class UIHelper : MonoBehaviour 
	{
		public Text text;

		public void Awake()
		{
			if(text == null)
				text = GetComponent<Text>();
		}

		//	public void OnEnable()
		//	{
		//		EventManager.OnSetPoint += OnSetPoint;
		//		EventManager.OnSetSpeed += OnSetSpeed;
		//		EventManager.OnSetAddSpeed += OnSetAddSpeed;
		//		EventManager.OnGameEvent += OnGameEvent;
		//		InputTouch.OnTouched += OnTouched;
		//	}
		//
		//	void OnTouched (TouchDirection td)
		//	{
		//		InputTouch.OnTouched -= OnTouched;
		//		OnFirstTap();
		//	}
		//
		//	public void OnDisable()
		//	{
		//		EventManager.OnSetPoint -= OnSetPoint;
		//		EventManager.OnSetSpeed -= OnSetSpeed;
		//		EventManager.OnSetAddSpeed -= OnSetAddSpeed;
		//		EventManager.OnGameEvent -= OnGameEvent;
		//		InputTouch.OnTouched -= OnTouched;
		//	}
		//
		public void ActivateText(bool activate)
		{
			if(text)
				text.gameObject.SetActive(activate);
		}

		//	public virtual void OnFirstTap(){}
		//
		//	public virtual void OnSetPoint(int point){}
		//
		//	public virtual void OnSetSpeed(float speed){}
		//
		//	public virtual void OnSetAddSpeed(AddSpeed addSpedd){}
		//
		//	public virtual void OnGameEvent(GameState gameState){}
	}
}