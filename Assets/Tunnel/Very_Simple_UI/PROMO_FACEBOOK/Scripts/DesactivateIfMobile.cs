using UnityEngine;
using System.Collections;

/// <summary>
/// Attached to GameObject we want to desactivate at star if we run the game on mobile.
/// </summary>
public class DesactivateIfMobile : MonoBehaviour 
{
	void OnEnable()
	{
		if(Application.isMobilePlatform)
			gameObject.SetActive(false);
	}
}
