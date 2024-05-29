using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashcardManager : MonoBehaviour
{
	public static FlashcardManager instance;
	public RevisionHandler flashcardCanvas;
	public ShowPictureOnScreen pictureScriptHolder;
	public bool interactingWithFlashcards;
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
	private FlashcardsHolder holder = new();
	private IDataService DataService = new JsonDataService();
	
	public void CreateFlashcard(string flashcardName, string path)
	{
		if(!holder.flashcards.ContainsKey(flashcardName))
		{
			DateTime now = DateTime.Now;
			holder.flashcards.Add(flashcardName, new FlashcardInfo()
			{
				name = flashcardName,
				picturePath = path,
				lastReview = now,
				nextReview = now
			});
		}
		else //If already exists, update flashcard
		{
			holder.flashcards[flashcardName].picturePath = path;
		}
		
		SaveFlashcards();
	}
	public void DeleteFlashcard(string flashcardName)
	{
		if(holder.flashcards.ContainsKey(flashcardName))
		{
			holder.flashcards.Remove(flashcardName);
			Debug.Log($"{flashcardName} deleted!");
		}
		else
		{
			Debug.Log($"{flashcardName} does not exist!");
		}
		SaveFlashcards();
	}	
	public Queue<FlashcardInfo> GetCardsToRevise()
	{
		Queue<FlashcardInfo> revise = new();
		foreach (FlashcardInfo card in holder.flashcards.Values)
		{
			DateTime current = DateTime.Now;
			if(card.nextReview < current)
			{
				revise.Enqueue(card);
			}
		}
		
		return revise;
	}
	public List<FlashcardInfo> GetPhotos()
	{
		List<FlashcardInfo> photos = new List<FlashcardInfo>();
		foreach (FlashcardInfo photo in holder.flashcards.Values)
		{
			photos.Add(photo);
		}
		return photos;
	}
	public void UpdateRevisedCard(string cardName, int dificultyUp)
	{
		holder.flashcards[cardName].dificultyScore += dificultyUp;
		holder.flashcards[cardName].lastReview = DateTime.Now;
		holder.flashcards[cardName].nextReview = DateTime.Now.AddHours(2 * holder.flashcards[cardName].dificultyScore/5);
		SaveFlashcards();
	}
	public void ShowScreen()
	{
		flashcardCanvas.gameObject.SetActive(true);
	}
	
	public void ToggleFlashcardInteraction(bool interact)
	{
		if(interact)
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
		else
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
		
		interactingWithFlashcards = interact;
	}
	
	void Start()
	{
		LoadFlashcards();
	}
	public void SaveFlashcards()
	{
		long timeSaving = DateTime.Now.Ticks;
		if(DataService.SaveData("flashcards-data.json", holder))
		{
			timeSaving -= DateTime.Now.Ticks;
			Debug.Log($"Time elapsed to save data: {timeSaving / TimeSpan.TicksPerMillisecond:N4}ms");
		}
		else
		{
			Debug.LogError("Could not save flashcards data!");
		}
	}
	void LoadFlashcards()
	{
		long timeLoading = DateTime.Now.Ticks;
		try
		{
			timeLoading -= DateTime.Now.Ticks;
			Debug.Log($"Time elapsed to load data: {timeLoading / TimeSpan.TicksPerMillisecond:N4}ms");
			FlashcardsHolder data = DataService.LoadData<FlashcardsHolder>("flashcards-data.json");
			holder = data;
		}
		catch (Exception)
		{
			Debug.LogError("Could not load flashcards data!");
		}
	}
}
