using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using Unity.VisualScripting;
public class AlbumManager : MonoBehaviour
{
	List<FlashcardInfo> photos = new List<FlashcardInfo>();
	[SerializeField] GameObject [] polaroids;
	[SerializeField] GameObject [] images;
	[SerializeField] TMP_Text [] names;
	int page = 0;
	int pages;
	int lenght;
	public GameObject backButton;
	public GameObject passButton;
	
	void OnEnable() 
	{
		photos = FlashcardManager.instance.GetPhotos();
		lenght = photos.Count;
		pages = lenght/8;
		ShowAlbum();
	}
	void Update()
	{
		if(page == 0)
		{
			backButton.SetActive(false);
		}else
		{
			backButton.SetActive(true);
		}
		if(page == pages)
		{
			passButton.SetActive(false);
		}else
		{
			passButton.SetActive(true);
		}
	}
	void ShowAlbum()
	{
		ClearPage();
		for (int i = 0, p = 0 + page * 8; i < 8 && p < lenght; i++, p++)
		{
			names[i].text = photos[p].name;
			byte[] rawImage = File.ReadAllBytes(photos[p].picturePath);
			Texture2D tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
			tex.LoadImage(rawImage);
			images[i].GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
			images[i].GetComponent<Image>().material = ShaderManager.instance.materialsDic[photos[p].pictureShader];
			polaroids[i].SetActive(true);
			images[i].SetActive(true);
		}
	}
	public void PassPage()
	{
		if(page < pages)
		{
			page++;
			ShowAlbum();
		}
	}
	public void BackPage()
	{
		if(page > 0)
		{
			page--;
			ShowAlbum();
		}
	}
	void ClearPage()
	{
		for(int i = 0; i < 8; i++)
		{
			polaroids[i].SetActive(false);
			images[i].SetActive(false);
			names[i].text = "";
		}
	}
}