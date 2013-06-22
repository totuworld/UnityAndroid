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
	
	void ResultFromAndroid(string result)
	{
		clicked = false;
		resultPrint = result;
	}
	
}
