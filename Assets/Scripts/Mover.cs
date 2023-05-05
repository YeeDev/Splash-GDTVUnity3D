using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] float forwardSpeed = 5;
    [SerializeField] float backWalkFactor = 2;
    [SerializeField] float turnSpeed = 5;
    [SerializeField] float jumpForce = 10;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Vector3 jumpSpeed = rb.velocity;
            jumpSpeed.y = jumpForce * Input.GetAxis("Vertical");
            rb.velocity = jumpSpeed;
        }
    }

    void FixedUpdate()
    {
        float walkSpeed = Input.GetAxis("Vertical");
        walkSpeed *= walkSpeed < 0 ? backWalkFactor : forwardSpeed;
        Vector3 translation = transform.forward * walkSpeed;
        rb.MovePosition(transform.position + translation * Time.fixedDeltaTime);

        Debug.Log(Input.GetAxis("Vertical"));

        if (Input.GetButton("Horizontal"))
        {
            transform.Rotate(transform.up * turnSpeed * Input.GetAxisRaw("Horizontal"));
        }
    }
}
