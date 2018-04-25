
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


//using UnityEngine;
//using System.Collections.Generic;
//
//public class FindShaderUseAtRuntime : MonoBehaviorHelper
//{
//	public string st = "";
//	public string stArea = "Empty List";
//	public Vector2 scrollPos;
//	public bool exact = false;
//	public List<Material> materialColorChange;
//
//
//
//	public float x = 0;
//	public float y = 0;
//	public float z = 0;
//	public float w = 0;
//
//	void OnEnable()
//	{
//		EventManager.OnChangeCurveX += SetCurvedX;
//		EventManager.OnChangeCurveY += SetCurvedY;
//	}
//
//	void OnDisable()
//	{
//		EventManager.OnChangeCurveX -= SetCurvedX;
//		EventManager.OnChangeCurveY -= SetCurvedY;
//	}
//
//	void Start()
//	{
//		EventManager.DOCurveX(0);
//		EventManager.DOCurveY(0);
//	}
//
//	void SetCurvedX(float x)
//	{
//		if(materialColorChange == null || materialColorChange.Count <= 0)
//			return ;
//
//		this.x = x;
//
//
//		foreach(var m in materialColorChange)
//		{
//			m.SetVector("_QOffset", new Vector4(this.x,this.y,this.z,this.w));
//		}
//	}
//
//	void SetCurvedY(float y)
//	{
//		if(materialColorChange == null || materialColorChange.Count <= 0)
//			return ;
//
//		this.y = y;
//
//
//		foreach(var m in materialColorChange)
//		{
//			m.SetVector("_QOffset", new Vector4(this.x,this.y,this.z,this.w));
//		}
//	}
//
//	void SetCurved(float x, float y, float z, float w)
//	{
//		if(materialColorChange == null || materialColorChange.Count <= 0)
//			return ;
//
//		this.x = x;
//		this.y = y;
//		this.z = y;
//		this.w = y;
//
//		foreach(var m in materialColorChange)
//		{
//			m.SetVector("_QOffset", new Vector4(this.x,this.y,this.z,this.w));
//		}
//	}
//}