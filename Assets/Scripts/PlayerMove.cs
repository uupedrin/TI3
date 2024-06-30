using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	float moveX;
	float moveY;
	Rigidbody body;
	Vector3 direction;
	[SerializeField] float moveForce;
	[SerializeField] float maxSpeed;
	[SerializeField] Transform orientation;
	
	Vector3 startPos;

	//Mobile
	public FixedJoystick joystick;
	public GameObject joystickSprite;

	void Start()
	{
		
		body = GetComponent<Rigidbody>();
		joystickSprite.SetActive(false);
		#if UNITY_ANDROID
		joystickSprite.SetActive(true);
		#endif
		
		LoadPlayerPosition();
		InvokeRepeating(nameof(SavePlayerPosition), 5f, 5f);
	}

	public void SavePlayerPosition()
	{
		PlayerPrefsManager.instance.SaveValue("PlayerPosX", transform.position.x);
		PlayerPrefsManager.instance.SaveValue("PlayerPosY", transform.position.y);
		PlayerPrefsManager.instance.SaveValue("PlayerPosZ", transform.position.z);
		Debug.Log("Saving");
	}
	public void LoadPlayerPosition()
	{
		try
		{
			float x = PlayerPrefsManager.instance.LoadValueF("PlayerPosX");
			float y = PlayerPrefsManager.instance.LoadValueF("PlayerPosY");
			float z = PlayerPrefsManager.instance.LoadValueF("PlayerPosZ");
			
			startPos = new Vector3(x,y,z);
			Debug.Log("Found");
		}
		catch (KeyNotFoundException)
		{
			startPos = transform.position;
		}
		transform.position = startPos;
	}
	
	void FixedUpdate()
	{
		if(FlashcardManager.instance.interactingWithFlashcards) return;
		moveX = Input.GetAxisRaw("Horizontal");
		moveY = Input.GetAxisRaw("Vertical");



		#if UNITY_ANDROID
		direction = orientation.forward * joystick.Vertical + orientation.right * joystick.Horizontal;
		#else

		direction = orientation.forward * moveY + orientation.right * moveX;
		#endif

		body.AddForce(direction.normalized * moveForce);
		if(body.velocity.magnitude > maxSpeed) body.velocity = Vector3.ClampMagnitude(body.velocity, maxSpeed);
	}
	
	private void OnDestroy()
	{
		SavePlayerPosition();
	}
}
