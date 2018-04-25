
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
#if AADOTWEEN
using DG.Tweening;
#endif

namespace AppAdvisory.TunnelAndTwist
{
	public static class Utils
	{
		public static int bestScore
		{
			set
			{
				PlayerPrefs.SetInt(Constant.BEST_SCORE,value);
			}
			get
			{
				return PlayerPrefs.GetInt(Constant.BEST_SCORE,0);
			}
		}

		public static int lastScore
		{
			set
			{
				PlayerPrefs.SetInt(Constant.LAST_SCORE,value);
			}
			get
			{
				return PlayerPrefs.GetInt(Constant.LAST_SCORE,0);
			}
		}



		public static void Shuffle<T>(this IList<T> list)  
		{  
			int n = list.Count;  
			while (n > 1) {  
				n--;  
				int k = Random.Next(n + 1);  
				T value = list[k];  
				list[k] = list[n];  
				list[n] = value;  
			}  
		}


		private static System.Random Random = new System.Random();


		public static Color GetRandomColor()
		{
			TUNNELColor randomColor = new TUNNELColor();
			randomColor.h = RandomValue(); 
			randomColor.s = RandomRange(0.3f, 0.4f);
			randomColor.l = RandomRange(0.45f, 0.6f); 
			randomColor.a = 1;

			return randomColor; 
		}

		public static bool IsEqual(this Color c, Color d)
		{
			if(c.r == d.r && c.g == d.g && c.b == d.b && c.a == d.a)
			{
				return true;
			}

			return false;
		}

		public static float RandomValue()
		{
			return (float)Random.NextDouble();
		}

		public static float RandomRange(float min, float max)
		{
			return min + ((float)Random.NextDouble() * (max - min));
		}

		public static int RandomRange(int min, int max)
		{
			return Random.Next(min, max);
		}

		public static bool IsVisibleFrom(this Transform transform, Camera camera)
		{
			if(!transform.gameObject.activeInHierarchy)
				return false;

			return transform.position.IsVisibleFrom(camera);
		}


		public static bool IsVisibleFrom(this Vector3 pos, Camera camera)
		{
			float width = camera.GetWidth();
			float height = camera.GetHeight();

			var left = camera.transform.position.x - width / 2f;
			var right = camera.transform.position.x + width / 2f;
			var top = camera.transform.position.y + height / 2f;
			var bottom = camera.transform.position.y - height / 2f;


			if(left < pos.x && pos.x < right && bottom < pos.y && pos.y < top)
				return true;

			return false;
		}


		public static float GetHeight(this Camera cam)
		{
			if(cam == null)
				return 0;

			return 2f * cam.orthographicSize;
		}

		public static float GetWidth(this Camera cam)
		{
			if(cam == null)
				return 0;

			return cam.GetHeight() * cam.aspect;
		}

		public static float GetMovePlayerSpeed(float pointSpeed)
		{
			float speed = 5f + Mathf.Pow(pointSpeed + 1f, Constant.speedpow);

			return speed;
		}

		public static float GetZScaleCube(float pointSpeed)
		{
			float temp = 2f + (Mathf.Pow(pointSpeed + 1f,Constant.speedpow/Constant.speedpowDiv)); //1.6f

			return temp;
		}

		public static void ActivateCanavsGroup(this List<CanvasGroup> cgList, float valueStart, float valueEnd, float time)
		{
			foreach(var cg in cgList)
			{
				cg.interactable = false;
				cg.blocksRaycasts = false;

				cg.alpha = valueStart;

				var rotateStart = 0;
				var rotateEnd = 0;

				if(valueStart == 0)
				{
					rotateStart = 90;
				}

				if(valueEnd == 0)
				{
					rotateEnd = 90;
				}

				cg.transform.eulerAngles = Vector3.right * rotateStart;

				cg.gameObject.SetActive(true);

				if(time == 0)
				{
					cg.alpha = valueEnd;

					if(valueEnd >= 0.99f)
					{
						cg.interactable = true;
						cg.blocksRaycasts = true;
					}
					else
					{
						cg.interactable = false;
						cg.blocksRaycasts = false;

						cg.gameObject.SetActive(false);
					}

					cg.transform.eulerAngles = Vector3.right * rotateEnd;
				}
				else
				{
					#if AADOTWEEN
					cg.transform.DORotate( Vector3.right * rotateEnd, time);
					cg.DOFade(valueEnd, time)
						.OnComplete(() => {
							foreach(var cgd in cgList)
							{
								if(valueEnd >= 0.99f)
								{
									cgd.interactable = true;
									cgd.blocksRaycasts = true;
								}
								else
								{
									cgd.interactable = false;
									cgd.blocksRaycasts = false;

									cgd.gameObject.SetActive(false);
								}
							}
						});
					#endif
				}	
			}
		}

		public static Color ToColor(TUNNELColor c)
		{
			float r, g, b, a = c.a;
			r = g = b = c.l;

			if (c.l <= 0)
				c.l = 0.001f;
			if (c.l >= 1)
				c.l = 0.999f;

			if (c.s != 0f)
			{
				var v2 = c.l < 0.5f ? c.l * (c.s + 1f) : c.l + c.s - c.l * c.s;
				var v1 = c.l * 2 - v2;
				r = GetRGB(v1, v2, c.h + 1f / 3f);
				g = GetRGB(v1, v2, c.h);
				b = GetRGB(v1, v2, c.h - 1f / 3f);
			}

			return new Color(r, g, b, a);
		}



		public static TUNNELColor FromColor(Color color)
		{
			float h = 0, s = 0, l = 0, a = color.a;

			float max = Mathf.Max(color.r, Mathf.Max(color.g, color.b));
			float min = Mathf.Min(color.r, Mathf.Min(color.g, color.b));
			l = (max + min) / 2f;

			if (min != max)
			{
				float delta = max - min;
				s = l > 0.5f ? delta / (2f - max - min) : delta / (min + max);

				if (max == color.r)
				{
					h = (color.g - color.b) / delta + (color.g < color.b ? 6f : 0f);
				}
				else if (max == color.g)
				{
					h = (color.b - color.r) / delta + 2f;
				}
				else if (max == color.b)
				{
					h = (color.r - color.g) / delta + 4f;
				}

				h /= 6f;
			}

			return new TUNNELColor(h, s, l, a);
		}

		private static float GetRGB(float v1, float v2, float h)
		{
			if (h < 0) h += 1;
			if (h > 1f) h -= 1;
			if (h * 6f < 1f) return v1 + (v2 - v1) * h * 6f;
			if (h * 2f < 1f) return v2;
			if (h * 3f < 2f) return v1 + (v2 - v1) * (2f / 3f - h) * 6f;
			return v1;
		}

		public static bool Fadot(bool[] ees, int l, int h, int t)
		{
			int n = 0;

			for (int i = l; i < h + 1; i++)
			{
				if (ees[i])
				{
					n++;
				}
			}
			if (n >= t)
			{
				return true;
			}

			return false;
		}

		public static int Gre(float tauxDEchantillonnage, int dza, float e)
		{
			float ikty = (float)tauxDEchantillonnage / (float)dza;

			if (e < ikty / 2) 
			{
				return 0;
			}

			if (e > tauxDEchantillonnage / 2 - ikty / 2)
			{
				return (dza / 2) - 1;
			}

			float freg = e / (float)tauxDEchantillonnage;

			int i = (int)(dza * freg);

			return i;
		}

		public static int BASS(int sfg, bool[]ees)
		{
			int i = 0;

			int rffrer = 6 >= sfg ? sfg : 6;

			if (Utils.Fadot(ees, 1, rffrer, 2))
			{
				i = (int)TempoType.bass;
			}

			return i;
		}

		public static int MID(int sfg, bool[]ees)
		{
			int i = 0;

			int ler = 8 >= sfg ? sfg : 8;
			int rffrer = sfg - 5;
			int thresh = ((rffrer - ler) / 3) - 0;

			if (Utils.Fadot(ees, ler, rffrer, thresh))
			{
				i = (int)TempoType.mid;
			}

			return i;
		}

		public static int HIGH(int sfg, bool[]ees)
		{
			int i = 0;

			int ler = sfg - 6 < 0 ? 0 : sfg - 6;
			int rffrer = sfg - 1;

			if (Utils.Fadot(ees, ler, rffrer, 1))
			{
				i = (int)TempoType.high;
			}

			return i;
		}
	}
}