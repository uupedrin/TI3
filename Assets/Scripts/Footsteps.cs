using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Rigidbody))]
public class Footsteps : MonoBehaviour
{
	[SerializeField] float soundRate;
	bool canPlay = true;
	
	void Update()
	{
		float horizontalInput = Input.GetAxisRaw("Horizontal");
		float verticalInput = Input.GetAxisRaw("Vertical");
		if((horizontalInput != 0 || verticalInput != 0) && canPlay)
		{
			StartCoroutine(nameof(WalkSound));
		}
	}
	
	IEnumerator WalkSound()
	{
		canPlay = false;
		int sound = Random.Range(1,21);
		AudioManager.instance.TryPlay2DEffect(sound);
		yield return new WaitForSeconds(soundRate);
		canPlay = true;
	}
}
