#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PhotoTool : MonoBehaviour 
{
	
	private string path;
	public Camera cam;
	
	public string color;
	public Transform unit;
	
	void Start()
	{
		if (path == null)
		{
			SelectDir ();
		}
		
		int angle = 0;
		for (int i = 0; i < 9; i++) 
		{
			Shot ();
			angle += 45;
			unit.localEulerAngles = Vector3.up * angle;
		}
		
		
	}
	
	
	public void Shot()
	{
		int sqr = 512;
		
		RenderTexture tempRT = new RenderTexture(sqr,sqr, 24 );
		
		cam.targetTexture = tempRT;
		cam.Render();
		
		RenderTexture.active = tempRT;
		Texture2D virtualPhoto = new Texture2D(sqr,sqr, TextureFormat.ARGB32, false);
		virtualPhoto.ReadPixels( new Rect(0, 0, sqr,sqr), 0, 0);
		
		RenderTexture.active = null;
		cam.targetTexture = null;
		
		byte[] bytes = virtualPhoto.EncodeToPNG();
		
		File.WriteAllBytes(path + "/"+ unit.name + "_" + color + "_" + unit.transform.localEulerAngles.y + ".png", bytes);
	}
	
	public void SelectDir()
	{
		path = EditorUtility.OpenFolderPanel ("","","");
	}
	

}
#endif
