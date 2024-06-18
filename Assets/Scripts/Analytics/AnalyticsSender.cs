using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using UnityEngine;

public class AnalyticsSender : MonoBehaviour
{
	public bool ENABLE_ANALYTICS = true;
	
	public bool firstPhotoTaken = false;
	public bool firstFlashcardDone = false;
	public bool GotGameMechanics = false;
	public float lastPhotoTaken;
	public int CorrectFlashcards = 0;
	public int IncorrectFlashcards = 0;
	
	public List<AnalyticsData> data;
	public static AnalyticsSender instance {get; private set;}
	private void Awake()
	{
		if(instance == null && instance != this)
		{
			instance = this;
			data = new List<AnalyticsData>();
			lastPhotoTaken = Time.time;
			transform.parent = null;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
	public void AddAnalytics(string sender, string track, string value)
	{
		AnalyticsData d = new AnalyticsData(Time.time, sender, track, value);
		Debug.Log($"Send: {d.sender}, Track: {d.track}, Value: {d.value}");
		data.Add(d);
	}
	public void Save()
	{
		if(ENABLE_ANALYTICS == false) return;
		AnalyticsFile f = new();
		f.data = data.ToArray();
		string json = JsonUtility.ToJson(f, true);
		SaveFile(json);
		SendEmail(json);
	}
	
	void SaveFile(string text)
	{
		string path = Application.dataPath + "/snap_n_learn_analytics.txt";
		Debug.Log($"File stored in {path}");
		File.WriteAllText(path, text);
	}
	void SendEmail(string text)
	{
		var client = new SmtpClient("smtp.gmail.com", 587)
		{
			Credentials = new NetworkCredential("pedroveiga.ribeiro@gmail.com", "oqvy opts nhoz ndny"),
			EnableSsl = true	
		};
		client.Send("pedroveiga.ribeiro@gmail.com", "Pedroveigar9@gmail.com", "Snap & Learn - Analytics", text);
		Debug.Log("Email Enviado");
	}
	
	public string FlashcardSuccessRate()
	{
		float ratio = CorrectFlashcards/(CorrectFlashcards + IncorrectFlashcards);
		return ratio.ToString();
	}

}
