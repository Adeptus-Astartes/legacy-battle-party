using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

public class Loading : MonoBehaviour
{
	int sceneId; 
	
	AsyncOperation menuSync;
	public Transform progressBar;
	
	void Awake()
	{
		sceneId = PlayerPrefs.GetInt ("LoadScene");
	}
	
	IEnumerator Start () 
	{		
		menuSync = Application.LoadLevelAsync(sceneId);
		yield return menuSync;
	}
	
	
	void Update()
	{
		if (!menuSync.isDone && progressBar != null)
		{
			//Debug.Log("MenuProgress:"+menuSync.progress);
			progressBar.Rotate(Vector3.forward * 50 * Time.deltaTime);
		}
		
	}
	
	void Load()
	{
		var async = Application.LoadLevelAsync(sceneId);
		while (!async.isDone) 
		{
			Debug.Log("%: " + async.progress);
			//yield;
		}
		return;
	}
	
	IEnumerator LoadLevelWithProgress (int levelToLoad) 
	{
		var async = Application.LoadLevelAsync(levelToLoad);
		Debug.Log("%: " + async.progress);
		while (!async.isDone) 
		{
			Debug.Log("%: " + async.progress);
			yield return async;
		}
	}
	
	
}
