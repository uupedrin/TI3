using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using Unity.VisualScripting;
using System.Linq;

public class RevisionHandler : MonoBehaviour
{
	[SerializeField] TMP_Text TXT_amountToRevise;
	[SerializeField] TMP_Text TXT_question;
	[SerializeField] TMP_Text TXT_name;
	[SerializeField] Image cardImage;
	[SerializeField] GameObject contentContainer;
	Color defaultQuestionColor;
	[SerializeField] TMP_InputField input;
	[SerializeField] GameObject[] buttonContainer;
	
	Image currentX;
	
	Queue<FlashcardInfo> flashcards;
	FlashcardInfo nextCard;
	
	void Start()
	{
		FlashcardManager.instance.flashcardCanvas = this;
		defaultQuestionColor = TXT_question.color;
		gameObject.SetActive(false);
	}
	
	void OnEnable()
	{
		StartCoroutine(getCards());
		if(GameManager.instance.uiManager != null) GameManager.instance.uiManager.ToggleWindowState(true);
	}
	
	IEnumerator getCards()
	{
		yield return new WaitForSeconds(.3f);
		flashcards = FlashcardManager.instance.GetCardsToRevise();
		HandleRevision();
	}
	
	public void Submit()
	{
		if(input.text.ToLower() == nextCard.name.ToLower() || nextCard.acceptableWords.Contains(input.text.ToLower())) //Correct
		{
			TXT_name.text = $"This is a/an {nextCard.name}";
			TXT_question.text = "CORRECT";
			TXT_question.color = Color.green;
			
			GameManager.instance.AddCoins(3);
			
			AnalyticsSender.instance.CorrectFlashcards++;		
		}
		else
		{
			TXT_name.text = $"This is a/an {nextCard.name}";
			TXT_question.text = "INCORRECT";
			TXT_question.color = Color.red;
			
			AnalyticsSender.instance.IncorrectFlashcards++;			
			AnalyticsSender.instance.AddAnalytics("Revision Handler - Submit | Wrong", "What user Typed", $"User typed: {input.text.ToLower()}, Should have typed {nextCard.name.ToLower()}");
			
		}
		input.text = "";
		SwitchButtons(1);
	}
	
	public void HandlButtonClick(int buttonValue)
	{
		if(buttonValue > 0) //Difficulty
		{
			FlashcardManager.instance.UpdateRevisedCard(nextCard.name,buttonValue);
			Invoke(nameof(CallHandleRevision), .5f);
		}
		else //Next button
		{
			HandleRevision();
		}
	}
	
	void CallHandleRevision()
	{
		currentX.gameObject.SetActive(false);
		HandleRevision();
	}
	
	public void SetCurrentX(Image x)
	{
		currentX = x;
	}
	
	void HandleRevision()
	{
		UpdateTexts();
		SwitchButtons(0);
		if(contentContainer.activeInHierarchy)
		{
			nextCard = flashcards.Dequeue();
			SetCardImage();
		}
	}
	
	void SwitchButtons(int buttonsToShowID)
	{
		for (int i = 0; i < buttonContainer.Length; i++)
		{
			if(i == buttonsToShowID)
			{
				buttonContainer[i].SetActive(true);
			}
			else
			{
				buttonContainer[i].SetActive(false);
			}
		}
		
		switch (buttonsToShowID)
		{
			case 0: //Submit
			input.gameObject.SetActive(true);
			break;
			default:
			input.gameObject.SetActive(false);
			break;
		}
	}
	
	void SetCardImage()
	{
		byte[] rawImage = File.ReadAllBytes(nextCard.picturePath);
		Texture2D tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
		tex.LoadImage(rawImage);
		cardImage.sprite = Sprite.Create(tex, new Rect(0,0, tex.width, tex.height), Vector2.zero);
		cardImage.material = ShaderManager.instance.materialsDic[nextCard.pictureShader];
		//cardImage.material.SetTexture(Shader.PropertyToID("_MainTex"),tex);
		cardImage.gameObject.SetActive(false);
		cardImage.gameObject.SetActive(true);
	}
	/*void Update()
	{
		cardImage.material = cardImage.sprite.GetComponent<SpriteRenderer>().material;
	}*/
	void UpdateTexts()
	{
		if(flashcards == null|| flashcards.Count > 0)
		{
			TXT_amountToRevise.text = $"{flashcards.Count} Flashcards left to revise.";
			contentContainer.SetActive(true);
		}
		else
		{
			TXT_amountToRevise.text = "Congratulations! No flashcards left!";
			contentContainer.SetActive(false);
		}
	}
	
	void OnDisable()
	{
		FlashcardManager.instance.ToggleFlashcardInteraction(false);
		if(GameManager.instance.uiManager != null) GameManager.instance.uiManager.ToggleWindowState(false);
	}
}
