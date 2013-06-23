using UnityEngine;
using System.Collections;
using System;

public class AndroidComBase : MonoBehaviour 
{
	string resultPrint = "ASKY";
	bool clicked = false;
	GUIStyle labelStyle = new GUIStyle();
	
	void Start()
	{
		labelStyle.fontSize = 30;
		labelStyle.normal.textColor = Color.white;
	}
	
	void OnGUI() {
        if (GUI.Button(new Rect(10, 10, 300, 100), "HelloAndroid"))
		{
			CallAndroid();
		}
        GUI.Label (new Rect (10, 200, 300, 20), resultPrint, labelStyle);
    }
	
	/// <summery>
	/// Android Class를 호출하는 method
	/// </summery>
	void CallAndroid()
	{
		if( clicked || Application.platform != RuntimePlatform.Android) 
		{
			Debug.Log("Do not Execute this method");
			return;
		}
		clicked = true;
		resultPrint = "";
					
#if UNITY_ANDROID
		// try, catch부분이 핵심인데...
		// 간단히 말해서 unity가 Android Class 중 com.unity3d.player.UnityPlayer 를 부른다.
		// 부른 그놈(jc)의 반(class)에가서 '너네 반에 currentActivity 란 애 나와!'라고 외친다.
		// 그때 튀어나온 애를 jo가 기억하게 만든다.
		// 그리고 jo에게 말한다.
		// unity 왈 "야 AnswerToUnity 해봐!"
		try
		{
			using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
				{
					Debug.Log("CallAndroid"); 
					
					jo.Call("AnswerToUnity");
					
				}
			}
		}
		catch (Exception e)
		{
			Debug.Log(e.StackTrace);
		}
#endif
		
	}
	
	/// <summery>
	/// Android Class가 Unity에 값을 전달 할 때 사용하는 Method
	/// </summery>
	void ResultFromAndroid(string result)
	{
		clicked = false;
		resultPrint = result;
	}
	
}
