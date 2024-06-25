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
	void Start ()
	{
		rotationSpeed = 360/dayLenghtMinutes/60;
		light = pLight.GetComponent<Light>();
	}
	void Update() 
	{		
		transform.Rotate(new Vector3(-1, 0, 0) * rotationSpeed * Time.deltaTime);
		
		if ((transform.localEulerAngles.x >= 150 && transform.localEulerAngles.x <= 180) || (transform.localEulerAngles.x <= 30 && transform.localEulerAngles.x >= 0))
		{
			RenderSettings.skybox = sunSet;
			light.color = new Color32(255, 210, 170, 255);
		}
		else if(transform.localEulerAngles.x >= 30 && transform.localEulerAngles.x <= 150)
		{
			RenderSettings.skybox = day;
			light.color = new Color32(255, 250, 170, 255);
		}
		else if (transform.localEulerAngles.x <= 360 && transform.localEulerAngles.x >= 180)
		{
			RenderSettings.skybox = night;
			light.color = new Color32(50, 50, 170, 255);
			light.intensity = 0.5f;
		}
	}
}