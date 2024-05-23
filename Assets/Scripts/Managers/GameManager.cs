using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public UIManager uiManager;
	public bool isPaused {get; private set;}
	
	void Awake()
	{
		if(instance != null && instance != this)
		{
			Destroy(this);
		}
		else
		{
			instance = this;
		}
		transform.SetParent(null);
		DontDestroyOnLoad(gameObject);
	}
	
	public void PauseGame(bool pauseState)
	{
		if(uiManager!=null && uiManager.isScenePausable)
		{
			isPaused = pauseState;
			if(isPaused && uiManager != null)
			{
				uiManager.ShowPauseMenu(pauseState);
			}
		}
	}
}
