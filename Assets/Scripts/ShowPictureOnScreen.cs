using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class ShowPictureOnScreen : MonoBehaviour
{
	public Image cardImage;
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
		//cardImage.material = ShaderManager.instance.shaders[ShaderManager.instance.selected];
		cardImage.gameObject.SetActive(false);
		cardImage.gameObject.SetActive(true);
	}
	void Update()
	{
		cardImage.material = ShaderManager.instance.shaders[ShaderManager.instance.selected];
	}
	void OnDisable()
	{
		if(GameManager.instance.uiManager != null) GameManager.instance.uiManager.ToggleWindowState(false);
		if(FlashcardManager.instance != null) FlashcardManager.instance.ToggleFlashcardInteraction(false);
	}
}
