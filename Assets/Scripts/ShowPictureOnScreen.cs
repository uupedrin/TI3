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
	}
	
	void ShowPicture(string picturePath)
	{
		byte[] rawImage = File.ReadAllBytes(picturePath);
		Texture2D tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
		tex.LoadImage(rawImage);
		cardImage.material.SetTexture(Shader.PropertyToID("_MainTex"),tex);
		cardImage.gameObject.SetActive(false);
		cardImage.gameObject.SetActive(true);
	}
	
	void OnDisable()
	{
		FlashcardManager.instance.ToggleFlashcardInteraction(false);
	}
}
