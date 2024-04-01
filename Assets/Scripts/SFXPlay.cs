using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlay : MonoBehaviour
{
	AudioSource source;
	void Awake()
	{
		source = GetComponent<AudioSource>();
	}
	void OnEnable()
	{
		if(source.clip != null)
		{
			source.Play();
			StartCoroutine(nameof(DisableAfterPlay));
		}
	}
	
	private IEnumerator DisableAfterPlay()
	{
		yield return new WaitForSeconds(source.clip.length);
		source.Stop();
		gameObject.transform.position = Vector3.zero;
		gameObject.SetActive(false);
	}
}
