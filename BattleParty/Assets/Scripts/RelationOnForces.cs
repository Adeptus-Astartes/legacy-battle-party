using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

public class RelationOnForces : MonoBehaviour 
{
	public Transform root;
	public List<AIController> Controllers;
	public List<LayoutElement> elements;
	public Image forceLine;

	/*
	public void Start()
	{
		for(int i = 0; i<allBases.Count;i++)
		{
			GameObject m_forceLine = Instantiate(forceLine.gameObject) as GameObject;
			m_forceLine.transform.SetParent(root);
			elements.Add(m_forceLine.GetComponent<LayoutElement>());
			m_forceLine.GetComponent<Image>().color = OwnerToColor(allBases[i].owner);
		}
	}
	*/

	public void Update()
	{
		for(int i = 0; i<Controllers.Count;i++)
		{
			elements[i].flexibleWidth = Controllers[i].allUnits;
		}
	}

	public Color OwnerToColor(Owner owner)
	{
		switch(owner)
		{
		case Owner.Black:
			return Color.black;
		case Owner.Blue:
			return Color.blue;
		case Owner.Green:
			return Color.green;
		case Owner.Purple:
			return Color.magenta;
		case Owner.Red:
			return Color.red;
		default:
			return Color.white;
		}
	}

}
