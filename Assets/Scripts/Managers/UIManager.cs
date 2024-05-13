using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	public GameObject photoUI;
	public bool shouldLockCursor = false;
	void Start()
	{
		GameManager.instance.uiManager = this;
	}

	public void TurnOnPhoto()
	{
		photoUI.SetActive(true);
	}

	public void TurnOffPhoto()
	{
		photoUI.SetActive(false);
	}
	
	public void PauseGame()
	{
		bool isPaused = GameManager.instance.isPaused;
		if(shouldLockCursor)
		{
			Cursor.visible = !isPaused;
			if(isPaused)
			{
				Cursor.lockState = CursorLockMode.None;
			}
			else
			{
				Cursor.lockState = CursorLockMode.Locked;
			}
		}
	}
	
	public void ChangeScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}
	
	public void QuitGame()
	{
		Application.Quit();
	}
}
