
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
using UnityEngine.EventSystems;
using UnityEngine.Events;
#if AADOTWEEN
using DG.Tweening;
#endif


namespace AppAdvisory.TunnelAndTwist
{
	public class ButtonTransitionAnimation : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
	{
		public UnityEvent Onclicked;

		bool isClicked = false;

		float sizeDefault;

		float sizeUp
		{
			get
			{
				return sizeDefault * 1.2f;
			}
		}

		float sizeDown
		{
			get
			{
				return sizeDefault * 0.8f;
			}
		}

		void Awake()
		{
			sizeDefault = transform.localScale.x;
		}

		void OnEnable()
		{
			isClicked = false;
		}

		public void OnPointerClick (PointerEventData eventData)
		{
			#if AADOTWEEN
			if(isClicked)
				return;

			isClicked = true;

			transform.DOKill();

			transform.DOScale (Vector3.one * sizeDown, 0.1f).OnComplete(() => {
				transform.DOScale (Vector3.one * sizeDefault, 0.1f).OnComplete(() => {
					if(Onclicked != null)
						Onclicked.Invoke();

					isClicked = false;
				});
			});
			#endif
		}

		public void OnPointerEnter (PointerEventData eventData)
		{
			#if AADOTWEEN
			if(isClicked)
				return;

			transform.DOKill();
			transform.DOScale (Vector3.one * sizeUp, 0.3f);

			isClicked = false;
			#endif
		}

		public void OnPointerExit (PointerEventData eventData)
		{
			#if AADOTWEEN
			if(isClicked)
				return;

			transform.DOKill();
			transform.DOScale (Vector3.one * sizeDefault, 0.3f);

			isClicked = false;
			#endif
		}
	}
}