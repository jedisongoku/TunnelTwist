
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
	public sealed class ObjectPooling : MonoBehaviorHelper
	{
		/// <summary>
		/// These fields will hold all the different types of assets you wish to pool.
		/// </summary>
		public ObjectPoolClass[] poolingObjects;
		public List<GameObject>[] pooledObjects;

		int defaultPoolAmount = 10;

		void Start () 
		{
			pooledObjects = new List<GameObject>[poolingObjects.Length];

			for(int i=0; i<poolingObjects.Length; i++)
			{
				pooledObjects[i] = new List<GameObject>();

				int poolingAmount;
				if(poolingObjects[i].numberToPreSpawnAtStart > 0) poolingAmount = poolingObjects[i].numberToPreSpawnAtStart;
				else poolingAmount = defaultPoolAmount;

				for(int j=0; j<poolingAmount; j++)
				{
					GameObject newItem = (GameObject) Instantiate(poolingObjects[i].prefab);
					newItem.SetActive(false);
					pooledObjects[i].Add(newItem);
					newItem.transform.parent = transform;
				}
			}
		}

		void OnEnable()
		{
			EventManager.OnReloadSceneEvent += OnReloadSceneEvent;
		}

		void OnDisable()
		{
			EventManager.OnReloadSceneEvent -= OnReloadSceneEvent;
		}

		void OnReloadSceneEvent()
		{
			foreach(Transform t in transform)
			{
				Destroy(t.gameObject);
			}
		}

		public static void Despawn(GameObject myObject)
		{
			myObject.SetActive(false);
		}

		public GameObject Spawn (string itemType)
		{
			GameObject newObject = GetPooledItem(itemType);
			if(newObject != null) {
				newObject.SetActive(true);
				return newObject;
			}
			Debug.Log("Warning: Pool is out of objects.\nTry enabling 'Pool Expand' option.");
			return null;
		}

		public GameObject Spawn (string itemType, Vector3 itemPosition, Quaternion itemRotation)
		{
			GameObject newObject = GetPooledItem(itemType);
			if(newObject != null) {
				newObject.transform.position = itemPosition;
				newObject.transform.rotation = itemRotation;
				newObject.SetActive(true);
				return newObject;
			}
			Debug.Log("Warning: Pool is out of objects.\nTry enabling 'Pool Expand' option.");
			return null;
		}

		public GameObject Spawn (string itemType, Vector3 itemPosition, Quaternion itemRotation, GameObject myParent)
		{

			GameObject newObject = GetPooledItem(itemType);
			if(newObject != null) {
				newObject.transform.position = itemPosition;
				newObject.transform.rotation = itemRotation;
				newObject.transform.parent = myParent.transform;
				newObject.SetActive(true);
				return newObject;
			}
			Debug.Log("Warning: Pool is out of objects.\nTry enabling 'Pool Expand' option.");
			return null;
		}

		public static void PlayEffect(GameObject particleEffect, int particlesAmount)
		{
			if(particleEffect.GetComponent<ParticleSystem>())
			{
				particleEffect.GetComponent<ParticleSystem>().Emit(particlesAmount);
			}
		}

		public static void PlaySound(GameObject soundSource)
		{
			if(soundSource.GetComponent<AudioSource>())
			{
				soundSource.GetComponent<AudioSource>().PlayOneShot(soundSource.GetComponent<AudioSource>().GetComponent<AudioSource>().clip);
			}
		}

		public GameObject GetPooledItem (string itemType)
		{
			for(int i=0; i<poolingObjects.Length; i++)
			{
				if(poolingObjects[i].prefab.name == itemType)
				{
					for(int j=0; j<pooledObjects[i].Count; j++)
					{
						if(!pooledObjects[i][j].activeInHierarchy)
						{
							return pooledObjects[i][j];
						}
					}
					GameObject newItem = (GameObject) Instantiate(poolingObjects[i].prefab);
					newItem.SetActive(false);
					pooledObjects[i].Add(newItem);
					newItem.transform.parent = transform;
					return newItem;
				}
			}

			return null;
		}
	}
}