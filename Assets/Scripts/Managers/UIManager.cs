using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	public GameObject photoUI;
	public GameObject BaseUI;
	public GameObject AlbumUI;
	public GameObject AlbumMngr;
	public bool isScenePausable = true;
	public bool anyWindowOpen {get; private set;}
	public GameObject PauseMenu;
	void Start()
	{
		GameManager.instance.uiManager = this;
		anyWindowOpen = false;
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
}
