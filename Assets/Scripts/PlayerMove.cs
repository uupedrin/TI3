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

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        direction = orientation.forward * moveY + orientation.right * moveX;
        body.AddForce(direction.normalized * moveForce);
        if(body.velocity.magnitude > maxSpeed) body.velocity = Vector3.ClampMagnitude(body.velocity, maxSpeed);
    }
}
