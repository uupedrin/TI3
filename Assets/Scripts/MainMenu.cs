using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void LoadScene(string scene)
	{
		SceneManager.LoadScene(scene);
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