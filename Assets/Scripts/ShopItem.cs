using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
	public int ShaderId;
	bool unlocked = false;
	
	void Start()
	{
		try
		{
			unlocked = PlayerPrefsManager.instance.LoadValueB($"HasShaderID0{ShaderId}");
		}
		catch (KeyNotFoundException)
		{
			unlocked = false;
			PlayerPrefsManager.instance.SaveValue($"HasShaderID0{ShaderId}", false);
		}
		
		if(unlocked)
		{
			Shop.instance.Buy(ShaderId);
			gameObject.SetActive(false);
		}
	}
	
	public int Unlock()
	{
		unlocked = true;
		PlayerPrefsManager.instance.SaveValue($"HasShaderID0{ShaderId}", unlocked);
		gameObject.SetActive(false);
		return ShaderId;
	}
}
