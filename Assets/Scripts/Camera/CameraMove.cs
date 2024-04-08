using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    float mouseSensitivity = 3f;
    [SerializeField]
    float cameraVerticalRotation = 3f;
    float cameraHorizontalRotation = 3f;

    [SerializeField] Transform orientation;

    [SerializeField]
    float zoom;
    float baseZoom = 60;
    float maxZoom = 25;
    float minZoom = 50;
    float sense = 10;


    [SerializeField]
    SnapCam snapCam;
    [SerializeField]
    bool canPhoto;
    [SerializeField]
    bool photoMode = false;
    RaycastHit hit;
    RaycastHit ray;
    bool firstRay;
    bool secondRay;
    int layerMask = 1<<6;
    int layerMask2 = ~20;
    public Vector2 LockAxis;
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
    }
    private void Update()
    {
        #if UNITY_ANDROID
        float mouseX = LockAxis.x * Time.deltaTime * mouseSensitivity;
        float mouseY = LockAxis.y * Time.deltaTime * mouseSensitivity;

        #else
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        #endif

        cameraVerticalRotation -= inputY;
        cameraHorizontalRotation += inputX;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = new Vector3(cameraVerticalRotation, cameraHorizontalRotation, 0);

        player.Rotate(Vector3.up * inputX);
        
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

        if (!photoMode && Input.GetKeyDown("space"))
        {
            photoMode = true;
        }
        if (photoMode) 
        {
            Camera.main.fieldOfView = zoom;
            zoom += Input.GetAxis("Mouse ScrollWheel") * sense;
            zoom = Mathf.Clamp(zoom, maxZoom, minZoom);
        }
        if (photoMode && canPhoto && (Input.GetKeyDown("return") || Input.GetMouseButtonDown(0)))
        {
            snapCam.TakeSnapShot();
            photoMode = false;
            Camera.main.fieldOfView = baseZoom;
        }
        if (photoMode && (Input.GetKeyDown("escape") || Input.GetMouseButtonDown(1)))
        {
            photoMode = false;
            Camera.main.fieldOfView = baseZoom;
        }
    }
}