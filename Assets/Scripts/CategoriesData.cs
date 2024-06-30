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
	[SerializeField] List<string> acceptanceList;
	
	public string GetFullName()
	{
		string text;
		if(showAdjective) text = objectAdjective + " " + objectName;
		else text = objectName;
		return text;
	}
	public string[] GetAcceptance()
	{
		string[] acceptance = new string[acceptanceList.Count];
		for (int i = 0; i < acceptanceList.Count; i++)
		{
			acceptance[i] = acceptanceList[i].ToLower();
		}
		return acceptance;
	}
}
