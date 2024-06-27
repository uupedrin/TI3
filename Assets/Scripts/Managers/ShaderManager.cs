using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class ShaderManager : MonoBehaviour
{
	public static ShaderManager instance;
	public List<Material> shaders;
	public List<Material> allShaders;
	public int selected;
	public Image ShaderHolder;
	public GameObject backButton;
	public GameObject passButton;
	public Dictionary<string, Material> materialsDic;
	
	public void CreateDictionary()
	{
		materialsDic = new();

		for (int i = 0; i < allShaders.Count; i++)
		{
			materialsDic.Add(allShaders[i].name, allShaders[i]);
		}
	}
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
		CreateDictionary();
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
		if (selected == shaders.Count - 1)
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