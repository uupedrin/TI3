using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeManagers : MonoBehaviour
{
	[SerializeField] float dayLenghtMinutes;
	[SerializeField] Light pLight;
	[SerializeField] Material sunSet;
	[SerializeField] Material day;
	[SerializeField] Material night;
	Light light;
	float rotationSpeed;
	
	float skyRotation;
	
	private void Awake()
	{
		rotationSpeed = 360/dayLenghtMinutes/60;
		light = pLight.GetComponent<Light>();
	}
	
	void Start ()
	{
		LoadDayTime();
	}
	void Update() 
	{		
		transform.Rotate(new Vector3(1, 0, 0) * rotationSpeed * Time.deltaTime);
		
		if (transform.localEulerAngles.x >= 345 || transform.localEulerAngles.x <= 15 || (transform.localEulerAngles.x >= 165 && transform.localEulerAngles.x <= 195))
		{
			RenderSettings.skybox = sunSet;
			light.color = new Color32(255, 210, 170, 255);
			RenderSettings.fogColor = new Color32(157, 159, 144, 255);
			
			sunSet.SetFloat("_Rotation", skyRotation);
		}
		else if(transform.localEulerAngles.x >= 15 && transform.localEulerAngles.x <= 165)
		{
			RenderSettings.skybox = day;
			light.color = new Color32(255, 251, 220, 255);
			RenderSettings.fogColor = new Color32(135, 209, 250, 255);
			
			day.SetFloat("_Rotation", skyRotation);
		}
		else if (transform.localEulerAngles.x <= 345 && transform.localEulerAngles.x >= 195)
		{
			RenderSettings.skybox = night;
			light.color = new Color32(50, 50, 170, 255);
			light.intensity = 0f;
			RenderSettings.fogColor = new Color32(67, 82, 101, 255);
			
			night.SetFloat("_Rotation", skyRotation);
		}
		
		
	}
	
	private void FixedUpdate() {
		skyRotation += Time.fixedDeltaTime * .25f;
		skyRotation %= 360;
	}
	
	private void OnDestroy() {
		SaveDayTime();
	}
	
	void LoadDayTime()
	{
		try
		{
			float xRotation = PlayerPrefsManager.instance.LoadValueF("DayRotation");
			
			transform.rotation = Quaternion.Euler(xRotation, transform.rotation.y, transform.rotation.z);
		}
		catch (KeyNotFoundException)
		{
			Debug.LogWarning("No day time found! Using default values");
		}
	}
	void SaveDayTime()
	{
		PlayerPrefsManager.instance.SaveValue("DayRotation", transform.localEulerAngles.x);
		Debug.Log("Saving Rotation");
	}
}