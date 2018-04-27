
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
#if AADOTWEEN
using DG.Tweening;
#endif


namespace AppAdvisory.TunnelAndTwist
{
	public class SlowMotionLogic : MonoBehaviorHelper
	{
		public Image[] images;
		public Color[] colors;

		float ratio = 0.1f;

        //bools to check whether the player has gotten a combo 3 or 5
        private bool gotCombo3, gotCombo5;

		public int comboCount
		{
			get
			{

				int count = 0;

				for (int i = 0; i < images.Length; i++) 
				{
					var im = images [i];

					if (im.fillAmount != 0)
						count++;
					else
						break;

				}

				return count;
			}
		}

		public float defaultSize = 200;
		void Awake()
		{
			images = GetComponentsInChildren<Image> ();

			for (int i = 0; i < images.Length; i++) 
			{
				var im = images [i];
				im.rectTransform.sizeDelta =  defaultSize * Vector2.one * (1 - i * ratio);
				im.color = new Color (GetRatio(i), 0, 0, 1);
			}

			images = GetComponentsInChildren<Image> ();

			Color[] colors = new Color[images.Length];

			for (int i = 0; i < images.Length; i++) 
			{
				images [i] = transform.GetChild (transform.childCount - 1 - i).GetComponent<Image>();
				colors [i] = images [i].color;
			}

			foreach (Image im in images) 
			{
				im.fillAmount = 0f;
			}

			EventManager.DOPointColor(new Color (1f, 1f, 1f, 1));
		}

        void OnEnable()
		{
			EventManager.OnAddOnePoint += AddCombo;
		}

		void OnDisable()
		{
			EventManager.OnAddOnePoint -= AddCombo;
			#if AADOTWEEN
			if (t1 != null)
				t1.Kill (false);

			if (t2 != null)
				t2.Kill (false);
			#endif
		}

		void Start()
		{
            gotCombo3 = false;
            gotCombo5 = false;
			StartCoroutine(_CoUpdate());
		}

        private void Update()
        {
            /*if(!gotCombo3)
            {
                if (comboCount == 3)
                {
                    gotCombo3 = true;
                    AppsFlyerMMP.Combo3();
                }
                else if(comboCount < 3)
                    gotCombo3 = false;
            }
            if (!gotCombo5)
            {
                if (comboCount == 5)
                {
                    gotCombo5 = true;
                    AppsFlyerMMP.Combo5();
                }
                else if(comboCount < 5)
                    gotCombo5 = false;
            }*/
        }

        IEnumerator _CoUpdate()
		{
			while(true)
			{
				_UpdatePointColor ();

				yield return new WaitForSeconds(0.1f);
			}
		}

		public float pointScaleAdd = 0;
		float pointScaleAdd_RATIO = 0.8f;
		Color lastColor = Color.white;
		float lastPointScale = 0;
		void _UpdatePointColor()
		{
			float count = 0;
			for (int i = 0; i < images.Length; i++) 
			{
				var im = images [i];
				count += im.fillAmount;
			}

			count /= 10f;

			float temp = 1f - count*count;

			pointScaleAdd = pointScaleAdd_RATIO*(1f - temp);

			if(lastPointScale != pointScaleAdd)
			{
				EventManager.DOPointScale(pointScaleAdd);
				lastPointScale = pointScaleAdd;
			}

			Color c = new Color (1f, temp, temp, 1);

			if(!c.IsEqual(lastColor))
			{
				EventManager.DOPointColor(c);
				lastColor = c;
			}

		}

		float GetRatio(int i)
		{
			return  (1f * (images.Length - 1f - i)) / (images.Length - 1f);
		}

		public void AddCombo(GameObject o)
		{
			if (Time.timeScale != 1)
				return;

            Debug.Log(comboCount);
            //Combo 3 triggeres on comboCount is 2 because 0 is considered 1
            if (comboCount == 2)
                AppsFlyerMMP.Combo3();
            if (comboCount == 4)
                AppsFlyerMMP.Combo5();

			StopCoroutine ("CoUpdate");
			StartCoroutine ("CoUpdate");
		}

		void DoBackSlowMotion(int countBack)
		{
			if (countBack < 0)
				return;

			Image im = images [countBack];

			#if AADOTWEEN
			im.DOFillAmount (1, 1f).SetUpdate (true).OnComplete (() => {
				countBack --;
				DoBackSlowMotion(countBack);
			});
			#endif

		}

		public float defaultTimeFillAmountIn = 0.2f;
		public float defaultTimeFillAmountOut = 1f;

		IEnumerator CoUpdate()
		{

			for (int i = 0; i < images.Length; i++) 
			{
				EventManager.DOSetComboCount(comboCount);

				var im = images [i];

				float timer = 0;
				float v = im.fillAmount;
				float time = defaultTimeFillAmountIn * (1f - v);

				while (timer <= time)
				{
					timer += Time.deltaTime;

					float f = Mathf.Lerp (v, 1f, timer / time);

					im.color = new Color (1f * f, 0, 0, 1);

					im.fillAmount = f;


					yield return null;
				}

				EventManager.DOSetComboCount(comboCount);

				im.fillAmount = 1f;

				if (v == 0)
					break;
			}

			if (comboCount == images.Length) 
			{
				DoMotionBlur ();

			} 
			else 
			{
				for (int i = images.Length - 1; i >= 0; i--) 
				{

					EventManager.DOSetComboCount(comboCount);

					var im = images [i];

					float timer = 0;
					float v = im.fillAmount;
					float time = defaultTimeFillAmountOut * v;

					while (timer <= time) {
						timer += Time.deltaTime;

						float f = Mathf.Lerp (v, 0, timer / time);

						im.color = new Color (1f * f, 0, 0, 1);

						im.fillAmount = f;

						yield return null;
					}

					im.fillAmount = 0;

					EventManager.DOSetComboCount(comboCount);
				}
			}
		}

		//	void DoColorAnim()
		//	{
		//		for (int i = colors.Length - 1; i > 1; i--) 
		//		{
		//			images [i].DOColor (images [i - 1].color, 1f).SetUpdate (true).SetDelay(i*0.1f);
		//		}
		//
		//		Invoke ("DoColorAnim", 0.1f * 10 + 1f);
		//	}

		#if AADOTWEEN
		Tweener t1;
		Tweener t2;
		#endif

		float slowMotion = 0.7f;//0.63f;


		public void DoMotionBlur()
		{

			EventManager.DOSetComboCount(comboCount);

			if (Time.timeScale != 1)
				return;

			EventManager.DOMotionBlur(0,true);

			EventManager.DOSetComboCount(comboCount);

			#if AADOTWEEN
			t1 = DOVirtual.Float (1f, 0.5f, 2f, (float f) => {

				EventManager.DOMotionBlur((1f - f)*slowMotion*2,true);

				Time.timeScale = f;

				EventManager.DOSetComboCount(comboCount);

			}).SetUpdate (true)
				.OnComplete (() => {

					t2 = DOVirtual.Float (0.5f, 1f, 10f, (float f) => {

						EventManager.DOMotionBlur((1f - f)*slowMotion*2,true);

						Time.timeScale = f;

						EventManager.DOSetComboCount(comboCount);

					}).SetUpdate (true).SetDelay(10)
						.OnStart(()=>{
							int delay = 0;
							for (int i = images.Length - 1; i >= 0; i--) 
							{
								var im = images [i];

								im.DOFillAmount(0,1f).SetDelay(1f*delay).SetUpdate(true);

								delay++;
							}

							EventManager.DOSetComboCount(comboCount);

						})
						.OnComplete (() => {

							EventManager.DOMotionBlur(0.3f,false);

							Time.timeScale = 1;

							EventManager.DOSetComboCount(comboCount);	
						});
				});

			#endif
		}
	}
}