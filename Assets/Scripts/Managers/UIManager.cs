using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	public GameObject photoUI;
	public GameObject BaseUI;
	public GameObject AlbumUI;
	public GameObject AlbumMngr;
	public bool isScenePausable = true;
	public GameObject PauseMenu;
	void Start()
	{
		GameManager.instance.uiManager = this;
	}
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.M))
		{
			if(AlbumUI.active == false)
			{
				AlbumUI.SetActive(true);
				AlbumMngr.SetActive(true);
				FlashcardManager.instance.ToggleFlashcardInteraction(true);
			}
			else if(AlbumUI.active == true)
			{
				AlbumUI.SetActive(false);
				AlbumMngr.SetActive(false);
				FlashcardManager.instance.ToggleFlashcardInteraction(false);
			}
		}
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(isScenePausable)
			{
				ShowPauseMenu(!PauseMenu.activeInHierarchy);
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
	
	public void ShowPauseMenu(bool show)
	{
		if(PauseMenu!=null)
		{
			PauseMenu.SetActive(show);
			if(show)
			{
				FlashcardManager.instance.ToggleFlashcardInteraction(true);
			}
			else
			{
				FlashcardManager.instance.ToggleFlashcardInteraction(false);
			}
		}
	}
	
	public void LoadScene(string scene)
	{
		SceneManager.LoadScene(scene);
		if(GameManager.instance.isPaused)
		{
			GameManager.instance.PauseGame(false);
		}
	}
	
	public void Quit()
	{
		Application.Quit();
	}
}
