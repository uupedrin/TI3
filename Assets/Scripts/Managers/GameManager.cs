using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public UIManager uiManager;
	public int coins = 0;
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
		//transform.SetParent(null);
		DontDestroyOnLoad(gameObject);
	}
	
	private void Start()
	{
		GetPlayerCoins();
	}
	
	void GetPlayerCoins()
	{
		try
		{
			coins = PlayerPrefsManager.instance.LoadValueI("coins");
			uiManager.UpdateCoins();
		}
		catch (KeyNotFoundException)
		{
			coins = 0;
			PlayerPrefsManager.instance.SaveValue("coins", coins);
			uiManager.UpdateCoins();
		}
	}
	
	public void PauseGame(bool pauseState)
	{
		if(uiManager!=null && uiManager.isScenePausable && PlayerCam.instance.photoMode == false)
		{
			isPaused = pauseState;
			if(isPaused && uiManager != null && PlayerCam.instance.photoMode == false)
			{
				uiManager.ShowPauseMenu(pauseState);
			}
		}
	}
	
	public void AddCoins(int value)
	{
		coins += value;
		coins = Math.Clamp(coins, 0, 9999);
		PlayerPrefsManager.instance.SaveValue("coins", coins);
		
		uiManager.UpdateCoins();
	}
	public void RemoveCoins(int value)
	{
		AddCoins(-value);
		uiManager.UpdateCoins();
	}
	public void CoinCheat()
	{
		AddCoins(200);
		uiManager.UpdateCoins();
	}
}
