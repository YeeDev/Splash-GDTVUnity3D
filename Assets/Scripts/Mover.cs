using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [Range(0, 10)] [SerializeField] float forwardSpeed = 5;
    [Range(0, 10)] [SerializeField] float backWalkFactor = 2;
    [Range(0, 10)] [SerializeField] float turnSpeedPenalty = 0.1f;
    [Range(0, 10)] [SerializeField] float turnSpeed = 5;
    [Range(0, 10)] [SerializeField] float jumpForce = 10;
    [Range(0, 10)] [SerializeField] float minJumpForce = 1;

    Rigidbody rb;

    private void Awake() => rb = GetComponent<Rigidbody>();

    //Called in Controller in - JumpInput
    public void Jump(float vAxis, float hAxis)
    {
        Vector3 jumpSpeed = rb.velocity;
        jumpSpeed.y = Mathf.Clamp(jumpForce * (CalculateWalkSpeed(vAxis, hAxis) / forwardSpeed), minJumpForce, jumpForce);
        rb.velocity = jumpSpeed;
    }

    //Called in Controller in - JumpInput
    public void HaltJump()
    {
        if (rb.velocity.y <= 0) { return; }

        Vector3 jumpSpeed = rb.velocity;
        jumpSpeed.y *= 0.5f;
        rb.velocity = jumpSpeed;
    }

    //Called in Controller in - FixedUpdate
    public void Move(float vAxis, float xAxis)
    {
        Vector3 translation = transform.forward * CalculateWalkSpeed(vAxis, xAxis);
        rb.MovePosition(transform.position + translation * Time.fixedDeltaTime);
    }

    //Called in Controller in - TurnInput
    public void Turn(float hAxis) => transform.Rotate(transform.up * turnSpeed * hAxis);

    private float CalculateWalkSpeed(float vAxis, float hAxis)
    {
        float walkSpeed = vAxis;
        walkSpeed *= walkSpeed < 0 ? backWalkFactor : forwardSpeed;

        if (vAxis > Mathf.Epsilon)
        {
            walkSpeed = Mathf.Clamp(walkSpeed - Mathf.Abs(hAxis) * turnSpeedPenalty, 0, walkSpeed);
        }

        Debug.Log(walkSpeed);
        return walkSpeed;
    }
}
