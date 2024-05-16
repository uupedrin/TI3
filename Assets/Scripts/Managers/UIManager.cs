using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	static public UIManager uiManager;
	public GameObject photoUI;
	public GameObject BaseUI;
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
	public void TurnOnBase()
	{
		BaseUI.SetActive(true);
	}

	public void TurnOffBase()
	{
		BaseUI.SetActive(false);
	}
}
