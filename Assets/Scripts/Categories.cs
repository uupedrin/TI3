using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(Outliner))]
public class Categories : MonoBehaviour
{
	public string objectName;
	public string objectColor;
	[HideInInspector] public string fullName;
	[SerializeField] bool showColor;
	StringBuilder sb = new StringBuilder();

	void Start()
	{
		if(showColor) sb.Append(objectColor + " ");
		sb.Append(objectName);
		fullName = sb.ToString();
	}
}
