using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelUIObject : MonoBehaviour 
{
	public Image levelImage;
	public Sprite levelIcon;
	public Sprite lockedLevelIcon;
	public Text record;
	public int levelId;

	public void LoadLevel()
	{
		PlayerPrefs.SetInt("LoadScene",levelId);
		Application.LoadLevel("Loading");
	}
}
