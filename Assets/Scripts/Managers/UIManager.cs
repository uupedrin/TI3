using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	static public UIManager uiManager;
	public GameObject photoUI;
	public GameObject BaseUI;
	public GameObject AlbumUI;
	void Start()
	{
		uiManager = this;
	}
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.M))
		{
			if(AlbumUI.active == false)
			{
				AlbumUI.SetActive(true);
			}
			else if(AlbumUI.active == true)
			{
				AlbumUI.SetActive(false);
			}
		}
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
