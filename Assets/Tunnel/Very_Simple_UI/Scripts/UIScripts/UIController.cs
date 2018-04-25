
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/





#pragma warning disable 0162 // code unreached.
#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.
#pragma warning disable 0618 // obslolete
#pragma warning disable 0108 
#pragma warning disable 0649 //never used

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using System.Collections;
#if AADOTWEEN
using DG.Tweening;
#endif
#if APPADVISORY_ADS
using AppAdvisory.Ads;
#endif
#if VS_SHARE
using AppAdvisory.SharingSystem;
#endif

#if APPADVISORY_LEADERBOARD
using AppAdvisory.social;
#endif

namespace AppAdvisory.VSUI
{
	/// <summary>
	/// Class attached to the UIController GameObject (who is a Canvas).
	/// In Charge of all the logics of the UI: animation, events...
	/// </summary>
	public class UIController : MonoBehaviour 
	{
		#if APPADVISORY_ADS

		#if UNITY_ANDROID
		public bool isAmazon = true;
		#else
		bool isAmazon = false;
		#endif

		#else
		public bool isAmazon = false;
		#endif

		/// <summary>
		/// URL of the iOS game. Find it on iTunes Connect.
		/// </summary>
		public string iD_iOS = "1134939249";
		public string url_ios
		{
			get
			{
				return "https://itunes.apple.com/us/app/" + iD_iOS; //1134939249
			}
		}
		/// <summary>
		/// URL of the Android game. Find it on Google Play.
		/// </summary>
		public string bundleIdAndroid = "com.appadvisory.ab2";
		public string url_android
		{
			get
			{
				return "https://play.google.com/store/apps/details?id=" + bundleIdAndroid; //1134939249
			}
		}
		/// <summary>
		/// URL of the Amazon game. Find it on the Amazon Developer Console.
		/// </summary>
		public string amazonID = "B01DPBSF2A";
		public string url_amazon
		{
			get
			{
				return "https://www.amazon.fr/dp/" + amazonID; //1134939249
			}
		}

		public string URL_STORE
		{
			get
			{
				string URL = "";

				#if UNITY_IOS
				URL = url_ios;
				#else
				URL = url_android;
				if(isAmazon)
					URL = url_amazon;
				#endif

				return URL;
			}
		}

		public RectTransform buttonVerySimpleShare;
		#if !VS_SHARE
		public string VerySimpleAdsURL = "http://u3d.as/oWD";
		#endif

		#if VS_SHARE
		public string shareTextBeforeUrl = "Get it here for free: "; 
		public string shareTextAfterUrl = " #appadvisory"; 
		#endif

		public void HideVerySimpleShare()
		{
			#if VS_SHARE
			VSSHARE.DOHideScreenshotIcon();
			#endif
		}

		public void OnClickedVerySimpleShare()
		{
			#if !VS_SHARE
			Debug.LogWarning("To take and share screenshots, you need Very Simple Share: " + VerySimpleAdsURL);
			Debug.LogWarning("Very Simple Share: " + VerySimpleAdsURL);
			Debug.LogWarning("Very Simple Share is ready to use in the game template: " + VerySimpleAdsURL);
			AnimVerySimpleShare(false);
			#endif
		}

		public void DOTakeScreenshotWithVerySimpleShare()
		{
			#if VS_SHARE
			VSSHARE.DOTakeScreenShot();
			#endif
		}

		void AnimVerySimpleShare(bool animIn)
		{
			#if AADOTWEEN
			if(buttonVerySimpleShare == null)
				return;

			#if !VS_SHARE
			if(animIn)
			{
				buttonVerySimpleShare.localScale = Vector2.zero;
				buttonVerySimpleShare.DOScale(Vector2.one, 0.3f);
			}
			else
			{
				//				buttonVerySimpleShare.localScale = Vector2.one;
				buttonVerySimpleShare.DOScale(Vector2.zero, 0.3f);
			}
			#else
			Destroy(buttonVerySimpleShare.gameObject);
			Debug.LogWarning("Please destroy the Game Object Button_Very_Simple_Share and drag and drop VSSHARE prefab instead");
			#endif

			#endif
		}

		#region Methods

		void Awake()
		{
			GC.Collect();
			Time.fixedDeltaTime = 1f/60f;
			Time.maximumDeltaTime = 3 * Time.fixedDeltaTime;
			DesactivateUIFitter();
			self = this;
			SetInGameScoreOut();

			#if APPADVISORY_ADS && UNITY_ANDROID
			this.isAmazon = AdsManager.instance.adIds.isAmazon;
			#endif
		}

		void Start()
		{
			if(doAnimInAtStart)
			{
				DOAnimIN();
			}
		}

		#region Animation IN
		/// <summary>
		/// Method called to do the animation IN, ie. from "out of the screen" to "in the screen".
		/// We will anim from top and horizontally.
		/// </summary>
		public void DOAnimIN () 
		{	
			AnimVerySimpleShare(true);

			DesactivateUIFitter();

			OnUIAnimInStart.Invoke();

			bool animFromTopFinished = false;
			bool animHorizontallyFinished = false;
			AnimateINFromTop(() => {
				animFromTopFinished = true;

				if(animFromTopFinished && animHorizontallyFinished)
				{
					animFromTopFinished = false;
					animHorizontallyFinished = false;
					OnUIAnimInEnd.Invoke();
				}
			});
			AnimateINHorizontaly(() => {
				animHorizontallyFinished = true;

				if(animFromTopFinished && animHorizontallyFinished)
				{
					animFromTopFinished = false;
					animHorizontallyFinished = false;
					OnUIAnimInEnd.Invoke();
				}
			});
		}
		/// <summary>
		/// Do the animation IN, ie. from "out of the screen" to "in the screen", from top.
		/// </summary>
		void AnimateINFromTop(Action callback)
		{
			DesactivateUIFitter();

			int countCompleted = 0;

			if(toAnimateFromTop != null && toAnimateFromTop.Length != 0)
			{
				for(int i = 0; i < toAnimateFromTop.Length; i++)
				{
					var r = toAnimateFromTop[i];

					var p = r.localPosition;

					p.y = Screen.height * 2;
					r.localPosition = p;

					CanvasGroup cg = r.GetComponent<CanvasGroup>();

					if(cg != null)
						cg.alpha = 0;
			
					#if AADOTWEEN
					r.DOLocalMoveY(0, 0.5f).SetDelay(0.5f + i * 0.1f)
						.OnStart(() => {
							if(cg != null)
								cg.DOFade(1, 0.2f);
						})
						.OnComplete(() => {
							countCompleted++;
							if(countCompleted >= toAnimateFromTop.Length)
							{
								if(callback!=null)
									callback();
							}
						});
					#endif
				}
			}
		}
		/// <summary>
		/// Do the animation IN, ie. from "out of the screen" to "in the screen", horizontally.
		/// </summary>
		void AnimateINHorizontaly(Action callback)
		{
			int countCompleted = 0;

			if(toAnimateHorizontaly != null && toAnimateHorizontaly.Length != 0)
			{
				for(int i = 0; i < toAnimateHorizontaly.Length; i++)
				{
					var r = toAnimateHorizontaly[i];

					if(i%2==0)
					{
						if(r != null)
							r.localPosition = new Vector2(-Screen.width * 2f, r.localPosition.y);
					}
					else
					{
						if(r != null)
							r.localPosition = new Vector2(+Screen.width * 2f, r.localPosition.y);
					}

					if( r != null)
					{
						CanvasGroup cg = r.GetComponent<CanvasGroup>();

						if(cg != null)
							cg.alpha = 0;

						#if AADOTWEEN
						r.DOLocalMoveX(0, 0.5f).SetDelay(0.5f + i * 0.1f)
							.OnStart(() => {
								if(cg != null)
									cg.DOFade(1, 0.5f);
							})
							.OnComplete(() => {
								countCompleted++;
								if(countCompleted >= toAnimateHorizontaly.Length)
								{
									if(callback!=null)
										callback();
								}
							});
						#endif
					}
					else
					{
						countCompleted++;
						if(countCompleted >= toAnimateHorizontaly.Length)
						{
							if(callback!=null)
								callback();
						}
					}
				}
			}
		}
		/// <summary>
		/// Do the animation OUT on the Y position of the UI Text score in game.
		/// </summary>
		static public void DOAnimOutScore()
		{
			self.DOAnimOutScoreInGame();
		}
		/// <summary>
		/// Do the animation OUT on the Y position of the UI Text score in game.
		/// </summary>
		public void DOAnimOutScoreInGame()
		{
			#if AADOTWEEN
			scoreIngame.DOKill();

			RectTransform r = scoreIngame.GetComponent<RectTransform>();

			r.DOLocalMoveY(Screen.height * 2, 0.5f).SetDelay(0.5f);
			#endif
		}
		#endregion


		#region Animation OUT
		/// <summary>
		/// Method called to do the animation OUT, ie. from "in the the screen" to "out of the screen".
		/// We will anim from top and horizontally.
		/// </summary>
		public void DOAnimOUT () 
		{
			AnimVerySimpleShare(false);

			DOAnimInScoreInGame();

			OnUIAnimOutStart.Invoke();

			bool animFromTopFinished = false;
			bool animHorizontallyFinished = false;

			AnimateOUTFromTop(() => {
				animFromTopFinished = true;

				if(animFromTopFinished && animHorizontallyFinished)
				{
					animFromTopFinished = false;
					animHorizontallyFinished = false;
					OnUIAnimOutEnd.Invoke();
				}
			});
			AnimateOUTHorizontaly(() => {
				animHorizontallyFinished = true;

				if(animFromTopFinished && animHorizontallyFinished)
				{
					animFromTopFinished = false;
					animHorizontallyFinished = false;
					OnUIAnimOutEnd.Invoke();
				}
			});
		}
		/// <summary>
		/// Do the animation OUT, ie. from "in the screen" to "out of the screen", from top.
		/// </summary>
		void AnimateOUTFromTop(Action callback)
		{
			int countCompleted = 0;

			if(toAnimateFromTop != null && toAnimateFromTop.Length != 0)
			{
				for(int i = 0; i < toAnimateFromTop.Length; i++)
				{
					var r = toAnimateFromTop[i];

					CanvasGroup cg = r.GetComponent<CanvasGroup>();

					if(cg != null)
						cg.alpha = 1;
					#if AADOTWEEN
					r.DOLocalMoveY(Screen.height * 2f, 0.5f).SetDelay(0.1f + i * 0.03f)
						.OnStart(() => {
							if(cg != null)
								cg.DOFade(0, 0.5f);
						})
						.OnComplete(() => {
							countCompleted++;
							if(countCompleted >= toAnimateFromTop.Length)
							{
								if(callback!=null)
									callback();
							}
						});
					#endif
				}
			}
		}
		/// <summary>
		/// Do the animation OUT, ie. from "in the screen" to "out of the screen", horizontaly.
		/// </summary>
		void AnimateOUTHorizontaly(Action callback)
		{
			int countCompleted = 0;

			if(toAnimateHorizontaly != null && toAnimateHorizontaly.Length != 0)
			{
				for(int i = 0; i < toAnimateHorizontaly.Length; i++)
				{
					var r = toAnimateHorizontaly[i];

					int sign = 1;

					if(i%2==0)
					{
						sign = -1;
					}

					CanvasGroup cg = r.GetComponent<CanvasGroup>();

					if(cg != null)
						cg.alpha = 1;
					
					#if AADOTWEEN
					r.DOLocalMoveX(sign * Screen.width * 2f, 0.5f).SetDelay(0.1f + i * 0.03f)
						.OnStart(() => {
							if(cg != null)
								cg.DOFade(0, 0.5f);
						})
						.OnComplete(() => {
							countCompleted++;
							if(countCompleted >= toAnimateHorizontaly.Length)
							{
								if(callback!=null)
									callback();
							}
						});
					#endif

				}
			}
		}
		/// <summary>
		/// Set the Y position of the UI Text score in game out of the screen.
		/// </summary>
		void SetInGameScoreOut()
		{
			#if AADOTWEEN
			scoreIngame.DOKill();
			RectTransform r = scoreIngame.GetComponent<RectTransform>();

			var p = r.localPosition;

			p.y = Screen.height * 2;
			r.localPosition = p;
			#endif
		}
		/// <summary>
		/// Do the animation IN on the Y position of the UI Text score in game.
		/// </summary>
		public void DOAnimInScoreInGame()
		{
			#if AADOTWEEN
			scoreIngame.DOKill();

			SetInGameScoreOut();

			RectTransform r = scoreIngame.GetComponent<RectTransform>();

			r.DOLocalMoveY(0, 0.5f).SetDelay(0.5f);
			#endif
		}
		#endregion
		#endregion
		/// <summary>
		/// To true if you want to have the anim in at start.
		/// </summary>
		public bool doAnimInAtStart = true;
		/// <summary>
		/// Set an unique instance of this GameObject.s
		/// </summary>
		static private UIController self;

		#region UI elements
		#region scoreInGame
		public Text scoreIngame;
		/// <summary>
		/// Set the in game score.
		/// </summary>
		static public void SetScore(int score)
		{
			#if AADOTWEEN
			Text uiScore = self.scoreIngame;

			uiScore.text = score.ToString();

			uiScore.rectTransform.DOKill();

			uiScore.transform.localScale = Vector3.one;

			if(score == 0)
				return;

			uiScore.rectTransform.DOScale(Vector2.one * 1.5f, 0.3f).SetEase(Ease.InBack).SetLoops(2, LoopType.Yoyo);
			#endif
		}
		/// <summary>
		/// Set the UI Text best score.
		/// </summary>
		static public void SetUIBestScore(int point)
		{
			self.SetBestText(point);
		}
		/// <summary>
		/// Set the UI Text last score.
		/// </summary>
		static public void SetUILastScore(int point)
		{
			self.SetLastText(point);
		}
		#endregion
		#region ToDesactivate
		public LayoutGroup[] layoutGroupToDesactivateAtStart;
		public ContentSizeFitter[] contentSizeFitterToDesactivateAtStart;
		public LayoutElement[] layoutElementToDesactivate;
		void DesactivateUIFitter()
		{
			if(layoutGroupToDesactivateAtStart != null && layoutGroupToDesactivateAtStart.Length > 0)
			{
				foreach(var v in layoutGroupToDesactivateAtStart)
				{
					v.enabled = false;
				}
			}

			if(contentSizeFitterToDesactivateAtStart != null && contentSizeFitterToDesactivateAtStart.Length > 0)
			{
				foreach(var v in contentSizeFitterToDesactivateAtStart)
				{
					v.enabled = false;
				}
			}

			if(layoutElementToDesactivate != null && layoutElementToDesactivate.Length > 0)
			{
				foreach(var v in layoutElementToDesactivate)
				{
					v.enabled = false;
				}
			}
		}
		#endregion
		/// <summary>
		/// Reference to all UI elements we will animate from the top of the screen.
		/// </summary>
		public RectTransform[] toAnimateFromTop;
		/// <summary>
		/// Reference to all UI elements we will animate horizontally.
		/// </summary>
		public RectTransform[] toAnimateHorizontaly;
		/// <summary>
		/// Reference to UI Text for the last score.
		/// </summary>
		public Text textLast;
		/// <summary>
		/// Reference to UI Text for the best score.
		/// </summary>
		public Text textBest;
		/// <summary>
		/// Set the UI Text best score.
		/// </summary>
		public void SetBestText(int point)
		{
			textBest.text  = "Best\n " + point;
		}
		/// <summary>
		/// Set the UI Text last score.
		/// </summary>
		public void SetLastText(int point)
		{
			textLast.text  = "Last\n " + point;
		}

		#endregion

		#region Unity Events
		#region Animation IN
		/// <summary>
		/// Unity event triggered when the animation IN, ie. from "out of the screen" to "in the screen" is started.
		/// </summary>
		[System.Serializable] public class OnUIAnimInStartHandler : UnityEvent{}
		/// <summary>
		/// Unity event triggered when the animation IN, ie. from "out of the screen" to "in the screen" is started.
		/// </summary>
		[SerializeField] public OnUIAnimInStartHandler OnUIAnimInStart;

		/// <summary>
		/// Unity event triggered when the animation IN, ie. from "out of the screen" to "in the screen" is ended.
		/// </summary>
		[System.Serializable] public class OnUIAnimInEndHandler : UnityEvent{}
		/// <summary>
		/// Unity event triggered when the animation IN, ie. from "out of the screen" to "in the screen" is ended.
		/// </summary>
		[SerializeField] public OnUIAnimInEndHandler OnUIAnimInEnd;
		#endregion

		#region Animation OUT
		/// <summary>
		/// Unity event triggered when the animation OUT, ie. from "in the the screen" to "out of screen" is started.
		/// </summary>
		[System.Serializable] public class OnUIAnimOUTStartHandler : UnityEvent{}
		/// <summary>
		/// Unity event triggered when the animation OUT, ie. from "in the the screen" to "out of screen" is started.
		/// </summary>
		[SerializeField] public OnUIAnimOUTStartHandler OnUIAnimOutStart;

		/// <summary>
		/// Unity event triggered when the animation OUT, ie. from "in the the screen" to "out of screen" is ended.
		/// </summary>
		[System.Serializable] public class OnUIAnimOUTEndHandler : UnityEvent{}
		/// <summary>
		/// Unity event triggered when the animation OUT, ie. from "in the the screen" to "out of screen" is ended.
		/// </summary>
		[SerializeField] public OnUIAnimOUTEndHandler OnUIAnimOutEnd;
		#endregion
		#endregion


	}
}