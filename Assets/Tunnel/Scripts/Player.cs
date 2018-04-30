
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using System.Collections;
using System;
#if AADOTWEEN
using DG.Tweening;
#endif


namespace AppAdvisory.TunnelAndTwist
{
	public class Player : MonoBehaviorHelper 
	{
		[SerializeField] private float currentDegree = 0;

		[SerializeField] private Transform groundCheck;

		[SerializeField] private Transform shadow;
		[SerializeField] private Vector3 shadowLocalPosition;
		[SerializeField] private Vector3 shadowLocalScale;
        [SerializeField] private GameManager gameManager;
        private int deathCounter;

		public bool m_isJumping;
		public bool isJumping
		{
			set
			{
				m_isJumping = value;
			}
			get 
			{

				return m_isJumping 
					#if AADOTWEEN
					|| DOTween.IsTweening(transform) || DOTween.IsTweening(transform.parent)
					#endif
					;
			}
		}

		bool isGameOver;

		bool isStarted;

		[SerializeField] private float timeSinceGameStarted;


		private Vector3 originalPosition = new Vector3 (0, -3.13f, 4.371f);

		[SerializeField] private ParticleSystem particleExplosion;


		void Awake()
		{
			transform.localPosition = originalPosition;

			currentDegree = 0;
			isJumping = false;
			isGameOver = true;
			isStarted = false;

            if (!PlayerPrefs.HasKey("DeathCounter"))
                PlayerPrefs.SetInt("DeathCounter", 0);

            shadowLocalPosition = shadow.localPosition;
			shadowLocalScale = shadow.localScale;

			shadow.gameObject.SetActive (false);
			#if AADOTWEEN
			DOVirtual.DelayedCall (1, () => {
				shadow.gameObject.SetActive (true);
			});
			#endif
		}

		void OnEnable()
		{
			EventManager.OnAnimIntroEvent += OnAnimIntroEvent;

		}

		void OnDisable()
		{
			InputTouch.OnTouched -= OnTouched;
			EventManager.OnAnimIntroEvent -= OnAnimIntroEvent;
		}

		void OnAnimIntroEvent(AnimIntro g)
		{
			if(g == AnimIntro.end)
			{
				InputTouch.OnTouched += OnTouched;

				EventManager.OnAnimIntroEvent -= OnAnimIntroEvent;
			}
		}

		void OnTouched(TouchDirection td)
		{

			if(isStarted == false)
			{
				isGameOver = false;
				EventManager.DOPlayerStartEvent();
			}

			isStarted = true;

			switch(td)
			{
			case TouchDirection.none :
				direction = 0;
				break;
			case TouchDirection.left :
				direction = -1;
				break;
			case TouchDirection.right :
				direction = +1;
				break;

			}
		}


		int direction = 0;



		void Update()
		{

			shadow.localPosition = shadowLocalPosition;

			if (isGrounded ()) 
			{
				if(isStarted)
					shadow.gameObject.SetActive (!isGameOver);
			}
			else 
			{
				if (!isJumping)
				{
					if(isStarted)
						shadow.gameObject.SetActive (false);
				}
				else 
				{
					if(isStarted)
						shadow.gameObject.SetActive (!isGameOver);
				}

				if (!isJumping && !isGameOver)
					AnimationGameOver ();
			}


			if (!isStarted) {
				timeSinceGameStarted = Time.realtimeSinceStartup;
				return;
			}

			if (Time.realtimeSinceStartup - timeSinceGameStarted < 1)
				return;


			if (isGameOver)
				return;




			if (direction == -1) {
				MoveLeft ();
			} else if (direction == +1) {
				MoveRight ();
			} else {
			}

		}

		public void AnimationGameOver()
		{
			if (isGameOver)
				return;


			isGameOver = true;

            
            //Using player prefs for death counter since the scene is reloaded on every death
            int deathCounter = PlayerPrefs.GetInt("DeathCounter") + 1;
            PlayerPrefs.SetInt("DeathCounter", deathCounter);

            if(deathCounter == UnityAds.instance.replaysBeforeAd)
            {
                deathCounter = 0;
                PlayerPrefs.SetInt("DeathCounter", deathCounter);
                UnityAds.instance.ShowAd();
            }

            AppsFlyerMMP.Score(gameManager.getScore());

			#if AADOTWEEN
			DOTween.KillAll ();

			EventManager.DOAnimFailEvent(AnimFail.start);

			transform.DOShakePosition (0.5f, 0.5f, 100, 90, false).SetUpdate (true).OnComplete (() => {
				transform.DOLocalMoveY (-10, 1f).SetUpdate (true).OnComplete (() => {
					gameObject.SetActive(false);

					EventManager.DOAnimFailEvent(AnimFail.end);

				});
			});
			#endif
		}



		void MoveLeft()
		{
			if (isJumping || isGameOver)
				return;
			DoMove (-1);
		}

		void MoveRight()
		{
			if (isJumping || isGameOver)
				return;
			DoMove (+1);
		}


		void DoMove(int sign)
		{
			AnimationJumpStarted ();

			EventManager.DOPlayerJumpEvent();

			currentDegree += sign*45f;

			RotateTo (sign);
		}

		void RotateTo(float direction)
		{
			float currentrotation = transform.parent.rotation.eulerAngles.z;

			float rotateTo = currentrotation + direction * 45;

			#if AADOTWEEN
			transform.parent.DORotate (rotateTo * Vector3.forward, 0.3f)
				.SetUpdate (true)
				.SetEase (Ease.OutQuad)
				.OnUpdate (AnimationJumpStarted)
				.OnStart(AnimationJumpStarted)
				.OnComplete (AnimationJumpCompleted);

			transform.DOLocalMoveY (-2, 0.15f)
				.SetUpdate (true)
				.SetEase(Ease.OutQuad)
				.OnUpdate (AnimationJumpStarted)
				.OnStart(AnimationJumpStarted)
				.OnComplete (()=>{
					transform.DOLocalMoveY (-3.13f, 0.15f)
						.SetEase(Ease.InQuad)
						.OnStart(AnimationJumpStarted)
						.OnComplete (AnimationJumpCompleted);
				});


			shadow.DOScale(0.0f * shadowLocalScale, 0.15f)
				.SetUpdate (true)
				.SetEase(Ease.OutQuad)
				.OnComplete (()=>{
					shadow.DOScale(shadowLocalScale, 0.15f)
						.SetEase(Ease.InQuad)
						.OnStart(AnimationJumpStarted)
						.OnComplete (AnimationJumpCompleted);
				});
			#endif

		}

		public void AnimationJumpStarted()
		{
			isJumping = true;
		}

		public void AnimationJumpCompleted()
		{
			isJumping = false;
		}

		public void DoExplosionParticle()
		{
			particleExplosion.Emit (40);

			EventManager.DOGetPointEvent();
		}

		bool isGrounded()
		{
			if (isJumping)
				return true;

			Vector3 down = transform.TransformDirection(Vector3.down);
			if (Physics.Raycast(groundCheck.position, down, 1)) {
				return true;
			}
			return false;
		}


		public int GetPlayerPosition()
		{

			var rot = transform.parent.rotation.eulerAngles.z;

			if(-22.5f <= rot && rot < 22.5f)
				return 0;

			if(22.5f <= rot && rot < 67.5f)
				return 1;

			if(67.5f <= rot && rot < 112.5f)
				return 2;

			if(112.5f <= rot && rot < 157.5f)
				return 3;

			if(157.5f <= rot && rot < 202.5f)
				return 4;

			if(202.5f <= rot && rot < 247.5f)
				return 5;

			if(247.5f <= rot && rot < 292.5f)
				return 6;

			if(292.5f <= rot && rot < 337.5f)
				return 7;

			if(337.5f <= rot && rot < 382.5f)
				return 0;

			Debug.LogWarning ("NO POSITION????????? : " + rot);

			return 0;
		}

        private void OnApplicationQuit()
        {
            //Reset death counter back to 0 for unity ads when the player quits the game
            PlayerPrefs.SetInt("DeathCounter", 0);
        }
    }
}