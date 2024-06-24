using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public GameObject photoUI;
	public GameObject BaseUI;
	public GameObject AlbumUI;
	public GameObject AlbumMngr;
	public bool isScenePausable = true;
	public bool anyWindowOpen {get; private set;}
	public GameObject PauseMenu;
	[SerializeField] TMP_Text coinTxt;
	public string cardName;
	
	public Slider[] audioSliders;
	
	void Start()
	{
		GameManager.instance.uiManager = this;
		anyWindowOpen = false;
		
		SetupComponents();
	}
	
	void SetupComponents()
	{
		try
		{
			float master = PlayerPrefsManager.instance.LoadValueF("MasterVol");
			float music = PlayerPrefsManager.instance.LoadValueF("MusicVol");
			float sfx = PlayerPrefsManager.instance.LoadValueF("SFXVol");
			
			audioSliders[0].value = master;
			audioSliders[1].value = music;
			audioSliders[2].value = sfx;
		}
		catch (KeyNotFoundException)
		{
			PlayerPrefsManager.instance.SaveValue("MasterVol", 0);
			PlayerPrefsManager.instance.SaveValue("MusicVol", 0);
			PlayerPrefsManager.instance.SaveValue("SFXVol", 0);
		}
	}
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(isScenePausable)
			{
				ShowPauseMenu(!PauseMenu.activeInHierarchy);
			}
		}
		
		if(Input.GetKeyDown(KeyCode.M))
		{
			if(GameManager.instance.isPaused) return;
			if(anyWindowOpen) return;
			if(AlbumUI.active == false)
			{
				AlbumUI.SetActive(true);
				AlbumMngr.SetActive(true);
				ToggleWindowState(true);
				FlashcardManager.instance.ToggleFlashcardInteraction(true);
			}
			else if(AlbumUI.active == true)
			{
				AlbumUI.SetActive(false);
				AlbumMngr.SetActive(false);
				ToggleWindowState(true);
				FlashcardManager.instance.ToggleFlashcardInteraction(false);
			}
		}
		
		if(Input.GetKeyDown(KeyCode.F7))
		{
			GameManager.instance.CoinCheat();
		}
		
		if(Input.GetKeyDown(KeyCode.F8))
		{
			FlashcardManager.instance.DeleteFlashcardData();
		}
	}
	
	public void CloseAlbum()
	{
		AlbumUI.SetActive(false);
		AlbumMngr.SetActive(false);
		FlashcardManager.instance.ToggleFlashcardInteraction(false);
		ToggleWindowState(false);
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
				if(!anyWindowOpen) FlashcardManager.instance.ToggleFlashcardInteraction(false);
			}
		}
	}
	
	public void ToggleWindowState(bool windowOpenState)
	{
		anyWindowOpen = windowOpenState;
	}
	
	public void HandleVolumeSliders()
	{
		float master = audioSliders[0].value;
		float music = audioSliders[1].value;
		float sfx = audioSliders[2].value;
		PlayerPrefsManager.instance.SaveValue("MasterVol", master);
		PlayerPrefsManager.instance.SaveValue("MusicVol", music);
		PlayerPrefsManager.instance.SaveValue("SFXVol", sfx);
		Vector3 audio = new Vector3(master,music,sfx);
		AudioManager.instance.SetMixer(audio);
	}
	
	public void LoadScene(string scene)
	{
		ToggleWindowState(false);
		SceneManager.LoadScene(scene);
		if(GameManager.instance.isPaused)
		{
			GameManager.instance.PauseGame(false);
		}
	}
	
	public void Quit()
	{
		AnalyticsSender.instance.AddAnalytics("UIManager - Quit", "Flashcard success rate", AnalyticsSender.instance.FlashcardSuccessRate());
		AnalyticsSender.instance.AddAnalytics("UIManager - Quit", "Time to close the game", "Time");
		AnalyticsSender.instance.Save();
		
		Debug.Log("Quit!");
		Application.Quit();
	}
	public void UpdateCoins()
	{
		coinTxt.text = GameManager.instance.coins.ToString();
	}
	public void HandleClose()
	{ 
		FlashcardManager.instance.UpdateFlashcardJson(cardName, FlashcardManager.instance.pictureScriptHolder.cardImage.material.name);
	}
}
