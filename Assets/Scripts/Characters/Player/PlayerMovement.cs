using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float groundDrag;
    [SerializeField] float airDrag;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;

    bool readyToJump = true;

    Vector3 moveDir;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slopes")]
    [SerializeField] float maxSlopeAngle;
    [SerializeField] float minSlopeAngle;
    RaycastHit slopeHit;
    bool exitingSlope;

    [Header("References")]
    [SerializeField] InputActionReference moveInputDir;
    [SerializeField] InputActionReference jumpInput;

    [SerializeField] Transform orientation;

    Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update(){
        LimitSpeed();

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        if(grounded) rb.drag = groundDrag;
        else rb.drag = airDrag;

        if(jumpInput.action.IsPressed()) Jump();

        Debug.Log(grounded);
    }

    void FixedUpdate(){
        MovePlayer();
    }

    void MovePlayer(){
        Vector2 inputDir = moveInputDir.action.ReadValue<Vector2>();
        moveDir = orientation.forward * inputDir.y + orientation.right * inputDir.x;

        if(OnSlope() && !exitingSlope){
            rb.AddForce(GetSlopeMoveDir() * moveSpeed * 20f, ForceMode.Force);

            if(rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        if(grounded)
            rb.AddForce(moveDir * moveSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDir * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        rb.useGravity = !OnSlope();
    }

    void LimitSpeed(){
        if(OnSlope() && !exitingSlope)
        {
            if(rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }
        else
        {
            Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            if(horizontalVelocity.magnitude > moveSpeed){
                Vector3 limitedHorizontalVelocity = horizontalVelocity.normalized * moveSpeed;

                rb.velocity = new Vector3
                (
                    limitedHorizontalVelocity.x,
                    rb.velocity.y,
                    limitedHorizontalVelocity.z
                );
            }
        }
    }

    void Jump(){
        if(!grounded || !readyToJump) return;

        exitingSlope = true;

        readyToJump = false;
        Invoke(nameof(ReadyJump), jumpCooldown);

        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ReadyJump(){
        readyToJump = true;
        exitingSlope = false;
    }

    bool OnSlope(){
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f)){
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle > minSlopeAngle;
        }
        return false;
    }

    Vector3 GetSlopeMoveDir(){
        return Vector3.ProjectOnPlane(moveDir, slopeHit.normal).normalized;
    }
}
