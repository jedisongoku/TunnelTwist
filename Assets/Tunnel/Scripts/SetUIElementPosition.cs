
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using System.Collections;


namespace AppAdvisory.TunnelAndTwist
{
	public class SetUIElementPosition : MonoBehaviour 
	{
		public UIPosition position;

		public Camera UICam;

		public RectTransform rectTransform;

		void Awake()
		{
			UICam = GetComponentInParent<Camera>();

			rectTransform = GetComponent<RectTransform>();
		}

		void Start () 
		{
			if(position == UIPosition.topLeft)
			{
				Vector3 p = UICam.ViewportToWorldPoint(new Vector3(0f, 1f, transform.position.z));
				transform.position = p + new Vector3(+0.4f,-0.8f,0);
			}

			if(position == UIPosition.topRight)
			{
				Vector3 p = UICam.ViewportToWorldPoint(new Vector3(1f, 1f, transform.position.z));
				transform.position = p + new Vector3(-0.4f,-0.8f,0);
			}
		}


		void DOIt()
		{
			Vector2 pos = gameObject.transform.position;  // get the game object position
			Vector2 viewportPoint = Camera.main.WorldToViewportPoint(pos);  //convert game object position to VievportPoint

			// set MIN and MAX Anchor values(positions) to the same position (ViewportPoint)
			rectTransform.anchorMin = viewportPoint;  
			rectTransform.anchorMax = viewportPoint; 
		}

		//	void OnDrawGizmosSelected() {
		//		Camera camera = GetComponentInParent<Camera>();
		//		Vector3 p = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
		//		Gizmos.color = Color.yellow;
		//		Gizmos.DrawSphere(p, 0.1F);
		//	}
	}

	public enum UIPosition
	{
		topLeft,
		topRight
	}
}