using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
	[SerializeField] Transform player;
	[SerializeField] float senseX;
	[SerializeField] float senseY;
	[SerializeField] Transform orientation;
	public Vector2 LockAxis;
	float xRotation;
	float yRotation;
	[SerializeField]float zoom;
	float baseZoom = 60;
	float maxZoom = 25;
	float minZoom = 50;
	[SerializeField] float zoomSpeed;
	[SerializeField] SnapCam snapCam;
	[SerializeField] bool canPhoto;
	[SerializeField] bool photoMode = false;
	RaycastHit hit;
	RaycastHit ray;
	bool firstRay;
	bool secondRay;
	int layerMask = 1<<6;
	int layerMask2 = ~20;
	public string photoName;
	
	Collider previous;
	
	void Start()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	void FixedUpdate()
	{
		if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask))
		{
			Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
			firstRay = true;
		}
		else
		{
			Debug.DrawRay(transform.position, transform.forward * 1000f, Color.red);
			canPhoto = false;
			firstRay = false;
		}
		if (Physics.Raycast(transform.position, transform.forward, out ray, Mathf.Infinity, layerMask2))
		{
			Debug.DrawRay(transform.position, transform.forward * ray.distance, Color.yellow);
			secondRay = true;
		}
		else
		{
			Debug.DrawRay(transform.position, transform.forward * 1000f, Color.red);
			canPhoto = false;
			secondRay = false;
		}
		//OUTLINE
		if(photoMode && canPhoto) 
		{
			// if(previous != ray.collider)
			// {
			// 	if(previous != null) previous.GetComponent<Outliner>().DisableOutline();
			// 	previous = ray.collider;
			// }
			// ray.collider.GetComponent<Outliner>().EnableOutline();
		}
		else if(photoMode && !canPhoto)
		{
			//if(previous != null) previous.GetComponent<Outliner>().DisableOutline();
		}
	}
	private void Update()
	{		
		if(FlashcardManager.instance.interactingWithFlashcards) return;
		
		#if UNITY_ANDROID
		float mouseX = LockAxis.x * Time.deltaTime * mouseSensitivity;
		float mouseY = LockAxis.y * Time.deltaTime * mouseSensitivity;

		#else
		float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * senseX;
		float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senseY;
		
		yRotation += mouseX;
		
		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90, 90);
		
		transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
		orientation.rotation = Quaternion.Euler(0, yRotation, 0);
		#endif
		
		if (firstRay &&  secondRay) 
		{
			if(hit.distance == ray.distance) 
			{
				canPhoto = true;
				Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);
				Debug.DrawRay(transform.position, transform.forward * ray.distance, Color.green);
			}
			else 
			{
				canPhoto= false;
			}
		}

		if (Input.GetMouseButtonDown(1))
		{
			photoMode = !photoMode;
		}
		if (photoMode) 
		{
			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoom, Time.deltaTime * zoomSpeed);
			UIManager.uiManager.TurnOffBase();
			UIManager.uiManager.TurnOnPhoto();
			zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
			zoom = Mathf.Clamp(zoom, maxZoom, minZoom);
		}
		if (photoMode && canPhoto && Input.GetMouseButtonDown(0))
		{
			photoName = ray.collider.GetComponent<Categories>().data.GetFullName();
			snapCam.TakeSnapShot(photoName);
			photoMode = false;
			Camera.main.fieldOfView = baseZoom;
			UIManager.uiManager.TurnOffPhoto();
			UIManager.uiManager.TurnOnBase();
		}
		if (!photoMode)
		{
			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, baseZoom, Time.deltaTime * zoomSpeed);
			UIManager.uiManager.TurnOffPhoto();
			UIManager.uiManager.TurnOnBase();
		}
		if(Input.GetKeyDown(KeyCode.F))
		{
			FlashcardManager.instance.ToggleFlashcardInteraction(true);
			FlashcardManager.instance.ShowScreen();
		}
	}	
}