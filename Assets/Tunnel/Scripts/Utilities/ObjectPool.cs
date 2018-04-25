
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

namespace AppAdvisory.TunnelAndTwist
{
	public class ObjectPool
	{

		public List<GameObject> pooledObjects;

		private GameObject pooledObj;

		private int maxPoolSize;

		public ObjectPool(GameObject obj, int initialPoolSize, int maxPoolSize)
		{

			pooledObjects = new List<GameObject>();


			for (int i = 0; i < initialPoolSize; i++)
			{

				GameObject nObj = GameObject.Instantiate(obj, Vector3.zero, Quaternion.identity) as GameObject;

				nObj.SetActive(false);

				pooledObjects.Add(nObj);

				GameObject.DontDestroyOnLoad(nObj);
			}

			this.maxPoolSize = maxPoolSize;
			this.pooledObj = obj;
		}

		public GameObject GetObject()
		{

			for (int i = 0; i < pooledObjects.Count; i++)
			{

				if (pooledObjects[i].activeSelf == false)
				{

					pooledObjects[i].SetActive(true);

					return pooledObjects[i];
				}
			}

			if (this.maxPoolSize > this.pooledObjects.Count)
			{

				GameObject nObj = GameObject.Instantiate(pooledObj, Vector3.zero, Quaternion.identity) as GameObject;

				nObj.SetActive(true);

				pooledObjects.Add(nObj);

				return nObj;
			}

			return null;
		}

	}
}