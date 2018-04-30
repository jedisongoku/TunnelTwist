
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
#if AADOTWEEN
using DG.Tweening;
#endif
#if APPADVISORY_ADS
using AppAdvisory.Ads;
#endif
#if APPADVISORY_LEADERBOARD
using AppAdvisory.social;
#endif

#if VS_SHARE
using AppAdvisory.SharingSystem;
#endif

namespace AppAdvisory.TunnelAndTwist
{
	/// <summary>
	/// Class in charge of the game logic. Attached to the gameObject GameManager in the hierarchy view
	/// </summary>
	public class GameManager : MonoBehaviorHelper
	{
		public int numberOfPlayToShowInterstitial = 5;

		public string VerySimpleAdsURL = "http://u3d.as/oWD";

		/// <summary>
		/// Point of the player. The player earn one point each time he gets a ItemPoint
		/// </summary>
		[SerializeField] private int m_point = 0;
		/// <summary>
		/// The current speed of the game
		/// </summary>
		[SerializeField] private float _pointSpeed  = 0;

		int bestScore = -1;
		/// <summary>
		/// Formula to get the speed
		/// </summary>
		public float pointSpeed
		{
			set 
			{
				float v = value;

				if (v < 0)
					v = 0;

				_pointSpeed = v;

				if(bestScore == -1)
					bestScore = Utils.bestScore;

				if (bestScore < point)
					Utils.bestScore = point;

				EventManager.DOSetSpeed(v);
			}
			get 
			{
				return _pointSpeed;
			}
		}
		/// <summary>
		/// To set the current player points
		/// </summary>
		private int point
		{
			set
			{
				m_point = value;

				EventManager.DOSetPoint(value);
			}
			get
			{
				return m_point;
			}
		}



        public int getScore()
        {
            return point;
        }

        /// <summary>
        /// Add 1 point to the player
        /// </summary>
        public int Add1Point()
		{
			point++;
			return point;
		}
		/// <summary>
		/// Add 1 speed to the player
		/// </summary>
		public void Add1Speed()
		{
			if(spawnCount <= 10)
				return;

			float ratio = 1.0f;

			if (Time.timeScale < 0.98f)
				ratio *= Time.timeScale;

			pointSpeed += 1f * ratio;
		}
		/// <summary>
		/// Remove 1 speed to the player
		/// </summary>
		public void Remove1Speed()
		{
			float ratio = 1;

			if (Time.timeScale < 0.98f)
				ratio = Time.timeScale / 1.3f;

			float decrease = 1f / ratio;
			pointSpeed -= decrease;

			EventManager.DOSetAddSpeed(new AddSpeed(decrease * (1 + FindObjectOfType<SlowMotionLogic>().comboCount), -1));
		}
		/// <summary>
		/// Count the number of spawn
		/// </summary>
		[NonSerialized] public int spawnCount = 0;


		public int numPlatformAtStart;

		public int numFirstFallablePlatform;


		public bool DEBUG = false;


		void OnFirstTap(TouchDirection td)
		{
			#if APPADVISORY_ADS
			AdsManager.instance.HideBanner();
			#endif

			#if VS_SHARE
			VSSHARE.DOHideScreenshotIcon();
			#endif

			InputTouch.OnTouched -= OnFirstTap;

			EventManager.DOSetSpeed(pointSpeed);
		}

		void Awake()
		{
			//		PlayerPrefs.DeleteAll();
			//		PlayerPrefs.Save();

			if(Time.realtimeSinceStartup < 3)
			{
				#if AADOTWEEN
				DOTween.Init();
				#endif
				#if APPADVISORY_LEADERBOARD
				LeaderboardManager.Init();
				#endif
			}
		}

		IEnumerator Start()
		{
			point = 0;

			Time.timeScale = 1;

			GC.Collect();

			Application.targetFrameRate = 60;

			Physics.gravity = Vector3.zero;

			var t = FindObjectOfType<PlatformParent>();

			lastScaleZ = t.transform.localScale.z;
			lastPosZ = t.transform.localPosition.z;

			for(int i = 0; i < numPlatformAtStart; i++)
			{
				yield return new WaitForSeconds(0.1f * i);

				SpawnPlatform();

			}

			#if APPADVISORY_ADS
			AdsManager.instance.ShowBanner();
			#endif
		}

		void OnEnable()
		{
			InputTouch.OnTouched += OnFirstTap;
			EventManager.OnDespawnPlatformEvent += OnDespawnPlatformEvent;
			EventManager.OnAddOnePoint += OnAddOnePoint;
			EventManager.OnDesactivatePointTrigger += OnDesactivatePointTrigger;
			EventManager.OnAnimFailEvent += OnAnimFailEvent;
		}

		void OnDisable()
		{
			InputTouch.OnTouched -= OnFirstTap;
			EventManager.OnDespawnPlatformEvent -= OnDespawnPlatformEvent;
			EventManager.OnAddOnePoint -= OnAddOnePoint;
			EventManager.OnDesactivatePointTrigger -= OnDesactivatePointTrigger;
			EventManager.OnAnimFailEvent -= OnAnimFailEvent;
		}

		public void OnAnimFailEvent(AnimFail g)
		{
			if (g == AnimFail.start) 
			{
				ShowAds ();
				ShowRateUs ();
			}

#if VS_SHARE
			if(g == AnimFail.start)
				VSSHARE.DOTakeScreenShot();

			if(g == AnimFail.end)
				VSSHARE.DOOpenScreenshotButton();
#endif

#if APPADVISORY_ADS
			AdsManager.instance.ShowBanner();
#endif



            //Send the scoreto the global leader board
#if UNITY_ANDROID
            GoogleManager.ReportScore(point);
#elif UNITY_IOS

#endif

            Utils.lastScore = point;

			if (Utils.bestScore < point)
				Utils.bestScore = point;
		}

		void OnAddOnePoint(GameObject pointTrigger)
		{
			pointTrigger.SetActive (false);
			ObjectPooling.Despawn(pointTrigger);
			Add1Point ();
			Remove1Speed ();
		}

		void OnDesactivatePointTrigger(GameObject pointTrigger)
		{
			pointTrigger.SetActive (false);
			ObjectPooling.Despawn(pointTrigger);
		}


		void OnDespawnPlatformEvent()
		{
			Add1Speed();
			SpawnPlatform();
		}

		Obstacle currentObstacle = null;
		Obstacle lastObstacle = null;


		float lastPosZ = -1;
		float lastScaleZ = -1;

		public Transform SpawnPlatform()
		{
			var o = objectPooling.Spawn("PlatformParentPrefab");

			o.SetActive (true);

			Transform t = o.transform;

			t.position = new Vector3 (0, 0, lastPosZ + lastScaleZ/2f + t.localScale.z/2f);

			lastPosZ = t.localPosition.z;
			lastScaleZ = t.localScale.z;

			var temp = currentObstacle;

			currentObstacle = new Obstacle (lastObstacle, spawnCount);

			lastObstacle = temp;

			t.GetComponent<PlatformParent> ().Set (currentObstacle);

			spawnCount++;



			return t;
		}

		public void SpawnPlatformFromDestroy(GameObject toDesactivate)
		{
			SpawnPlatform ();

			ObjectPooling.Despawn(toDesactivate);

			Add1Speed();
		}

		void OnDestroy()
		{
#if AADOTWEEN
			DOTween.KillAll();
#endif
			PlayerPrefs.Save();
		}

		public void ShowRateUs()
		{
			int count = PlayerPrefs.GetInt("GAMEOVER_COUNT_RATEUS",0);
			count++;
			PlayerPrefs.SetInt("GAMEOVER_COUNT_RATEUS",count);
			PlayerPrefs.Save();

			if (count > 30) 
			{
#if VSRATE
				AppAdvisory.VSRATE.RateUsManager.ShowRateUsWindows ();
#endif
			}
		}
		public void ShowAds()
		{
			int count = PlayerPrefs.GetInt("GAMEOVER_COUNT",0);
			count++;
			PlayerPrefs.SetInt("GAMEOVER_COUNT",count);
			PlayerPrefs.Save();

#if APPADVISORY_ADS
			if(count > numberOfPlayToShowInterstitial)
			{
#if UNITY_EDITOR
				print("count = " + count + " > numberOfPlayToShowINterstitial = " + numberOfPlayToShowInterstitial);
#endif
				if(AdsManager.instance.IsReadyInterstitial() || AdsManager.instance.IsReadyVideoAds())
				{
#if UNITY_EDITOR
					print("AdsManager.instance.IsReadyInterstitial() == true ----> SO ====> set count = 0 AND show interstial");
#endif
					if(UnityEngine.Random.Range(0,100) < 50)
					{
						if(AdsManager.instance.IsReadyVideoAds())
						{
							PlayerPrefs.SetInt("GAMEOVER_COUNT",0);
							AdsManager.instance.ShowVideoAds();
						}
						else
						{
							if(AdsManager.instance.IsReadyInterstitial())
							{
								PlayerPrefs.SetInt("GAMEOVER_COUNT",0);
								AdsManager.instance.ShowInterstitial();
							}
						}	
					}
					else
					{
						if(AdsManager.instance.IsReadyInterstitial())
						{
							PlayerPrefs.SetInt("GAMEOVER_COUNT",0);
							AdsManager.instance.ShowInterstitial();
						}
						else
						{
							if(AdsManager.instance.IsReadyVideoAds())
							{
								PlayerPrefs.SetInt("GAMEOVER_COUNT",0);
								AdsManager.instance.ShowVideoAds();
							}
						}
					
					}
				}
				else
				{
#if UNITY_EDITOR
					print("AdsManager.instance.IsReadyInterstitial() == false");
#endif
				}

			}
			else
			{
				PlayerPrefs.SetInt("GAMEOVER_COUNT", count);
			}
			PlayerPrefs.Save();
#else
			if(count >= numberOfPlayToShowInterstitial)
			{
			Debug.LogWarning("To show ads, please have a look to Very Simple Ad on the Asset Store, or go to this link: " + VerySimpleAdsURL);
			Debug.LogWarning("Very Simple Ad is already implemented in this asset");
			Debug.LogWarning("Just import the package and you are ready to use it and monetize your game!");
			Debug.LogWarning("Very Simple Ad : " + VerySimpleAdsURL);
			PlayerPrefs.SetInt("GAMEOVER_COUNT",0);
			}
			else
			{
			PlayerPrefs.SetInt("GAMEOVER_COUNT", count);
			}
			PlayerPrefs.Save();
#endif
		}
	}
}