using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Prefab Details", menuName = "Details", order = 0)]
public class CategoriesData : ScriptableObject
{
	public string objectName;
	public string objectAdjective;
	[HideInInspector] public string fullName;
	[SerializeField] bool showAdjective;

	public string GetFullName()
	{
		string text;
		if(showAdjective) text = objectAdjective + " " + objectName;
		else text = objectName;
		return text;
	}
}
