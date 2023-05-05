using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] float forwardSpeed = 5;
    [SerializeField] float backWalkFactor = 2;
    [SerializeField] float turnSpeed = 5;
    [SerializeField] float jumpForce = 10;
    [SerializeField] float turnSpeedPenalty = 0.1f;
    [SerializeField] Transform groundChecker;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float checkerRadius;

    Rigidbody rb;

    private void Awake() => rb = GetComponent<Rigidbody>();

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && Physics.CheckSphere(groundChecker.position, checkerRadius, groundMask))
        {
            Vector3 jumpSpeed = rb.velocity;
            jumpSpeed.y = jumpForce * Input.GetAxis("Vertical");
            rb.velocity = jumpSpeed;
        }
        else if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            Vector3 haltSpeed = rb.velocity;
            haltSpeed.y *= 0.5f;
            rb.velocity = haltSpeed;
        }
    }

    void FixedUpdate()
    {
        float walkSpeed = Input.GetAxis("Vertical");
        walkSpeed *= walkSpeed < 0 ? backWalkFactor : forwardSpeed;
        walkSpeed = Mathf.Clamp(walkSpeed - Mathf.Abs(Input.GetAxis("Horizontal")) * turnSpeedPenalty, 0 , walkSpeed);
        Vector3 translation = transform.forward * walkSpeed;
        rb.MovePosition(transform.position + translation * Time.fixedDeltaTime);

        if (Input.GetButton("Horizontal"))
        {
            transform.Rotate(transform.up * turnSpeed * Input.GetAxisRaw("Horizontal"));
        }
    }
}
