using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UnitGroup : MonoBehaviour 
{
	public Unit unit;
	public UnitType unitType;
	public Owner team;
	public float scale;
	public float minDistance = 30;
	public Base target;

	public int count = 10;
	public int spawned = 0;
	public bool spawn = true;
	public float spawnInterval = 0.5f;

	void Update ()
	{
		if(spawn)
		{
			StartCoroutine(Spawn());
			spawn = false;
		}
	}

	public IEnumerator Spawn()
	{	
		for(int a = 0;a<(spawned/3)+1;a--)
		{
			for(int b = 3;b>0;b--)
			{
				if (spawned + 1 <= count) {
					GameObject unitObject = Instantiate (unit.gameObject, transform.position, Quaternion.identity) as GameObject;
					unitObject.name = a.ToString () + b.ToString ();
					if(GameObject.FindGameObjectWithTag ("Root").transform)
				    	unitObject.transform.SetParent (GameObject.FindGameObjectWithTag ("Root").transform);
					Unit spawnedUnit = unitObject.GetComponent<Unit> ();
					spawnedUnit.scale = scale;
					spawnedUnit.xOff = Random.Range (-10, 10);
					spawnedUnit.yOff = Random.Range (-10, 10);
					spawnedUnit.targetBase = target;
					spawnedUnit.team = team;
					spawned++;

				} else {
					Destroy(gameObject);
				}
			}
			yield return new WaitForSeconds(spawnInterval);
		
		}
		yield return null;
	}
}
