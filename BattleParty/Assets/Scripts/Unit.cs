using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
	public Base targetBase;
	public UnitType type;
	public Owner team;
	public float scale;
	public float speed;
	public float minDistance;
	public float fadeDistance;

	public w2dp_Agent agent;
	public int currentWaypoint;

	public Image unitImage;
	public Image unitShadow;
	public float alpha = 0;
	public AnimationCurve movementAnimation;

	[Header("Unit")]

	public List<Sprite> sprites;
	
	public Vector2 direction;

	public float xOff;
	public float yOff;

	float liveTime;


	//statements
	public bool born = true;
	public bool death = false;

	public void Start()
	{
		unitImage.rectTransform.sizeDelta *= scale;
		unitImage.sprite = sprites[0];
		if (unitShadow)
		{
			unitShadow.rectTransform.sizeDelta *= scale;
			unitShadow.transform.localPosition = new Vector2 (xOff, yOff-3);
		}
		if(team == GameCore.playerTeam)
			agent.AgentSpeed = speed * scale * Skills.speedUnitBonus;
		else
			agent.AgentSpeed = speed * scale;

		agent.Move(transform.position,targetBase.transform.position);
	}

	//Sign on speed bonus
	public void OnEnable()
	{
		if(team == GameCore.playerTeam)
		{
			EventManager.OnUseSpeedBonus += ActivateSpeedBonus;
		}
	}

	public void OnDisable()
	{
		if(team == GameCore.playerTeam)
		{
			EventManager.OnUseSpeedBonus -= ActivateSpeedBonus;
		}
	}

	public void ActivateSpeedBonus(float speedUnitMultiplier,float speedUnitBonusTime,bool activate)
	{
		if(activate)
		{
			print(Skills.speedUnitBonus);
			agent.AgentSpeed = speed * scale * Skills.speedUnitBonus;
			agent.Move(transform.position,targetBase.transform.position);
		}
		else
		{
			agent.AgentSpeed = speed * scale;
			agent.Move(transform.position,targetBase.transform.position);
		}
	}

	public void FixedUpdate ()
	{
		if(born)
		{
			alpha += Time.fixedDeltaTime;
			unitImage.color = new Color(1,1,1,alpha);
			if (unitShadow)
				unitShadow.color = new Color (1, 1, 1, alpha / 2);
			if(alpha > 1)
				born = false;
		}
		if(death)
		{
			alpha -= Time.fixedDeltaTime;
			unitImage.color = new Color(1,1,1,alpha);
			if (unitShadow)
				unitShadow.color = new Color (1, 1, 1, alpha / 2);
			if(alpha < 0)
				death = false;
		}
		liveTime += Time.fixedDeltaTime;
		unitImage.transform.localPosition = new Vector2(xOff,yOff + movementAnimation.Evaluate(liveTime) * 3);

		// Gets a vector that points from the player's position to the target's.
		var heading = targetBase.transform.position - transform.position;
		var distance = heading.magnitude;
		var direction = heading / distance; // This is now the normalized direction.

		if (heading.sqrMagnitude < fadeDistance * fadeDistance) 
		{
			death = true;
		}

		if (heading.sqrMagnitude < minDistance * minDistance) 
		{
			targetBase.Damage(this);
			Destroy(gameObject);
		}

		RotateUnit();
	}

	public void RotateUnit()
	{
		if(FloatInRange(-0.5f,0.5f,direction.x) && FloatInRange(0.5f,1f,direction.y))
		{
			unitImage.sprite = sprites[0];
		}
		if(FloatInRange(0.5f,1f,direction.x) && FloatInRange(0.5f,1f,direction.y))
		{
			unitImage.sprite = sprites[1];
		}
		if(FloatInRange(0.5f,1f,direction.x) && FloatInRange(-0.5f,0.5f,direction.y))
		{
			unitImage.sprite = sprites[2];
		}
		if(FloatInRange(0.5f,1f,direction.x) && FloatInRange(-1f,-0.5f,direction.y))
		{
			unitImage.sprite = sprites[3];
		}
		if(FloatInRange(-0.5f,0.5f,direction.x) && FloatInRange(-1f,-0.5f,direction.y))
		{
			unitImage.sprite = sprites[4];
		}
		if(FloatInRange(-1f,-0.5f,direction.x) && FloatInRange(-1f,-0.5f,direction.y))
		{
			unitImage.sprite = sprites[5];
		}
		if(FloatInRange(-1f,-0.5f,direction.x) && FloatInRange(-0.5f,0.5f,direction.y))
		{
			unitImage.sprite = sprites[6];
		}
		if(FloatInRange(-1f,-0.5f,direction.x) && FloatInRange(0.5f,1f,direction.y))
		{
			unitImage.sprite = sprites[7];
		}
	}

	public bool FloatInRange(float a, float b,float value)
	{
		if(value > a && value < b)
			return true;
		else
			return false;
	}
}
