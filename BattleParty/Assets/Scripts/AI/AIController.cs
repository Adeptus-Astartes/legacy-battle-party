
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AIController : MonoBehaviour 
{
	public bool canControl = false;
	public Owner team;
	public List<Base> underControl;
	public List<Base> enemy;
	public Complexity complexity;

	public float minAction = 2;
	public float maxAction = 5;
	public float nextAction = 0;

	[Space(10)]
	public float minWaitTime = 3;
	public float maxWaitTime = 7;

	public float allUnits;

	public void OnEnable()
	{
		EventManager.OnCapture += CheckCapture;
	}

	public void OnDisable()
	{
		EventManager.OnCapture -= CheckCapture;
	}

	public void Awake()
	{
		foreach(Base myBase in underControl)
		{
			myBase.FindEnemies();
		}
	}

	public void FixedUpdate()
	{
		float tempUnits = 0;
		foreach(Base m_base in underControl)
		{
			tempUnits += m_base.unitCount;
		}
		
		allUnits = tempUnits;
		if(!canControl)
		{
			if(Time.time > nextAction)
			{

				DoAction();
				nextAction = Time.time + Random.Range(minAction,maxAction);
			}
		}
	}

	public void DoAction()
	{
		int dice = Random.Range(0,10);
		switch (complexity)
		{
		case Complexity.Easy:
			if(dice>0 && dice < 3)
				Attack();
			if(dice > 3 && dice < 7)
				Move();
			if(dice > 7)
				Wait();
			break;
		case Complexity.Medium:
			if(dice>0 && dice < 6)
				Attack();
			if(dice > 6 && dice < 8)
				Move();
			if(dice > 8)
				Wait();
			break;
		case Complexity.Hard:
			if(dice>0 && dice < 4)
				Attack();
			if(dice > 4 && dice < 5)
				Move();
			if(dice > 5)
				Wait();
			break;
		}

	}

	public void Attack()
	{
		Base myBase = underControl[Random.Range(0,underControl.Count)];
		Base newEnemy = null;
		if(myBase.enemies.Count > 0)
			newEnemy = myBase.enemies[Random.Range(0,myBase.enemies.Count)];

		if(newEnemy != null)
		{
			myBase.Attack(newEnemy);
		}
	
	}

	public void Move()
	{
		Base myBase = underControl[Random.Range(0,underControl.Count)];
		Base newTarget = null;
		
		if(newTarget != null)
		{
			myBase.Move(newTarget);
		}
	}

	public void Wait()
	{
		nextAction += Random.Range(minWaitTime,maxWaitTime);
	}

	public void CheckCapture(Base capturedBase,Owner newOwner)
	{
		foreach(Base myBase in underControl)
		{
			myBase.FindEnemies();
		}
		if(newOwner == team)
		{
			
			if (newOwner == GameCore.playerTeam)
			{
				//may add level score booster and total booster of gold and score
				int money = 1 + SPlayerPrefs.GetInt ("MONEY");
				SPlayerPrefs.SetInt ("MONEY",money + 100);

				int score = 1 + SPlayerPrefs.GetInt ("SCORE");
				SPlayerPrefs.SetInt ("SCORE", score + 149);
			}

			//Add to undercontrol List
			if(!underControl.Contains(capturedBase))
				underControl.Add(capturedBase);
			//Remove captured base from enemies
			int lenght_a = capturedBase.enemies.Count;
			for(int i = lenght_a - 1; i>=0;i--)
			{
				if(capturedBase.enemies[i].owner == team)
				{
					capturedBase.enemies.Remove(capturedBase.neighbours[i]);
				}
			}
			//Add captured base neighbours to enemy
			int lenght_b = capturedBase.neighbours.Count;
			for(int i = lenght_b - 1; i>=0;i--)
			{
				if(capturedBase.neighbours[i].owner != team)
				{
					if(!capturedBase.enemies.Contains(capturedBase.neighbours[i]))
						capturedBase.enemies.Add(capturedBase.neighbours[i]);
				}
			}
		}
		else
		{
			int lenght_c = underControl.Count;
			for(int i = lenght_c - 1; i>=0;i--)
			{
				if(underControl[i] == capturedBase)
					underControl.RemoveAt(i);
			}
			int lenght_d = capturedBase.neighbours.Count;
			for(int i = lenght_d - 1; i>=0;i--)
			{
				if(underControl.Contains(capturedBase.neighbours[i]))
					enemy.Remove(capturedBase.neighbours[i]);
			}
		}
		if(underControl.Count == 0)
		{
			transform.root.SendMessage("PlayerDefeat",this);
			print("Player " + team.ToString() + " Loose!");
			enabled = false;
		}


	}
}
