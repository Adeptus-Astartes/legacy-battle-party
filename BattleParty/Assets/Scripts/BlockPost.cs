using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class BlockPost : Base 
{
	public Coloring baseColoring;
	public Coloring unitColoring;
	/*
	public void Awake()
	{
		forces.allBases.Add(this);
	}
	*/
	public void Start()
	{
		UpdateColor();
	}

	public void UpdateColor()
	{
//		UnitType m_unitType = BaseToUnit (baseType);
//		foreach(Units units in core.units)
//		{
//			if (units.type == m_unitType) 
//			{
//				foreach()
//				{
//					
//				}
//			}
//		}
		UpdateColors();

		switch(owner)
		{
		case Owner.Red:
			ui.countBackground.color = Color.red;
			break;
		case Owner.Purple:
			ui.countBackground.color = Color.magenta;
			break;
		case Owner.Green:
			ui.countBackground.color = Color.green;
			break;
		case Owner.Blue:
			ui.countBackground.color = Color.blue;
			break;
		case Owner.Black:
			ui.countBackground.color = Color.black;
			break;
		}
	}

	public void UpdateColors()
	{
		foreach(BuidingSprites build in core.building)
		{
			if(build.type == baseType)
			{
				foreach(Coloring color in build.colors)
				{
					if(color.color == owner)
					{
						baseColoring = color;
					}
				}
			}
		}

		foreach(Units unitSpr in core.units)
		{
			if(unitSpr.type == unitType)
			{
				foreach(UnitSprites color in unitSpr.units)
				{
					if(color.color == owner)
					{
						unit = color.unit;
					}
				}
			}
		}

		ui.building.sprite = baseColoring.sprites[currentLevel];
	}

	public void Update()
	{
		IncrementUnits();
		UpdateUI();
	}

	public void IncrementUnits()
	{
		if(unitCount <= maxUnitCount)
		{
			if(owner == GameCore.playerTeam)
			{
				unitCount += increaseSpeed * Skills.incrementUnitBonus * Time.deltaTime;
			}
			else
			{
				unitCount += increaseSpeed * Time.deltaTime;
			}
			
		}
		if(unitCount < 15)
		{
			currentLevel = 0;
			UpdateColors();
		}
		if(unitCount > 15 && unitCount < 30)
		{
			currentLevel = 1;
			UpdateColors();
		}
		if(unitCount > 30)
		{
			currentLevel = 2;
			UpdateColors();
		}
	}

	public void UpdateUI()
	{
		ui.count.text = unitCount.ToString("0");
	}

	public override void Attack(Base target)
	{
		GameObject unitGroup = new GameObject();
		unitGroup.AddComponent<RectTransform>();
		unitGroup.name = "UnitGroup" + baseType.ToString();
		unitGroup.transform.position = transform.position;
		unitGroup.transform.SetParent(this.transform.parent);
		UnitGroup group = unitGroup.AddComponent<UnitGroup>();
		group.scale = GameObject.Find("Canvas").GetComponent<RectTransform>().localScale.x;
		group.unit = unit;
		group.target = target;
		group.team = owner;
		group.unitType = BaseToUnit(baseType);
		group.count = (int)unitCount/2;
		unitCount -= (int)unitCount/2;

	}
	public override void Damage(Unit unit)
	{
		GameCore.RemoveSelectedBase (this);
		if(owner != unit.team)
		{
			float damage = 0;
			switch (unit.type) 
			{
			case UnitType.ArrowMan:
				damage = 1;
				break;
			case UnitType.Barbarian:
				damage = 0.8f;
				break;
			case UnitType.SwordMan:
				damage = 1.3f;
				break;
			default :
				damage = 1;
				break;
			}

			if(unitCount - damage < 0)
			{
				owner = unit.team;
				UpdateColor();
				EventManager.Capture(this,unit.team);

			}
			else
			{
				if (unit.team == GameCore.playerTeam) 
				{
					unitCount -= damage * Skills.damageUnitBonus;
				} 
				else
				{
					unitCount -= damage;
				}
			}
		}
		else
		{
			unitCount ++;
		}
	}

	public override void Move(Base target)
	{
		print ("MOVE");
		GameObject unitGroup = new GameObject();
		unitGroup.AddComponent<RectTransform>();
		unitGroup.name = "UnitGroup" + baseType.ToString();
		unitGroup.transform.position = transform.position;
		unitGroup.transform.SetParent(this.transform.parent);
		UnitGroup group = unitGroup.AddComponent<UnitGroup>();
		group.scale = GameObject.Find("Canvas").GetComponent<RectTransform>().localScale.x;
		group.unit = unit;
		group.target = target;
		group.team = owner;
		group.unitType = BaseToUnit(baseType);
		group.count = (int)unitCount/2;
		unitCount -= (int)unitCount/2;
	}

	public UnitType BaseToUnit(BaseType type)
	{
		switch(type)
		{
		case BaseType.SwordManBuilding:
			return UnitType.SwordMan;
			break;
		case BaseType.BarbarManBuilging:
			return UnitType.Barbarian;
			break;
		case BaseType.ArrowManBuilding:
			return UnitType.ArrowMan;
			break;
		default : 
			return UnitType.SwordMan;
			break;
		}
	}
}
