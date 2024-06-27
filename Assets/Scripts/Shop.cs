using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
	const int COST = 25;
	
	public static Shop instance;
	public List<Material> shopShaders;
	[SerializeField] GameObject ShopCanvas;
	[SerializeField] Button buyButton;
	
	GameObject selected;
	
	void Awake()
	{
		if(instance == null && instance != this)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		
	}
	
	void Start()
	{
		ShopCanvas.SetActive(false);
	}
	
	void Update()
	{
		VerifyMoney();
	}
	
	public void OpenShop()
	{
		ShopCanvas.SetActive(true);
		FlashcardManager.instance.ToggleFlashcardInteraction(true);
		GameManager.instance.uiManager.ToggleWindowState(true);
	}
	public void CloseShop()
	{
		ShopCanvas.SetActive(false);
		FlashcardManager.instance.ToggleFlashcardInteraction(false);
		GameManager.instance.uiManager.ToggleWindowState(false);
	}
	
	public void Select(GameObject toSelect)
	{
		if(selected != null) selected.GetComponent<Outline>().enabled = false;
		selected = toSelect;
		selected.GetComponent<Outline>().enabled = true;
	}
	
	public void Buy(int shaderNumber)
	{
		shaderNumber = Mathf.Clamp(shaderNumber, 0, shopShaders.Count);
		ShaderManager.instance.shaders.Add(shopShaders[shaderNumber]);		
	}
	
	public void BuySelected()
	{
		Buy(selected.GetComponent<ShopItem>().Unlock());
		GameManager.instance.RemoveCoins(COST);
	}
	
	void VerifyMoney()
	{
		if(GameManager.instance.coins < COST || selected == null)
		{
			buyButton.interactable = false;
		}
		else
		{
			buyButton.interactable = true;
		}
	}
}
