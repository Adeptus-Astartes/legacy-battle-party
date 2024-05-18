using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MainMenu : MonoBehaviour 
{
	public CanvasGroup mainMenu;
	public CanvasGroup levelSelect;
	public CanvasGroup shop;
	[Header("SoundButton")]
	public Image soundButton;
	public Sprite soundButtonOn;
	public Sprite soundButtonOff;
	public AudioSource clickSound;

	public void Awake()
	{
		if (SPlayerPrefs.GetString ("IsFirstLaunch") == "") 
		{
			SPlayerPrefs.SetInt ("MONEY", 5000);
			SPlayerPrefs.SetInt ("SCORE", 100);

			SPlayerPrefs.SetInt ("IBOOSTER",15);
			SPlayerPrefs.SetInt ("SBOOSTER", 5);
			SPlayerPrefs.SetInt ("DBOOSTER", 3);

			SPlayerPrefs.SetString ("IsFirstLaunch","NO");
		}

		if(SPlayerPrefs.GetFloat("SoundVolume") == 1)
		{
			soundButton.sprite = soundButtonOn;
			AudioListener.volume = 1;
		}
		else
		{
			soundButton.sprite = soundButtonOff;
			AudioListener.volume = 0;
		}
	}

	public void UICallback(string value)
	{
		switch(value)
		{
		case "Click":
			clickSound.Play ();
			break;
		case "Play":
			mainMenu.SendMessage("BackwardTween");
			levelSelect.SendMessage("ForwardTween");
			levelSelect.transform.SetAsLastSibling();
			break;
		case "BackFromLevels":
			mainMenu.SendMessage("ForwardTween");
			levelSelect.SendMessage("BackwardTween");
			mainMenu.transform.SetAsLastSibling();
			break;
		case "Shop":
			mainMenu.SendMessage("BackwardTween");
			shop.SendMessage("ForwardTween");
			shop.transform.SetAsLastSibling();
			break;
		case "BackFromShop":
			mainMenu.SendMessage("ForwardTween");
			shop.SendMessage("BackwardTween");
			mainMenu.transform.SetAsLastSibling();
			break;

		case "Sound":
				if(SPlayerPrefs.GetFloat("SoundVolume") == 1)
				{
					soundButton.sprite = soundButtonOff;
					AudioListener.volume = 0;
				}
				else
				{
					soundButton.sprite = soundButtonOn;
					AudioListener.volume = 1;
				}
			SPlayerPrefs.SetFloat("SoundVolume",AudioListener.volume);
			break;
		}
	}
}
