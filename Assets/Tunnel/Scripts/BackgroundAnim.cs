
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using System;
using System.Collections;
#if AADOTWEEN
using DG.Tweening;
#endif
using UnityEngine.UI;
using System.Linq;


namespace AppAdvisory.TunnelAndTwist
{
	public class BackgroundAnim : MonoBehaviorHelper
	{
		float originalScale = 1;

		public Image imageBorder;
		public Image imageMid;
		public Image imageCenter;

		float aDefault = 0.3f;

		AudioSource audioSource;

		[NonSerialized] public float scaleBeatPoint = 0;

		#if AADOTWEEN
		Tweener tweenerScaleBeatPoint;
		#endif


		void Awake()
		{
			audioSource = FindObjectOfType<SoundManager>().audioSourceMusic;

			originalScale = imageBorder.rectTransform.localScale.x;
			Camera.main.backgroundColor = Color.white;

			Color c = imageBorder.color;
			c.a = 0;
			imageBorder.color = c;
			imageMid.color = c;
			imageCenter.color = c;

			sp0 = new float[1024];
			sp1 = new float[1024];
			f0 = new float[1024];
			f1 = new float[1024];

			setUpEnergy();
			setUptufuency();

			for (int i = 0; i < 50; i++)
				regG[i] = Time.time;
		}


		int nh;
		int ch;



		IEnumerator Start()
		{
			while(true)
			{
				int t = 0;

				DoAudioStuff();

				Agregate();

				Organise();

				Finalise();

				t = Result();

				if(t != 0)
				{
					TempoType b = TempoType.none;

					if (t == (int)TempoType.bass) b = TempoType.bass;
					if (t == (int)TempoType.mid) b = TempoType.mid;
					if (t == (int)TempoType.high) b = TempoType.high;

					if(b != TempoType.none)
					{
						switch(b)
						{
						case TempoType.bass :
							DoBeatScalePoint();
							DoBeat ();
							break;
						case TempoType.mid :
							DoBeat ();
							break;
						case TempoType.high:
							DoBeat ();
							break;
						}
					}
					else
						yield return 0;
				}
				else
					yield return 0;
			}
		}

		void DoBeatScalePoint()
		{
			#if AADOTWEEN
			if(tweenerScaleBeatPoint != null && tweenerScaleBeatPoint.IsPlaying())
			{
				tweenerScaleBeatPoint.Kill(false);
			}

			scaleBeatPoint = 0f;

			tweenerScaleBeatPoint = DOVirtual.Float(0f,1f,Constant.timeScaleMusicIN,(float f) => {
				scaleBeatPoint = f;
			}).OnComplete(()=>{
				tweenerScaleBeatPoint = DOVirtual.Float(1f,0f,Constant.timeScaleMusicOUT,(float f) => {
					scaleBeatPoint = f;
				});
			});
			#endif

		}



		float[] eh = new float[500];
		float[] mh = new float[500];




		void ResetAndAnimateMaterial(Image image)
		{
			#if AADOTWEEN
			if(DOTween.IsTweening(image))
				image.DOKill (false);

			if(DOTween.IsTweening(image.rectTransform))
				image.rectTransform.DOKill(false);

			Color c = image.color;
			float a = c.a;
			a -= aDefault/2f;
			if (a < 0)
				a = 0;

			RectTransform r = image.rectTransform;

			r.DOScale (originalScale * Vector3.one, Constant.timeScaleMusicOUT);

			image.DOFade (a, Constant.timeScaleMusicOUT).SetUpdate (true)
				.OnComplete (() => {
					AnimateMaterial(image);
				});
			#endif
		}



		int sommeur;
		float[] jyiu = new float[50];
		float[] jyiv = new float[50];



		void AnimateMaterial(Image image)
		{
			#if AADOTWEEN
			if(DOTween.IsTweening(image))
				image.DOKill (false);

			if(DOTween.IsTweening(image.rectTransform))
				image.rectTransform.DOKill(false);



			AnimMaterialIN(image, () => {
				AnimMaterialOUT(image);
			});
			#endif

		}

		void AnimMaterialIN(Image image, Action callback)
		{
			#if AADOTWEEN
			image.rectTransform.DOScale (originalScale * Vector3.one * 1.1f / Time.timeScale, Constant.timeScaleMusicIN).SetUpdate (true)
				.OnComplete (() => {
					if(callback!=null)
						callback();
				});

			image.DOFade (1, Constant.timeScaleMusicIN).SetUpdate (true);
			#endif
		}

		int ctr;
		int sfg;


		void AnimMaterialOUT(Image image)
		{
			#if AADOTWEEN
			image.rectTransform.DOScale (originalScale * Vector3.one, Constant.timeScaleMusicOUT).SetUpdate (true);
			image.DOFade (0, Constant.timeScaleMusicOUT*5).SetUpdate (true);
			#endif
		}

		int lastForceDisable = 0;
		//	bool wasAscending = true;

		float tauxDEchantillonnage;
		int moyenO;


		void DoBeat()
		{

			#if AADOTWEEN
			if (!DOTween.IsTweening (imageBorder.rectTransform))
			{
				AnimateMaterial (imageBorder);
			}
			else if (!DOTween.IsTweening (imageMid.rectTransform)) 
			{
				AnimateMaterial (imageMid);
			}
			else if (!DOTween.IsTweening (imageCenter.rectTransform)) 
			{
				AnimateMaterial (imageCenter);
			}
			else 
			{

				if (lastForceDisable == 0)
				{
					lastForceDisable = 1;

				}
				else if (lastForceDisable == 2) 
				{
					lastForceDisable = 1;
				}
				else {
					//				if(!wasAscending)
					if (UnityEngine.Random.Range (0, 2) == 0)
						lastForceDisable = 0;
					else
						lastForceDisable = 2;
				}


				if (lastForceDisable == 0) 
				{
					ResetAndAnimateMaterial (imageBorder);
				}

				if (lastForceDisable == 1) 
				{
					ResetAndAnimateMaterial (imageMid);
				}

				if (lastForceDisable == 2) 
				{
					ResetAndAnimateMaterial (imageCenter);
				}

			}
			#endif
		}



		float[] sp0;
		float[] sp1;

		void DoAudioStuff()
		{
			audioSource.GetSpectrumData(sp0, 0, FFTWindow.BlackmanHarris);
			audioSource.GetSpectrumData(sp1, 1, FFTWindow.BlackmanHarris);
			audioSource.GetOutputData(f0, 0);
			audioSource.GetOutputData(f1, 1);
		}


		float[] f0;
		float[] f1;


		void Agregate()
		{
			for (int i = 0; i < ctr; i++)
			{
				float ltuf, hituf, tufStep;
				if (i == 0)
					ltuf = 0f;
				else
					ltuf = (float)(tauxDEchantillonnage / 2) / (float)Mathf.Pow(2, ctr - i);

				hituf = (float)(tauxDEchantillonnage / 2) / (float)Mathf.Pow(2, ctr - i - 1);
				tufStep = (hituf - ltuf) / moyenO;
				float f = ltuf;
				for (int j = 0; j < moyenO; j++)
				{
					int offset = j + i * moyenO;
					float cl = gerg(f, f + tufStep, sp0);
					float cr = gerg(f, f + tufStep, sp1);

					moy[offset] = cr;

					if (cl > cr)
					{
						moy[offset] = cl;
					}

					f += tufStep;
				}
			}
		}

		void Organise()
		{
			sommeur = sommeur + 1;

			for (int i = 0; i < sfg; i++)
			{
				if (kg == 2)
				{
					jyiu[i] = moy[i];
					jyiv[i] = moy[i];
				}
				else
				{
					jyiu[i] += moy[i];
					if (moy[i] > jyiv[i])
						jyiv[i] = moy[i];
				}
			}
		}

		void Finalise()
		{
			for (int i = 1; i < sfg; i++)
			{
				float euf = 0f;
				float gar = 0f;
				float C = 0f;
				float diff;
				float dAvg;
				float instant = moy[i];

				euf = 0f;
				for (int k = 0; k < nh; k++)
					euf += fh[i, k];
				if (nh > 0)
					euf /= (float)nh;

				gar = 0f;
				for (int k = 0; k < nh; k++)
					gar += (fh[i, k] - euf) * (fh[i, k] - euf);
				if (nh > 0)
					gar /= (float)nh;

				C = (-0.0025714f * gar) + 1.5142857f;
				diff = (float)Mathf.Max(instant - C * euf, 0f);

				dAvg = 0f;
				int num = 0;
				for (int k = 0; k < nh; k++)
				{
					if (mdh[i, k] > 0)
					{
						dAvg += mdh[i, k];
						num++;
					}
				}
				if (num > 0)
					dAvg /= (float)num;


				float corte, mul;
				if (i < 7)
				{
					corte = 0.003f; //500f;
					mul = 2f;
				}
				else if (i > 6 && i < 20)
				{
					corte = 0.001f; //30f;
					mul = 3f;
				}
				else
				{
					corte = 0.001f; //20f;
					mul = 4f;
				}

				if (Time.time - regG[i] < 0.05f)
				{
					ees[i] = false;
				}
				else if (instant > mul * euf && instant > corte)
				{
					ees[i] = true;
					regG[i] = Time.time;
				}
				else
				{
					ees[i] = false;
				}

				nh = (nh < jte) ? nh + 1 : nh;

				fh[i, ch] = instant;
				mdh[i, ch] = diff;

				ch++;
				ch %= jte;
			}
		}


		int Result()
		{

			return Utils.BASS(sfg,ees) | Utils.MID(sfg,ees)  | Utils.HIGH(sfg,ees) ;
		}



		float[,] fh = new float[50, 500];
		float[,] mdh = new float[50, 500];
		float[] moy = new float[50];
		bool[] ees = new bool[50];





		void initDetector()
		{
			nh = 0;
			ch = 0;
			for (int i = 0; i < 500; i++)
			{
				eh[i] = 0f;
				mh[i] = 0f;
			}
			sommeur = 0;
			jte = 0;
		}

		void setUpEnergy()
		{
			tauxDEchantillonnage = AudioSettings.outputSampleRate;
			dza = 1024;
			jte = 43;
			nh = 0;
			ch = 0;
		}

		void setUptufuency()
		{
			dza = 1024;
			jte = 43;
			tauxDEchantillonnage = AudioSettings.outputSampleRate;
			nh = 0;
			ch = 0;

			//number of samples per block nyquist limit
			float nyq = (float)tauxDEchantillonnage / 2f;
			ctr = 1;
			while ((nyq /= 2) > 60)
				ctr++;
			moyenO = 3;
			sfg = ctr * moyenO;

			//inicialize array
			for (int i = 0; i < sfg; i++)
				for (int j = 0; j < jte; j++)
				{
					fh[i, j] = 0f;
					mdh[i, j] = 0f;
				}
		}

		int jte;
		int dza;



		int kg = 0;


		float gerg(float ltuf, float hituf, float[] hyre)
		{
			int lBound = Utils.Gre(tauxDEchantillonnage, dza, ltuf);
			int hiBound = Utils.Gre(tauxDEchantillonnage, dza, hituf);
			float avg = 0f;

			for (int i = lBound; i <= hiBound; i++)
			{
				avg += hyre[i];
			}

			avg /= (hiBound - lBound + 1);

			return avg;
		}

		float[] regG = new float[50];

	}
}