using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class ShaderManager : MonoBehaviour
{
	public Material[] shaders;
	public int selected;
	public Image ShaderHolder;
	public GameObject backButton;
	public GameObject passButton;
	
	public void Update()
	{
		if (selected == 0)
		{
			backButton.SetActive(false);
		}else
		{
			backButton.SetActive(true);
		}
		if (selected == shaders.Length )
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