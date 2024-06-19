using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public Slider[] audioSliders;
	
	private void Start()
	{
		SetupComponents();
	}
	
	public void LoadScene(string scene)
	{
		SceneManager.LoadScene(scene);
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
	
	public void Quit()
	{
		AnalyticsSender.instance.AddAnalytics("UIManager - Quit", "Flashcard success rate", AnalyticsSender.instance.FlashcardSuccessRate());
		AnalyticsSender.instance.AddAnalytics("UIManager - Quit", "Time to close the game", "Time");
		AnalyticsSender.instance.Save();
		
		Debug.Log("Quit!");
		
		Application.Quit();
	}
}