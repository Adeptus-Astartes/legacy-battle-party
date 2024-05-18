using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
	public delegate void ClickAction(Base myBase);
	public static event ClickAction OnClick;
	public static event ClickAction OnDoubleClick;


	public static void Select(Base myBase) 
	{
		if(OnClick != null)
			OnClick(myBase);
	}

	public static void MoveTo(Base target) 
	{
		if(OnDoubleClick != null)
			OnDoubleClick(target);
	}

	public delegate void CaptureBase(Base capturedBase,Owner newOwner);
	public static event CaptureBase OnCapture;

	public static void Capture(Base target,Owner newOwner) 
	{
		if(OnCapture != null)
			OnCapture(target,newOwner);
	}


	public delegate void ChangeSpeedUnit(float speedUnitMultiplier,float speedUnitBonusTime,bool activate);
	public static event ChangeSpeedUnit OnUseSpeedBonus;
	
	public static void UseSpeedBonus(float speedUnitMultiplier,float speedUnitBonusTime,bool activate) 
	{
		if(OnUseSpeedBonus != null)
			OnUseSpeedBonus(speedUnitMultiplier,speedUnitBonusTime,activate);
	}



}
