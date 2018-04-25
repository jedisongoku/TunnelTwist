
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
	public class AnimateSphereTexture : MonoBehaviorHelper 
	{
		public Material m_material;

		float textureSphereSpeed = 0;

		void OnEnable()
		{
			EventManager.OnSetSpeed += TextureSphereSpeed;
			EventManager.OnPlayerStartEvent += OnPlayerStartEvent;
		}

		void OnDisable()
		{
			EventManager.OnSetSpeed -= TextureSphereSpeed;
			EventManager.OnPlayerStartEvent -= OnPlayerStartEvent;
		}

		void OnPlayerStartEvent()
		{
			EventManager.OnPlayerStartEvent -= OnPlayerStartEvent;
			AnimTexture();
		}

		public void TextureSphereSpeed(float pointSpeed)
		{

			float time = 1f - (pointSpeed/200f);

			if (time < 0.3f)
				time = 0.3f;

			textureSphereSpeed = time;
		}

		public void AnimTexture()
		{
			m_material.SetTextureOffset("_MainTex", Vector2.zero);

			#if AADOTWEEN
			m_material.DOOffset(new Vector2(1,0),"_MainTex", textureSphereSpeed * 2f)
				.SetEase(Ease.Linear)
				.OnComplete (AnimTexture);
			#endif
		}
	}
}