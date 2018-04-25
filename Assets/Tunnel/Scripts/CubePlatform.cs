
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
	public class CubePlatform : MonoBehaviorHelper 
	{
		public Vector3 localPositionDefault;

		Vector3 localPositionStart
		{
			get
			{
				Vector3 pos = localPositionDefault;
				pos.x = localPositionDefault.x * 100;
				pos.y = localPositionDefault.y * 100;

				return pos;
			}
		}

		[SerializeField] private GameObject m_itemPosition;

		[SerializeField] private Renderer m_renderer;

		[SerializeField] private Collider m_collider;

		void Awake()
		{
			localPositionDefault = transform.localPosition;

		}

		void OnDisable()
		{
			#if AADOTWEEN
			if(tweener != null)
				tweener.Kill(false);

			transform.DOKill (false);
			StopAllCoroutines ();
			#endif
		}

		void OnReloadScene()
		{
			#if AADOTWEEN
			if(tweener != null)
				tweener.Kill(false);

			transform.DOKill (false);
			StopAllCoroutines ();

			transform.localPosition = localPositionDefault;
			#endif
		}

		#if AADOTWEEN
		Tweener tweener;
		#endif

		GameObject point;

		public GameObject ActivatePoint()
		{

			point = objectPooling.Spawn("PointPrefab", m_itemPosition.transform.position, transform.rotation);

			point.SetActive (true);

			return point;
		}

		IEnumerator FollowParent(Transform t)
		{
			while (true) 
			{
				Vector3 lastPos = t.position;
				yield return 0;
				t.position = m_itemPosition.transform.position;
				if (lastPos == t.position)
					break;
			}
		}

		public void DesactivateRendererAndCollider()
		{
			point = null;

			m_renderer.enabled = false;

			m_collider.enabled = false;

			m_itemPosition.SetActive (false);

			#if AADOTWEEN
			transform.DOKill (false);
			#endif

			StopAllCoroutines ();
		}

		public void ActivatedRendererAndCollider()
		{
			point = null; 

			#if AADOTWEEN
			if(tweener != null)
				tweener.Kill(false);

			transform.DOKill (false);
			#endif

			StopAllCoroutines ();

			m_renderer.gameObject.SetActive (true);

			m_collider.gameObject.SetActive (true);

			m_renderer.enabled = true;

			m_collider.enabled = true;

			m_itemPosition.SetActive (true);

			float time = 2f;

			transform.localPosition = localPositionStart;
			#if AADOTWEEN
			tweener = transform.DOLocalMove (localPositionDefault, time).SetEase(Ease.Linear)
				.SetEase(Ease.OutQuart)
				.OnUpdate(() => {
					if(point != null)
					{
						point.transform.position = m_itemPosition.transform.position;
					}
				});
			#endif
		}

		public bool IsActive()
		{
			return m_renderer.enabled;
		}
	}
}