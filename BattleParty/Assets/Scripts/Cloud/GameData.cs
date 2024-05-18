using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour
{
	//Boosters
	public static int incrementBoosterCount;
	public static int speedBoosterCount;
	public static int damageBoosterCount;

	public static void UseIncrementBoosterCount()
	{
		incrementBoosterCount--;
		SPlayerPrefs.SetInt ("IncrementBoosterCount",incrementBoosterCount);
	}




}
