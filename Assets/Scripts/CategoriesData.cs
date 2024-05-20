using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Prefab Details", menuName = "Details", order = 0)]
public class CategoriesData : ScriptableObject
{
	public string objectName;
	public string objectColor;
	[HideInInspector] public string fullName;
	[SerializeField] bool showColor;

	public string GetFullName()
	{
		return fullName = objectColor + " " + objectName;
	}
}
