
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
	public class Spectrum : MonoBehaviour 
	{
		public AudioSource asource;
		public int channel;
		float[] spectrum; 
		float[] volume;
		public int numSamples;
		public GameObject enem;

		public Transform ball;

		List<GameObject> list = new List<GameObject>();

		public int freq;

		// Use this for initialization
		void Awake () {
			volume = new float[numSamples];
			spectrum = new float[numSamples];
			list = new List<GameObject>();
			for (int i=1; i<255; i++)

			{
				GameObject enemyClone = Instantiate(enem,new Vector3(i,0,0), transform.rotation) as GameObject ;
				enemyClone.name = "sp"+i;
				list.Add(enemyClone);
			}


		}

		// Update is called once per frame
		void Update () {
			asource.GetOutputData(volume, channel);
			asource.GetSpectrumData(spectrum, channel, FFTWindow.BlackmanHarris);

			for (int i=0; i<list.Count; i++) 
			{

				GameObject sp1 = list[i];
				sp1.transform.localScale= new  Vector3(1,200*spectrum[i],1);
				sp1.GetComponent<MeshRenderer>().material.color= new Color(100*spectrum[i],1f/(20*spectrum[i]),1f/(20*spectrum[i]),1);



			}

			print("freq " + freq + " = " + spectrum[freq]);
			/*GameObject sp1 = GameObject.Find("sp1");
		sp1.transform.localScale= new  Vector3(1,100*spectrum[0],1);
	GameObject sp2 = GameObject.Find("sp2");
		sp2.transform.localScale= new  Vector3(1,100*spectrum[1],1);
	GameObject sp3 = GameObject.Find("sp3");
		sp3.transform.localScale= new  Vector3(1,100*spectrum[2],1);
	GameObject sp4 = GameObject.Find("sp4");
		sp4.transform.localScale= new  Vector3(1,100*spectrum[3],1);
	GameObject sp5 = GameObject.Find("sp5");
		sp5.transform.localScale= new  Vector3(1,100*spectrum[4],1);
	GameObject sp6 = GameObject.Find("sp6");
		sp6.transform.localScale= new  Vector3(1,100*spectrum[5],1);
	GameObject sp7 = GameObject.Find("sp7");
		sp7.transform.localScale= new  Vector3(1,100*spectrum[6],1);
	GameObject sp8 = GameObject.Find("sp8");
		sp8.transform.localScale= new  Vector3(1,100*spectrum[7],1);
	GameObject sp9 = GameObject.Find("sp9");
		sp9.transform.localScale= new  Vector3(1,100*spectrum[8],1);
	GameObject sp10 = GameObject.Find("sp10");
		sp10.transform.localScale= new  Vector3(1,100*spectrum[9],1);
	GameObject sp11 = GameObject.Find("sp11");
		sp11.transform.localScale= new  Vector3(1,100*spectrum[10],1);
	GameObject sp12 = GameObject.Find("sp12");
		sp12.transform.localScale= new  Vector3(1,100*spectrum[11],1);
	GameObject sp13 = GameObject.Find("sp13");
		sp13.transform.localScale= new  Vector3(1,100*spectrum[12],1);
	GameObject sp14 = GameObject.Find("sp14");
		sp14.transform.localScale= new  Vector3(1,100*spectrum[13],1);

		*/

		}
	}
}