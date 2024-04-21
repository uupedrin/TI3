using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	static public UIManager uiManager;
	public GameObject photoUI;
	void Start()
	{
		uiManager = this;
	}

	public void TurnOnPhoto()
	{
		photoUI.SetActive(true);
	}

	public void TurnOffPhoto()
	{
		photoUI.SetActive(false);
	}
}
