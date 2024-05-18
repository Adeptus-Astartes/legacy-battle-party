using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;

[System.Serializable]
public class InGameUI
{
	[Header("SoundButton")]
	public Image soundButton;
	public Sprite soundButtonOn;
	public Sprite soundButtonOff;

	[Header("PauseSetup")]
	public GameObject pauseButton;
	public GameObject pauseWindow;

	[Header("Defeat")]
	public Text defeatXpReward;
	public Text defeatGdReward;
	public GameObject defeatWindow;

	[Header("Victory")]
	public Text victoryXpReward;
	public Text victoryGdReward;
	public GameObject victoryWindow;
}


[System.Serializable]
public class DefeatPlayer
{
	public string name;
	public Sprite icon;
	public string score;

}
[System.Serializable]
public class DefeatPlayerSetUp
{
	public GameObject defPlayerTab;
	public Text defHeader;
	public Text defScore;
	public Image defPlayerIcon;
}

public class GameMenu : MonoBehaviour 
{
	public SoundManager soundManager;
	[Header("InGameUI")]
	public InGameUI inGameUI;


	[Header("DefeatPlayerSet-Up")]
	public List<DefeatPlayer> defeatPlayerList;
	public DefeatPlayerSetUp defeatPlayerSetUp;


	public void Awake()
	{
		if(SPlayerPrefs.GetFloat("SoundVolume") == 1)
		{
			inGameUI.soundButton.sprite = inGameUI.soundButtonOn;
			AudioListener.volume = 1;
		}
		else
		{
			inGameUI.soundButton.sprite = inGameUI.soundButtonOff;
			AudioListener.volume = 0;
		}
	}

	public void ShowAds()
	{
		ShowRewardedAd();
	}

	public void ShowRewardedAd()
	{
		if (Advertisement.IsReady("rewardedVideo"))
		{
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
		}
	}
	
	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log("The ad was successfully shown.");
			//
			// YOUR CODE TO REWARD THE GAMER
			// Give coins etc.
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}

	
	public void UICallBack(string value)
	{
		if(value == "Sound")
		{
			if(SPlayerPrefs.GetFloat("SoundVolume") == 1)
			{
				inGameUI.soundButton.sprite = inGameUI.soundButtonOff;
				AudioListener.volume = 0;
			}
			else
			{
				inGameUI.soundButton.sprite = inGameUI.soundButtonOn;
				AudioListener.volume = 1;
			}
			SPlayerPrefs.SetFloat("SoundVolume",AudioListener.volume);
		}

		if(value == "Pause")
		{
			Time.timeScale = 0;
			inGameUI.pauseWindow.SendMessage("ForwardTween");
			soundManager.Pause ();
		}
		if(value == "Resume")
		{
			Time.timeScale = 1;
			inGameUI.pauseWindow.SendMessage("BackwardTween");
			soundManager.Resume ();
		}	

		if(value == "Restart")
		{
			Time.timeScale = 1;
			PlayerPrefs.SetInt("LoadScene",Application.loadedLevel);
			Application.LoadLevel("Loading");
		}

		if(value == "Menu")
		{
			Time.timeScale = 1;
			PlayerPrefs.SetInt("LoadScene",0);
			Application.LoadLevel("Loading");
		}
		if(value == "Defeat")
		{
			Time.timeScale = 0;
			inGameUI.defeatXpReward.text = "SCORE + " + SPlayerPrefs.GetInt ("SCORE").ToString(); 
			inGameUI.defeatGdReward.text = "GOLD + " + SPlayerPrefs.GetInt ("MONEY").ToString();
			inGameUI.defeatWindow.SendMessage("ForwardTween");
			soundManager.DefSound ();
			soundManager.Pause ();
		}
		if(value == "Victory")
		{
			Time.timeScale = 0;
			inGameUI.victoryXpReward.text = "SCORE + " + SPlayerPrefs.GetInt ("SCORE").ToString(); 
			inGameUI.victoryGdReward.text = "GOLD + " + SPlayerPrefs.GetInt ("MONEY").ToString();
			inGameUI.victoryWindow.SendMessage("ForwardTween");
			soundManager.WinSound ();
			soundManager.Pause ();
		}
		if(value == "ShowAchievements") 
		{
			
		}
		if(value == "ShowLeader") 
		{

		}
	}

	public void ShowDefeatedPlayer (AIController player) 
	{
		print ("*GanmeMenu* " + player.team.ToString());
	}
}
