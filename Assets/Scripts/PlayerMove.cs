using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
    }

    void FixedUpdate()
    {
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
}
