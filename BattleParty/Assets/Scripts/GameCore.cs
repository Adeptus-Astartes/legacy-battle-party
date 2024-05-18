using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum Complexity {Easy, Medium, Hard};

[System.Serializable]
public class Units
{
	public UnitType type;
	public List<UnitSprites> units;
}
[System.Serializable]
public class BuidingSprites
{
	public BaseType type;
	public List<Coloring> colors;
}

[System.Serializable]
public class UnitSprites
{
	public Owner color;
	public Unit unit;
}

[System.Serializable]
public class Coloring
{
	public Owner color;
	public List<Sprite> sprites;
}

public class GameCore : MonoBehaviour 
{
	public static Owner playerTeam;
	public static List<Base> multiplySelectedBases;

	public static Base EnemyTarget;

	public List<Units> units;
	public List<BuidingSprites> building;

	public List<AIController> players;




	public void Start()
	{
		playerTeam = Owner.Red;
		multiplySelectedBases = new List<Base>();

	}

	public static void AddSelectedBase(Base selectedBase)
	{
		if(!multiplySelectedBases.Contains(selectedBase))
		{
			multiplySelectedBases.Add(selectedBase);
		}
	}
	public static void RemoveSelectedBase(Base selectedBase)
	{
		if(multiplySelectedBases.Contains(selectedBase))
		{
			multiplySelectedBases.Remove(selectedBase);
		}
	}

	public void UnselectAll()
	{
		EventManager.Select(null);
		multiplySelectedBases.Clear();
	}
	

	public void AttackBase(Base selectedBase)
	{
		bool canAttack = false;
		if(multiplySelectedBases.Count != 0)
		foreach(Base myBase in multiplySelectedBases)
		{
				if (myBase != selectedBase) 
				{
					foreach(Base neigh in players[0].underControl)
					{
						if (neigh.neighbours.Contains (selectedBase))
							canAttack = true;
					}
					if(canAttack)
						myBase.Attack (selectedBase);
				}

		}

	}


	public static void MoveInBase(Base selectedBase)
	{
		if(multiplySelectedBases.Count != 0)
		{
			
			foreach(Base myBase in multiplySelectedBases)
		    {
				if (myBase != selectedBase) {
					//debug += "\n" + myBase.name.ToString();
					myBase.Move (selectedBase);
				}
		    }
		}
	}

	public void PlayerDefeat(AIController player)
	{
		if(player.canControl)
		{
			this.SendMessage("UICallBack","Defeat");
		}
		else
		{
			players.Remove(player);
			if (players.Count == 1) {
				if (players [0].canControl) {
					this.SendMessage ("UICallBack", "Victory");
				}

			} 
			else 
			{
				this.SendMessage ("ShowDefeatedPlayer",player);
			}
		}
	}


}
