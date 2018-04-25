
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


namespace AppAdvisory.TunnelAndTwist
{
	public class PlatformParent : MonoBehaviorHelper 
	{
		public Obstacle obstacle;

		public CubePlatform[] CubePlatforms;

		void Awake()
		{
			CubePlatforms = GetComponentsInChildren<CubePlatform>(true);
		}

		void DoDestroy()
		{
			EventManager.DODespawnPlatformEvent();
			gameObject.Despawn();
		}

		void Update()
		{
			if (IsBehind ())
			{
				DoDestroy ();

			}
		}

		bool IsBehind()
		{
			Vector3 forward = transform.TransformDirection(Vector3.forward);
			Vector3 toOther = playerTransform.position - transform.position;
			if (Vector3.Dot (forward, toOther) > 2f*transform.localScale.z - 1)
				return true;

			return false;
		}

		List<CubePlatform> cubeActiveAroundPlayer = new List<CubePlatform> ();

		void OnEnable()
		{
			transform.localScale = new Vector3 (1, 1, Utils.GetZScaleCube(gameManager.pointSpeed));
		}

		void OnDisable()
		{
			StopAllCoroutines();
		}

		public void Set(Obstacle obstacle)
		{
			if(player == null)
				return;

			this.obstacle = obstacle;

			for (int i = 0; i < obstacle.GetSize (); i++) 
			{
				if(!obstacle.IsActivate(i))
					CubePlatforms [i].DesactivateRendererAndCollider ();
				else
					CubePlatforms [i].ActivatedRendererAndCollider ();
			}

			cubeActiveAroundPlayer = new List<CubePlatform> ();

			int playerPos = player.GetPlayerPosition ();

			int cubeLeft = NormalizePosition(playerPos - 1);
			int cubeFont = NormalizePosition(playerPos);
			int cubeRight = NormalizePosition(playerPos + 1);

			if (gameManager.spawnCount > 10) 
			{
				if(gameManager.pointSpeed < 30)
				{
					AddToListCubeActive (CubePlatforms [cubeLeft]);
					AddToListCubeActive (CubePlatforms [cubeRight]);
					DoActivePoint();
					return;
				}

				if(gameManager.pointSpeed < 100)
				{
					AddToListCubeActive (CubePlatforms [cubeFont]);
					AddToListCubeActive (CubePlatforms [cubeLeft]);
					AddToListCubeActive (CubePlatforms [cubeRight]);
					DoActivePoint();
					return;
				}

				AddToListCubeActive (CubePlatforms [cubeFont]);
				AddToListCubeActive (CubePlatforms [cubeFont]);
				AddToListCubeActive (CubePlatforms [cubeLeft]);
				AddToListCubeActive (CubePlatforms [cubeRight]);
				DoActivePoint();
				return;
			}

		}


		void DoActivePoint()
		{
			Utils.Shuffle (cubeActiveAroundPlayer);
			int rand = ((int)gameManager.pointSpeed)/30;

			if(Utils.RandomRange(0, 2 + rand) != 0)
			{
				if (cubeActiveAroundPlayer.Count > 0)
					cubeActiveAroundPlayer [0].ActivatePoint ();

				if (gameManager.pointSpeed > 100 && Utils.RandomRange(0, 4) != 0 && cubeActiveAroundPlayer.Count > 1)
					cubeActiveAroundPlayer [1].ActivatePoint ();
			}
		}

		void AddToListCubeActive(CubePlatform o)
		{
			if(o.IsActive())
				cubeActiveAroundPlayer.Add (o);
		}


		public int NormalizePosition(int pos)
		{
			if (pos >= 8)
				pos = pos - 8;
			else if (pos <= -1)
				pos = 8 + pos;

			return pos;
		}
	}
}