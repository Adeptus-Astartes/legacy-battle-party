using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class SkillsUI
{
	public Text incrementInfo;//Time and Multiplier
	public Text speedInfo;//Time and Multiplier
	public Text damageInfo;//Time and Multiplier
	[Space(10)]
	public Text incrementCount;
	public Text speedCount;
	public Text damageCount;
}

public class Skills : MonoBehaviour 
{
	//UI
	public SkillsUI UI;

	//BONUSES
	[HideInInspector]
	public bool activateIUB = false;
	public static float incrementUnitBonus = 1;
	private float incrementUnitMultiplier = 1.5f;
	private float incrementUnitBonusTime = 3;
	private float incrementUnitBonusTempTime = 0;
	private int incrementCount;

	[HideInInspector]
	public bool activateSUB = false;
	public static float speedUnitBonus = 1;
	private float speedUnitMultiplier = 1.5f;
	private float speedUnitBonusTime = 3;
	private float speedUnitBonusTempTime = 0;
	private int speedCount;

	[HideInInspector]
	public bool activateDUB = false;
	public static float damageUnitBonus = 1;
	private float damageUnitMultiplier = 1.5f;
	private float damageUnitBonusTime = 3;
	private float damageUnitBonusTempTime = 0;
	private int damageCount;

	private void Awake()
	{
		LoadUpgrades();
	}

	private void LoadUpgrades()
	{
		incrementCount = SPlayerPrefs.GetInt ("IBOOSTER");
		speedCount = SPlayerPrefs.GetInt ("SBOOSTER");
		damageCount = SPlayerPrefs.GetInt ("DBOOSTER");

		UI.incrementCount.text = incrementCount.ToString ();
		UI.speedCount.text = speedCount.ToString ();
		UI.damageCount.text = damageCount.ToString ();
	}

	bool costil = false;
	public void Increase()
	{
		if (incrementCount - 1 >= 0) 
		{
			incrementCount--;
			SPlayerPrefs.SetInt ("IBOOSTER",incrementCount);
			UI.incrementCount.text = incrementCount.ToString ();
			activateIUB = true;
			incrementUnitBonusTempTime += incrementUnitBonusTime;
		} 
		else
		{
			
		}
	}
	public void IncreaseSpeed()
	{
		if (speedCount - 1 >= 0)
		{
			speedCount--;
			SPlayerPrefs.SetInt ("SBOOSTER",speedCount);
			UI.speedCount.text = speedCount.ToString ();
			costil = true;
			activateSUB = true;
			speedUnitBonusTempTime += speedUnitBonusTime;
		} 
		else 
		{
			
		}
	}
	public void IncreaseDamage()
	{
		if (damageCount - 1 >= 0) 
		{
			damageCount--;
			SPlayerPrefs.SetInt ("DBOOSTER",damageCount);
			activateDUB = true;
			damageUnitBonusTempTime += damageUnitBonusTime;
			UI.damageCount.text = damageCount.ToString ();
		} 
		else
		{
		
		}
	}

	public void Update()
	{
		if(activateIUB)
		{
			if(incrementUnitBonusTempTime > 0)
			{
				incrementUnitBonusTempTime -= Time.deltaTime;
				incrementUnitBonus = incrementUnitMultiplier;

				UI.incrementInfo.text = incrementUnitBonusTempTime.ToString("0.0") + " X " + incrementUnitBonus.ToString("0.0");

			}
			else
			{
				incrementUnitBonus = 1;
				UI.incrementInfo.text = "";
				activateIUB = false;
			}
		}
		if(activateSUB)
		{
			if(speedUnitBonusTempTime > 0)
			{
				speedUnitBonusTempTime -= Time.deltaTime;
				speedUnitBonus = speedUnitMultiplier;

				if(costil)
				{
					EventManager.UseSpeedBonus(speedUnitMultiplier, speedUnitBonusTime,true);
					costil = false;
				}


				UI.speedInfo.text = speedUnitBonusTempTime.ToString("0.0") + " X " + speedUnitBonus.ToString("0.0");
				
			}
			else
			{
				EventManager.UseSpeedBonus(speedUnitMultiplier, speedUnitBonusTime,false);
				speedUnitBonus = 1;
				UI.speedInfo.text = "";
				activateSUB = false;
			}
		}

		if(activateDUB)
		{
			if(damageUnitBonusTempTime > 0)
			{
				damageUnitBonusTempTime -= Time.deltaTime;
				damageUnitBonus = damageUnitMultiplier;
				
				UI.damageInfo.text = damageUnitBonusTempTime.ToString("0.0") + " X " + damageUnitBonus.ToString("0.0");
				
			}
			else
			{
				damageUnitBonus = 1;
				UI.damageInfo.text = "";
				activateDUB = false;
			}
		}
	}

}
