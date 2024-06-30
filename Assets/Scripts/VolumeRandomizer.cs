using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class VolumeRandomizer : MonoBehaviour
{
	[SerializeField] bool USE_DEBUG;
	
	[Range(0,1)][SerializeField] float maxVolume;
	[SerializeField] float volumeChangeTime;
	float volume;
	AudioSource source;
	
	float timer = 0;
	
	void Awake()
	{
		source = GetComponent<AudioSource>();
		
		source.volume = maxVolume;
		
		if(USE_DEBUG) 
		{
			volume = maxVolume;
		}
		else
		{
			GetNewVolume();
		}
	}
	
	void Update()
	{
		if(source.volume != volume)	source.volume = Mathf.Lerp(source.volume, volume, Time.deltaTime);
		
		timer += Time.deltaTime;
		if(timer >= volumeChangeTime)
		{
			GetNewVolume();
		}
	}
	
	void GetNewVolume()
	{
		volume = Random.Range(0, maxVolume);
		timer = 0;
	}
}
