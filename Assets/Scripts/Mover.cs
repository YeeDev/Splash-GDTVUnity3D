using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] float speed = 5;
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
            jumpSpeed.y = jumpForce;
            rb.velocity = jumpSpeed;
        }
    }

    void FixedUpdate()
    {
        Vector3 translation = transform.forward * Input.GetAxis("Vertical") * speed;
        rb.MovePosition(transform.position + translation * Time.fixedDeltaTime);

        if (Input.GetButton("Horizontal"))
        {
            transform.Rotate(transform.up * turnSpeed * Input.GetAxisRaw("Horizontal"));
        }
    }
}
