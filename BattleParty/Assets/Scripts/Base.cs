using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

using FuckTheSystem;


public enum BaseType{SwordManBuilding,BarbarManBuilging,ArrowManBuilding,ShootingBlockPost};
public enum Owner{Red,Blue,Green,Purple,Black};
public enum UnitType{SwordMan,Barbarian,ArrowMan};

[System.Serializable]
public class BaseUI
{
	public Text count;
	public Image countBackground;
	public Image building;
	public Image hightlight;
}

public abstract class Base : MonoBehaviour, IPointerClickHandler
{
	public GameCore core;
	public RelationOnForces forces;
	[Space(10)]
	public Owner owner;
	public BaseType baseType;
	public List<Base> neighbours;
	public List<Base> enemies;
	[Space(10)]

	public bool selected;
	public int currentLevel = 0;
	public float increaseSpeed = 10;
	public float unitCount = 10;
	public float maxUnitCount = 50;
	public BaseUI ui;


	public AIController controller;

	public UnitType unitType;
	[HideInInspector]
	public Unit unit;

	void OnEnable()
	{
		EventManager.OnClick += OnClick;
		EventManager.OnDoubleClick += OnDoubleClick;
	}
	
	
	void OnDisable()
	{
		EventManager.OnClick -= OnClick;
		EventManager.OnDoubleClick -= OnDoubleClick;
	}

	public void FindEnemies()
	{
		/*
		foreach(AIController m_controller in core.players)
		{
			if (m_controller.team == owner) 
			{
				controller = m_controller;
				break;
			}
		}
		*/

		for(int a = enemies.Count-1; a>= 0;a--)
		{
			if(enemies[a].owner == owner)
			{
				enemies.Remove(enemies[a]);
				//controller.enemy.Remove(enemies[a]);
			}

		}
		for(int b = 0; b<neighbours.Count; b++)
		{
			if(neighbours[b].owner != owner)
			{
				if (!enemies.Contains (neighbours [b])) 
				{
					enemies.Add (neighbours [b]);
					//controller.enemy.Add(neighbours[b]);
				}
			}
		}
	}
	float lastTimeClick = -1;
	int clicksInRow = -1;
	public void OnPointerClick(PointerEventData data)
	{
		/*
		//debug = data.clickCount.ToString();
		if(data.clickCount > 1)
		{
			EventManager.MoveTo(this);
		}
		else
		{
			EventManager.Select(this);
		}
		*/
		float currentTimeClick = data.clickTime;
		if(Mathf.Abs(currentTimeClick - lastTimeClick) < 0.75f){
			clicksInRow++;
			if (clicksInRow >= 2) {
				//debug += "\nDOUBLE CLICK";
				EventManager.MoveTo (this);
			} else {
				EventManager.Select(this);
			}
		}
		else
		{
			clicksInRow = 0;
		}
		lastTimeClick = currentTimeClick;

	}

	public void OnPointerPress(PointerEventData data)
	{
		debug = data.clickCount.ToString();
	}


	public abstract void Attack(Base target);
	public abstract void Move(Base type);
	public abstract void Damage(Unit unit);
	
	private void OnClick(Base selectedBase)
	{
		if(selectedBase != null)
		{
			if(selectedBase == this)
			{
				if(selectedBase.owner == GameCore.playerTeam)
				{
				
					if(!selected)
					{
						GameCore.AddSelectedBase(selectedBase);
						selected = true;
					}
					else
					{	
						GameCore.RemoveSelectedBase(selectedBase);
						selected = false;
					}
				}
				else
				{
					if(GameCore.multiplySelectedBases.Count != 0)
					{
						//					print("I '" + name + "' Attack '" + selectedBase.gameObject.name + "' !");
						core.AttackBase(selectedBase);
						print (selectedBase.name);
					}
				}
		
			}
		}
		else
		{
			selected = false;
		}
		
		ui.hightlight.gameObject.SetActive(selected);
	}
	public static string debug;

	void OnGUI()
	{
		GUI.color = Color.red;
		GUILayout.Label (debug);
	}

	private void OnDoubleClick(Base targetBase)
	{
		if(targetBase != this && targetBase.owner == GameCore.playerTeam)
		{
			if(selected)
			{
				//debug += "\nI '" + name + "' Move To '" + targetBase.gameObject.name + "' !";
				GameCore.MoveInBase(targetBase);
				selected = false;
			}
		}
		else
		{
			selected = false;
		}
		ui.hightlight.gameObject.SetActive(selected);
	}

}
