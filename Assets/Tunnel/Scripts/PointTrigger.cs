
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
	public class PointTrigger : TriggerHelper 
	{
		public float defaultScale = 0.2f;
		public Vector3 myOriginalPos;
		public Transform pointMesh;
		public Transform shadowMesh;


		new void OnEnable()
		{
			base.OnEnable();
			shadowMesh.localScale = pointMesh.localScale;
			EventManager.OnPointScale += OnPointScale;
		}

		new void OnDisable()
		{
			base.OnDisable();
			EventManager.OnPointScale -= OnPointScale;
		}

		float scaleBeatPoint = 0;

		public override void CheckCustom()
		{
			scaleBeatPoint = backgroundAnim.scaleBeatPoint;

			float p = scale * 10;

			pointMesh.localScale = Vector3.Lerp(pointMesh.localScale,Vector3.one * (defaultScale + p + scaleBeatPoint*5), Time.time);

			shadowMesh.localScale = pointMesh.localScale;
		}

		float scale = 0;

		void OnPointScale(float scale)
		{
			this.scale = scale;

			//		float p = scale * 10;
			//
			//
			//
			////		pointMesh.localScale = Vector3.one * (defaultScale + p + scaleBeat);
			//		pointMesh.localScale = Vector3.one * (defaultScale + p + scaleBeatPoint*5);
			//		shadowMesh.localScale = pointMesh.localScale;
		}
        
		public override void OnCollisionWithPlayer()
		{
			player.DoExplosionParticle ();

			EventManager.DOAddOnePoint(gameObject);

			gameObject.SetActive (false);
		}

		//	public override void CheckCustom()
		//	{
		//		float p = slowMotionLogic.pointScaleAdd;
		//		pointMesh.localScale = Vector3.one * (defaultScale + p + scaleBeat);
		//		pointMesh.localScale = Vector3.one * (defaultScale + p + scaleBeat*5);
		//		shadowMesh.localScale = pointMesh.localScale;
		//	}

		public override void IsOutOfScreen()
		{
			EventManager.DODesactivatePointTrigger(gameObject);
		}
	}
}