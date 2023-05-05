using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Controller : MonoBehaviour
{
    [SerializeField] Transform groundChecker;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float checkerRadius;

    float hAxis = 0;
    float vAxis = 0;
    Mover mover;

    private bool IsGrounded => Physics.CheckSphere(groundChecker.position, checkerRadius, groundMask);

    private void Awake()
    {
        mover = GetComponent<Mover>();
    }

    private void Update()
    {
        SetMoveAxis();
        JumpInput();
        TurnInput();
    }

    void FixedUpdate() => mover.Move(vAxis, hAxis);

    private void SetMoveAxis()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
    }

    private void JumpInput()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded) { mover.Jump(vAxis, hAxis); }
        else if (Input.GetButtonUp("Jump") && !IsGrounded) { mover.HaltJump(); }
    }

    private void TurnInput() => mover.Turn(hAxis);

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (groundChecker == null) { return; }

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundChecker.position, checkerRadius);
    }
#endif
}
