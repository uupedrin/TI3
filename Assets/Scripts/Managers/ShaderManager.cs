using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class ShaderManager : MonoBehaviour
{
	public static ShaderManager instance;
	public Material[] shaders;
	public int selected;
	public Image ShaderHolder;
	public GameObject backButton;
	public GameObject passButton;
	public Dictionary<string, Material> materialsDic;
	void CreateDictionary()
	{
		materialsDic = new();
		for (int i = 0; i < shaders.Length; i++)
		{
			materialsDic.Add(shaders[i].name, shaders[i]);
		}
	}
	void Awake()
	{
		CreateDictionary();
		if(instance != null && instance != this)
		{
			Destroy(this);
		}
		else
		{
			instance = this;
		}
	}
	
	public void Update()
	{
		if (selected == 0)
		{
			backButton.SetActive(false);
		}else
		{
			backButton.SetActive(true);
		}
		if (selected == shaders.Length - 1)
		{
			passButton.SetActive(false);
		}else
		{
			passButton.SetActive(true);
		}
		ShaderHolder.material = shaders[selected];
	}
	public void AddSelected()
	{
		selected++;
	}
	public void SubSelected()
	{
		selected--;
	}
}