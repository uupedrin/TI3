using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class ShowPictureOnScreen : MonoBehaviour
{
	[SerializeField] private Image cardImage;
	[SerializeField] private TMP_Text TXT_objectName;
	void Start()
	{
		FlashcardManager.instance.pictureScriptHolder = this;
		gameObject.SetActive(false);
	}
	public void Enable(string name, string picturePath)
	{
		ShowPicture(picturePath);
		TXT_objectName.text = name;
		gameObject.SetActive(true);
		FlashcardManager.instance.ToggleFlashcardInteraction(true);
		GameManager.instance.uiManager.ToggleWindowState(true);
	}
	
	void ShowPicture(string picturePath)
	{
		byte[] rawImage = File.ReadAllBytes(picturePath);
		Texture2D tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
		tex.LoadImage(rawImage);
		cardImage.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
		//cardImage.material.SetTexture(Shader.PropertyToID("_MainTex"),tex);
		cardImage.gameObject.SetActive(false);
		cardImage.gameObject.SetActive(true);
	}
	
	void OnDisable()
	{
		GameManager.instance.uiManager.ToggleWindowState(false);
		FlashcardManager.instance.ToggleFlashcardInteraction(false);
	}
}
